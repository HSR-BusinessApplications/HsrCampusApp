// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Infrastructure.Repository
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using ApplicationServices;
    using DomainServices;
    using Model;
    using Plugin.Settings.Abstractions;
    using SQLite;

    public class NewsRepository : INewsRepository, IDisposable
    {
        private const string PicturePath = "Picture/{0}.jpg";
        private const string IconPath = "Icon/{0}{1}.png";
        private const string DbName = "news.sqlite";

        private static readonly string LastUpdatedKey = typeof(NewsRepository).FullName + "LastUpdated";
        private static readonly object Locking = new object();
        private readonly SQLiteConnection conn;

        private readonly IDevice device;
        private readonly IIOCacheService cache;
        private readonly ISettings settings;

        private bool disposedValue;

        public NewsRepository(IIOCacheService cache, IDevice device, ISettings settings)
        {
            this.cache = cache;
            this.device = device;
            this.settings = settings;
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var dbFilePath = Path.Combine(folderPath, DbName);
            this.conn = new SQLiteConnection(dbFilePath);
            lock (Locking)
            {
                this.conn.CreateTable<SqlNewsFeed>();
                this.conn.CreateTable<SqlNews>();
            }
        }

        public DateTime LastUpdated
        {
            get
            {
                return this.settings.GetValueOrDefault(LastUpdatedKey, default(DateTime));
            }

            private set
            {
                this.settings.AddOrUpdateValue(LastUpdatedKey, value);
            }
        }

        public void UpdateFeeds(IEnumerable<WhNewsFeed> feeds)
        {
            if (!feeds.Any())
            {
                return;
            }

            var marker = Guid.NewGuid().ToString();

            lock (Locking)
            {
                var markedFeeds = new List<SqlNewsFeed>();
                foreach (var feed in this.conn.Table<SqlNewsFeed>())
                {
                    feed.Marker = marker;
                    markedFeeds.Add(feed);
                }

                this.conn.UpdateAll(markedFeeds);

                this.conn.InsertAll(feeds.Select(ConvertFeed), "OR REPLACE");

                this.DeleteFeeds(feed => feed.Marker == marker);
            }

            this.LastUpdated = DateTime.UtcNow;
        }

        public IEnumerable<SqlNewsFeed> RetrieveFeeds()
        {
            lock (Locking)
            {
                return this.conn.Table<SqlNewsFeed>().OrderBy(feed => feed.Index);
            }
        }

        public void UpdateIcon(SqlNewsFeed feed, byte[] icon, int size)
        {
            feed.IconPath = string.Format(IconPath, feed.Key, size);

            this.cache.AssurePath(feed.IconPath);
            this.cache.WriteBitmap(feed.IconPath, icon);

            feed.LastIconUpdate = DateTime.UtcNow;

            lock (Locking)
            {
                this.conn.Update(feed);
            }
        }

        public IEnumerable<SqlNews> UpdateNewsRange(SqlNewsFeed feed, IEnumerable<WhNews> newPosts)
        {
            if (!newPosts.Any())
            {
                return Enumerable.Empty<SqlNews>();
            }

            if (this.device.Platform != DevicePlatform.iOS)
            {
                this.cache.AssurePath(string.Format(PicturePath, "file"));
            }

            newPosts = newPosts.OrderByDescending(news => news.Date).ToList();

            var result = new List<SqlNews>();

            lock (Locking)
            {
                var minDate = newPosts.Min(news => news.Date);
                var maxDate = newPosts.Max(news => news.Date);

                this.DeleteNews(news => news.FeedKey == feed.Key
                                    && news.Date > minDate
                                    && news.Date < maxDate);

                foreach (var newPost in newPosts)
                {
                    string picturePath = null;

                    if (!string.IsNullOrEmpty(newPost.PictureBitmap))
                    {
                        var cachePath = string.Format(PicturePath, newPost.NewsId);

                        try
                        {
                            if (this.device.Platform != DevicePlatform.iOS)
                            {
                                this.cache.WriteBitmap(cachePath, Convert.FromBase64String(newPost.PictureBitmap));
                            }

                            picturePath = cachePath;
                        }
                        catch
                        {
                            // Probably no picture available
                        }
                    }

                    var sqlNews = ConvertNews(feed, newPost, picturePath);
                    result.Add(sqlNews);
                }

                this.conn.InsertAll(result, "OR REPLACE");
            }

            return result;
        }

        public bool HasNewsBefore(SqlNewsFeed feed, DateTime before)
        {
            lock (Locking)
            {
                return this.conn.Table<SqlNews>()
                    .Any(news => news.FeedKey == feed.Key && news.Date < before);
            }
        }

        public IEnumerable<SqlNews> RetrieveNewsBefore(SqlNewsFeed feed, DateTime before, int amount)
        {
            lock (Locking)
            {
                return this.conn.Table<SqlNews>()
                    .Where(news => news.FeedKey == feed.Key && news.Date < before)
                    .OrderByDescending(news => news.Date)
                    .Take(amount);
            }
        }

        public void DeleteNewsEntries(SqlNewsFeed oldFeed)
        {
            lock (Locking)
            {
                this.DeleteNews(news => news.FeedKey == oldFeed.Key);
            }
        }

        public void Truncate()
        {
            lock (Locking)
            {
                this.DeleteFeeds();
                this.DeleteNews();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.conn?.Close();
                }

                this.disposedValue = true;
            }
        }

        private static SqlNewsFeed ConvertFeed(WhNewsFeed feed, int index)
        {
            return new SqlNewsFeed
            {
                Key = feed.Key,
                Index = index,
                Name = feed.Name,
                HsrSport = feed.HsrSport
            };
        }

        private static SqlNews ConvertNews(SqlNewsFeed feed, WhNews whPost, string picturePath)
        {
            return new SqlNews
            {
                Date = whPost.Date.ToUniversalTime(),
                FeedKey = feed.Key,
                LastUpdated = DateTime.UtcNow,
                PicturePath = picturePath,
                Message = whPost.Message,
                NewsId = whPost.NewsId,
                Title = whPost.Title,
                Url = whPost.Url
            };
        }

        private void DeleteFeeds(Expression<Func<SqlNewsFeed, bool>> condition = null)
        {
            lock (Locking)
            {
                var oldFeeds = condition == null
                    ? this.conn.Table<SqlNewsFeed>()
                    : this.conn.Table<SqlNewsFeed>().Where(condition);

                foreach (var oldFeed in oldFeeds)
                {
                    if (this.device.Platform != DevicePlatform.Android && oldFeed.IconPath != null)
                    {
                        this.cache.Delete(oldFeed.IconPath);
                    }

                    this.DeleteNews(news => news.FeedKey == oldFeed.Key);
                }

                if (condition == null)
                {
                    this.conn.DeleteAll<SqlNewsFeed>();
                }
                else
                {
                    this.conn.Table<SqlNewsFeed>()
                        .Delete(condition);
                }

                if (!this.conn.Table<SqlNewsFeed>().Any())
                {
                    this.LastUpdated = DateTime.MinValue;
                }
            }
        }

        private void DeleteNews(Expression<Func<SqlNews, bool>> condition = null)
        {
            lock (Locking)
            {
                if (this.device.Platform != DevicePlatform.iOS)
                {
                    var oldPosts = condition == null
                                   ? this.conn.Table<SqlNews>()
                                   : this.conn.Table<SqlNews>().Where(condition);

                    foreach (var oldPost in oldPosts)
                    {
                        if (oldPost.PicturePath != null)
                        {
                            this.cache.Delete(oldPost.PicturePath);
                        }
                    }
                }

                if (condition == null)
                {
                    this.conn.DeleteAll<SqlNews>();
                }
                else
                {
                    this.conn.Table<SqlNews>()
                        .Delete(condition);
                }
            }
        }
    }
}

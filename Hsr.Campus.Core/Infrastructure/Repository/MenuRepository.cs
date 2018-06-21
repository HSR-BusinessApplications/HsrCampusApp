// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Infrastructure.Repository
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using DomainServices;
    using Model;
    using Plugin.Settings.Abstractions;
    using SQLite;

    public class MenuRepository : IMenuRepository, IDisposable
    {
        private const string DbName = "menu.sqlite";

        private static readonly string LastUpdatedKey = typeof(MenuRepository).FullName + "LastUpdated";
        private static readonly object Locking = new object();
        private readonly SQLiteConnection conn;

        private readonly ISettings settings;

        private bool disposedValue;

        public MenuRepository(ISettings settings)
        {
            this.settings = settings;

            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var dbFilePath = Path.Combine(folderPath, DbName);
            this.conn = new SQLiteConnection(dbFilePath);
            lock (Locking)
            {
                this.conn.CreateTable<SqlMenuFeed>();
                this.conn.CreateTable<SqlMenu>();
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

        public void UpdateFeeds(IEnumerable<WhMenuFeed> feeds)
        {
            if (!feeds.Any())
            {
                return;
            }

            var marker = Guid.NewGuid().ToString();

            lock (Locking)
            {
                var markedFeeds = new List<SqlMenuFeed>();
                foreach (var feed in this.conn.Table<SqlMenuFeed>())
                {
                    feed.Marker = marker;
                    markedFeeds.Add(feed);
                }

                this.conn.UpdateAll(markedFeeds);

                this.conn.InsertAll(feeds.Select(ConvertMenuFeed), "OR REPLACE");

                this.DeleteMenuFeeds(feed => feed.Marker == marker);
            }

            this.LastUpdated = DateTime.UtcNow;
        }

        public IEnumerable<SqlMenuFeed> RetrieveFeeds()
        {
            lock (Locking)
            {
                return this.conn.Table<SqlMenuFeed>();
            }
        }

        public void UpdateMenu(string id, string feedId, WhMenuDay day, string menuHtml)
        {
            lock (Locking)
            {
                this.conn.InsertOrReplace(ConvertMenu(id, feedId, day, menuHtml));
            }
        }

        public IEnumerable<SqlMenu> RetrieveMenus(string feedId)
        {
            lock (Locking)
            {
                return this.conn.Table<SqlMenu>().Where(menu => menu.FeedId == feedId).OrderBy(menu => menu.Date);
            }
        }

        public SqlMenu RetrieveMenu(string id)
        {
            lock (Locking)
            {
                return this.conn.Table<SqlMenu>().FirstOrDefault(menu => menu.Id == id);
            }
        }

        public string MarkMenus()
        {
            var marker = Guid.NewGuid().ToString();

            lock (Locking)
            {
                var markedMenus = new List<SqlMenu>();
                foreach (var menu in this.conn.Table<SqlMenu>())
                {
                    menu.Marker = marker;
                    markedMenus.Add(menu);
                }

                this.conn.UpdateAll(markedMenus);
            }

            return marker;
        }

        public void UnmarkMenu(string id)
        {
            lock (Locking)
            {
                var markedMenus = new List<SqlMenu>();
                foreach (var menu in this.conn.Table<SqlMenu>()
                                         .Where(menu => menu.Id == id))
                {
                    menu.Marker = default(string);
                    markedMenus.Add(menu);
                }

                this.conn.UpdateAll(markedMenus);
            }
        }

        public void DeleteMarkedMenus(string marker)
        {
            lock (Locking)
            {
                this.DeleteMenus(menu => menu.Marker == marker);
            }
        }

        public void Truncate()
        {
            lock (Locking)
            {
                this.DeleteMenuFeeds();
                this.DeleteMenus();
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

        private static SqlMenuFeed ConvertMenuFeed(WhMenuFeed feed)
        {
            return new SqlMenuFeed
            {
                Id = feed.Id,
                ShortName = feed.ShortName,
                Name = feed.Name
            };
        }

        private static SqlMenu ConvertMenu(string id, string feedId, WhMenuDay day, string menu)
        {
            return new SqlMenu
            {
                Id = id,
                FeedId = feedId,
                Date = day.Date,
                ContentHash = day.ContentHash,
                HtmlPage = menu
            };
        }

        private void DeleteMenuFeeds(Expression<Func<SqlMenuFeed, bool>> condition = null)
        {
            lock (Locking)
            {
                if (condition == null)
                {
                    this.conn.DeleteAll<SqlMenuFeed>();
                }
                else
                {
                    this.conn.Table<SqlMenuFeed>()
                        .Delete(condition);
                }

                if (!this.conn.Table<SqlMenuFeed>().Any())
                {
                    this.LastUpdated = DateTime.MinValue;
                }
            }
        }

        private void DeleteMenus(Expression<Func<SqlMenu, bool>> condition = null)
        {
            lock (Locking)
            {
                if (condition == null)
                {
                    this.conn.DeleteAll<SqlMenu>();
                }
                else
                {
                    this.conn.Table<SqlMenu>()
                        .Delete(condition);
                }
            }
        }
    }
}

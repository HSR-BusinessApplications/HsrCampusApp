// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using ApplicationServices;
    using DomainServices;
    using Model;
    using MvvmCross.Core.Navigation;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.Plugins.WebBrowser;

    public class NewsFeedViewModel : AbstractCollectionViewModel<SqlNews>, ITitled
    {
        private readonly INewsRepository newsRepository;
        private readonly INewsSync newsSync;
        private readonly IMvxWebBrowserTask webBrowser;

        private string title;
        private SqlNewsFeed feed;
        private bool hasMoreItems;

        public NewsFeedViewModel(IMvxNavigationService navigationService, INewsRepository newsRepository, INewsSync newsSync, IMvxWebBrowserTask webBrowser)
        {
            this.NavigationService = navigationService;
            this.newsRepository = newsRepository;
            this.newsSync = newsSync;
            this.webBrowser = webBrowser;
        }

        public MvxAsyncCommand LoadMoreCommand => new MvxAsyncCommand(() => this.UpdateAsync(false, true));

        public ICommand ShowDetailCommand => new MvxCommand<SqlNews>(this.ShowDetail);

        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
                this.RaisePropertyChanged();
            }
        }

        public SqlNewsFeed Feed
        {
            get
            {
                return this.feed;
            }

            set
            {
                this.feed = value;
                this.RaisePropertyChanged();
            }
        }

        public bool HasMoreItems
        {
            get
            {
                return this.hasMoreItems;
            }

            private set
            {
                this.hasMoreItems = value;
                this.RaisePropertyChanged();
            }
        }

        public void Init(SqlNewsFeed parentFeed)
        {
            this.Feed = parentFeed;
            this.Title = parentFeed.Name;
            this.HasMoreItems = true;
            this.Items = new ObservableCollection<SqlNews>();
            this.LoadFromCache(DateTime.Now);
        }

        public async Task UpdateAsync(bool force, bool loadMore)
        {
            if (this.IsUpdating)
            {
                return;
            }

            this.IsUpdating = true;

            DateTime dateOfLastEntry;
            if (loadMore && this.Items.Count > 0)
            {
                dateOfLastEntry = this.Items.Last().Date;
                this.LoadFromCache(dateOfLastEntry);
            }
            else
            {
                dateOfLastEntry = DateTime.Now;
            }

            this.HasMoreItems = false;

            var updateResult = await this.newsSync.UpdateNewsAsync(force, this.Feed, dateOfLastEntry, this.ObtainCancellationToken());

            switch (updateResult.Item1)
            {
                case ResultState.OAuthExpired:
                case ResultState.Error:
                case ResultState.ErrorNetwork:
                    break;
                case ResultState.NoData:
                    if (force)
                    {
                        this.Items = new ObservableCollection<SqlNews>();
                    }

                    this.HasMoreItems = false;
                    break;
                case ResultState.NotModified:
                case ResultState.Canceled:
                    this.HasMoreItems = true;
                    break;
                case ResultState.Success:
                    this.MergeUpdateResult(updateResult.Item2, dateOfLastEntry);
                    this.HasMoreItems = true;
                    break;
                default:
                    this.IsUpdating = false;
                    Debug.Assert(false, $"ResultState: {updateResult.Item1}, not handled");
                    return;
            }

            this.IsUpdating = false;
        }

        public void ShowDetail(SqlNews news)
        {
            try
            {
                this.webBrowser.ShowWebPage(news.Url);
            }
            catch
            {
                // ignored
            }
        }

        private void LoadFromCache(DateTime dateBefore)
        {
            if (this.IsLoading)
            {
                return;
            }

            this.IsLoading = true;

            var news = this.newsRepository.RetrieveNewsBefore(this.Feed, dateBefore, 10);

            foreach (var newsPost in news)
            {
                this.Items.Add(newsPost);
            }

            this.HasContent = this.Items.Count > 0;

            this.IsLoading = false;
        }

        private void MergeUpdateResult(IEnumerable<SqlNews> newsList, DateTime dateOfLastEntry)
        {
            if (this.IsLoading)
            {
                return;
            }

            this.IsLoading = true;

            var first = this.Items.FirstOrDefault(news => news.Date < dateOfLastEntry);

            var itemsIndex = first == default(SqlNews) ? this.Items.Count : this.Items.IndexOf(first);

            foreach (var news in newsList)
            {
                if (this.Items.Contains(news, new NewsIdComperator()))
                {
                    var oldIndex = this.Items.IndexOf(this.Items.First(sqlNews => sqlNews.NewsId == news.NewsId));
                    this.Items[oldIndex] = news;

                    if (oldIndex != itemsIndex)
                    {
                        if (itemsIndex < this.Items.Count)
                        {
                            this.Items.Move(oldIndex, itemsIndex);
                        }
                        else
                        {
                            var removedItem = this.Items[oldIndex];

                            this.Items.RemoveAt(oldIndex);
                            this.Items.Add(removedItem);
                        }
                    }
                }
                else
                {
                    if (itemsIndex < this.Items.Count)
                    {
                        this.Items.Insert(itemsIndex, news);
                    }
                    else
                    {
                        this.Items.Add(news);
                    }
                }

                itemsIndex++;
            }

            while (itemsIndex < this.Items.Count)
            {
                this.Items.RemoveAt(itemsIndex);
            }

            this.HasContent = this.Items.Count > 0;

            this.IsLoading = false;
        }

        private class NewsIdComperator : IEqualityComparer<SqlNews>
        {
            public bool Equals(SqlNews x, SqlNews y)
            {
                return x.NewsId == y.NewsId;
            }

            public int GetHashCode(SqlNews obj)
            {
                return obj.NewsId.GetHashCode();
            }
        }
    }
}

// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using ApplicationServices;
    using DomainServices;
    using Model;
    using MvvmCross.Core.Navigation;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.Platform;
    using Resources;

    public class NewsViewModel : AbstractCollectionViewModel<NewsFeedViewModel>
    {
        private readonly INewsRepository newsRepository;
        private readonly INewsSync newsSync;
        private readonly IUserInteractionService userInteraction;
        private int subUpdating;
        private bool isLocalUpdating;

        public NewsViewModel(IMvxNavigationService navigationService, INewsRepository newsRepository, INewsSync newsSync, IUserInteractionService userInteraction)
        {
            this.NavigationService = navigationService;
            this.newsRepository = newsRepository;
            this.newsSync = newsSync;
            this.userInteraction = userInteraction;
        }

        public ICommand UpdateCommand => new MvxAsyncCommand(() => this.UpdateAsync(true));

        public override DateTime LastUpdated => this.newsRepository.LastUpdated.ToLocalTime();

        private bool IsLocalUpdating
        {
            get
            {
                return this.isLocalUpdating;
            }

            set
            {
                this.isLocalUpdating = value;
                this.IsUpdating = this.isLocalUpdating || this.subUpdating > 0;
            }
        }

        public override async void Start()
        {
            base.Start();

            this.LoadFromCache();
            await this.UpdateAsync(false);
        }

        public async Task UpdateAsync(bool force)
        {
            if (this.IsLocalUpdating)
            {
                return;
            }

            this.IsLocalUpdating = true;

            var tasks = new List<Task> { this.UpdateFeedsAsync(force) };
            tasks.AddRange(this.Items.Select(newsFeedViewModel => newsFeedViewModel.UpdateAsync(force, false)));
            await Task.WhenAll(tasks);

            this.IsLocalUpdating = false;
        }

        private async Task UpdateFeedsAsync(bool force)
        {
            var state = await this.newsSync.UpdateFeedsAsync(force, this.ObtainCancellationToken());

            switch (state)
            {
                case ResultState.Success:
                case ResultState.NoData:

                    this.RaisePropertyChanged(nameof(this.LastUpdated));

                    this.MergeUpdateResult(this.newsRepository.RetrieveFeeds());
                    break;
                case ResultState.Error:
                case ResultState.ErrorNetwork:
                    this.userInteraction.Toast(AppResources.ConnectionFailed, ToastTime.Short);
                    break;
                case ResultState.OAuthExpired:
                case ResultState.NotModified:
                case ResultState.Canceled:
                    break;
                default:
                    Debug.Assert(false, $"ResultState: {state}, not handled");
                    return;
            }
        }

        private void LoadFromCache()
        {
            if (this.IsLoading)
            {
                return;
            }

            this.IsLoading = true;

            this.HasContent = false;

            this.Items = new ObservableCollection<NewsFeedViewModel>();

            foreach (var feed in this.newsRepository.RetrieveFeeds())
            {
                this.ItemsAddFeed(feed);
            }

            this.RaisePropertyChanged(nameof(this.Items));

            this.IsLoading = false;
        }

        private void MergeUpdateResult(IEnumerable<SqlNewsFeed> feeds)
        {
            if (this.IsLoading)
            {
                return;
            }

            this.IsLoading = true;

            var itemsIndex = 0;

            foreach (var feed in feeds)
            {
                if (this.Items.Any(model => model.Feed.Key == feed.Key))
                {
                    var oldIndex = this.Items.IndexOf(this.Items.First(model => model.Feed.Key == feed.Key));
                    this.Items[oldIndex].Feed = feed;

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
#pragma warning disable 4014
                        this.ItemsAddFeed(feed, itemsIndex).UpdateAsync(false, false);
                    }
                    else
                    {
                        this.ItemsAddFeed(feed).UpdateAsync(false, false);
#pragma warning restore 4014
                    }
                }

                itemsIndex++;
            }

            while (itemsIndex < this.Items.Count)
            {
                this.Items.RemoveAt(itemsIndex);
            }

            this.RaisePropertyChanged(nameof(this.Items));

            this.HasContent = this.Items.Count > 0;

            this.IsLoading = false;
        }

        private NewsFeedViewModel ItemsAddFeed(SqlNewsFeed feed, int index = -1)
        {
            var newsFeedViewModel = Mvx.IocConstruct<NewsFeedViewModel>();

            newsFeedViewModel.PropertyChanged += this.OnSubIsUpdating;

            newsFeedViewModel.Init(feed);

            if (index == -1)
            {
                this.Items.Add(newsFeedViewModel);
                this.HasContent = true;
            }
            else
            {
                this.Items.Insert(index, newsFeedViewModel);
            }

            return newsFeedViewModel;
        }

        private void OnSubIsUpdating(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(NewsFeedViewModel.IsUpdating))
            {
                this.subUpdating += ((NewsFeedViewModel)sender).IsUpdating ? 1 : -1;

                this.IsUpdating = this.IsLocalUpdating || this.subUpdating > 0;
            }
        }
    }
}

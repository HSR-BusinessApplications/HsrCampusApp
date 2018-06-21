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
    using MvvmCross.Platform;
    using Resources;

    public class MenuViewModel : AbstractCollectionViewModel<MenuFeedViewModel>
    {
        private readonly IUserInteractionService userInteraction;
        private readonly IMenuRepository menuRepository;

        private readonly IMenuSync menuSync;

        private MenuFeedViewModel selectedFeedVm;

        public MenuViewModel(IMvxNavigationService navigationService, IUserInteractionService userInteraction, IMenuRepository menuRepository, IMenuSync menuSync)
        {
            this.NavigationService = navigationService;
            this.userInteraction = userInteraction;
            this.menuRepository = menuRepository;
            this.menuSync = menuSync;
        }

        public event Action UpdateSelectedItemAction;

        public DateTime SelectedDay { get; set; }

        public ICommand LoadCacheCommand => new MvxCommand(this.LoadFromCache);

        public ICommand UpdateCommand => new MvxAsyncCommand(() => this.UpdateAsync(true));

        public MenuFeedViewModel SelectedFeedVm
        {
            get
            {
                return this.selectedFeedVm;
            }

            set
            {
                this.selectedFeedVm = value;
                this.RaisePropertyChanged();
            }
        }

        public override DateTime LastUpdated => this.menuRepository.LastUpdated.ToLocalTime();

        public void UpdateSelectedItem()
        {
            this.UpdateSelectedItemAction?.Invoke();
        }

        public override async void Start()
        {
            base.Start();

            this.SelectedDay = DateTime.Today;
            this.UpdateSelectedItem();

            this.LoadFromCache();
            await this.UpdateAsync(false);
        }

        public async Task UpdateAsync(bool force)
        {
            if (this.IsUpdating)
            {
                return;
            }

            this.IsUpdating = true;

            var state = await this.menuSync.UpdateAsync(force, this.ObtainCancellationToken());

            switch (state)
            {
                case ResultState.Success:
                case ResultState.NoData:

                    this.RaisePropertyChanged(nameof(this.LastUpdated));

                    this.MergeUpdateResult(this.menuRepository.RetrieveFeeds());
                    break;
                case ResultState.Error:
                case ResultState.ErrorNetwork:
                    if (force)
                    {
                        this.userInteraction.Toast(AppResources.ConnectionFailed, ToastTime.Short);
                    }

                    break;
                case ResultState.OAuthExpired:
                case ResultState.NotModified:
                case ResultState.Canceled:
                    break;
                default:
                    Debug.Assert(false, $"ResultState: {state}, not handled");
                    return;
            }

            this.IsUpdating = false;
        }

        private MenuFeedViewModel CreateViewModelFromFeed(SqlMenuFeed feed)
        {
            var menuFeedViewModel = Mvx.IocConstruct<MenuFeedViewModel>();

            menuFeedViewModel.SetFeed(feed);

            menuFeedViewModel.MenuViewModel = this;

            return menuFeedViewModel;
        }

        private void LoadFromCache()
        {
            if (this.IsLoading)
            {
                return;
            }

            this.IsLoading = true;

            var items = new ObservableCollection<MenuFeedViewModel>();

            foreach (var feed in this.menuRepository.RetrieveFeeds())
            {
                items.Add(this.CreateViewModelFromFeed(feed));
            }

            this.Items = items;

            this.HasContent = this.Items.Count > 0;

            this.SelectedFeedVm = this.HasContent ? this.Items[0] : null;

            this.IsLoading = false;
        }

        private void MergeUpdateResult(IEnumerable<SqlMenuFeed> feeds)
        {
            if (this.IsLoading)
            {
                return;
            }

            this.IsLoading = true;

            var itemsIndex = 0;

            foreach (var feed in feeds)
            {
                if (this.Items.Any(model => model.Feed.Id == feed.Id))
                {
                    var oldIndex = this.Items.IndexOf(this.Items.First(model => model.Feed.Id == feed.Id));
                    this.Items[oldIndex].SetFeed(feed);

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
                        this.Items.Insert(itemsIndex, this.CreateViewModelFromFeed(feed));
                    }
                    else
                    {
                        this.Items.Add(this.CreateViewModelFromFeed(feed));
                    }
                }

                itemsIndex++;
            }

            while (this.Items.Count > itemsIndex)
            {
                this.Items.RemoveAt(itemsIndex);
            }

            this.RaisePropertyChanged(nameof(this.Items));

            this.HasContent = this.Items.Count > 0;

            if (!this.Items.Contains(this.selectedFeedVm))
            {
                this.SelectedFeedVm = this.HasContent ? this.Items[0] : null;
            }

            this.UpdateSelectedItem();

            this.IsLoading = false;
        }
    }
}

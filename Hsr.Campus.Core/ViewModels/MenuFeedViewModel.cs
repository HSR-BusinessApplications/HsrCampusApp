// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using DomainServices;
    using Model;
    using MvvmCross.Core.Navigation;

    public class MenuFeedViewModel : AbstractCollectionViewModel<SqlMenu>, ITitled
    {
        private readonly IMenuRepository menuRepository;

        private string titleField = string.Empty;
        private SqlMenuFeed feed;

        public MenuFeedViewModel(IMvxNavigationService navigationService, IMenuRepository menuRepository)
        {
            this.NavigationService = navigationService;
            this.menuRepository = menuRepository;
        }

        public MenuViewModel MenuViewModel { get; set; }

        public string Title
        {
            get
            {
                return this.titleField;
            }

            set
            {
                this.titleField = value;
                this.RaisePropertyChanged();
            }
        }

        public SqlMenuFeed Feed
        {
            get
            {
                return this.feed;
            }

            private set
            {
                this.feed = value;
                this.RaisePropertyChanged();
            }
        }

        public void SetFeed(SqlMenuFeed sqlMenuFeed)
        {
            this.Feed = sqlMenuFeed;
            this.Title = sqlMenuFeed.ShortName;

            this.LoadFromCache();
        }

        public SqlMenu FindItemClosestToSelectedDay()
        {
            if (this.Items == null)
            {
                return null;
            }

            foreach (var item in this.Items.OrderBy(menu => menu.Date))
            {
                if (item.Date >= this.MenuViewModel.SelectedDay)
                {
                    return item;
                }
            }

            return this.Items.LastOrDefault();
        }

        private void LoadFromCache()
        {
            if (this.IsLoading)
            {
                return;
            }

            this.IsLoading = true;

            var items = new ObservableCollection<SqlMenu>();

            foreach (var menu in this.menuRepository.RetrieveMenus(this.Feed.Id))
            {
                items.Add(menu);
            }

            this.Items = items;

            this.HasContent = this.Items.Count > 0;

            this.IsLoading = false;
        }
    }
}

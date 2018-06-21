// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.Linq;
    using Core.ApplicationServices;
    using Core.Resources;
    using Core.ViewModels;
    using CoreGraphics;
    using MvvmCross.iOS.Views;
    using MvvmCross.iOS.Views.Presenters.Attributes;
    using MvvmCross.Platform;
    using UIKit;

    [MvxFromStoryboard("News")]
    [MvxChildPresentation]
    internal partial class NewsViewController : MvxTabBarViewController<NewsViewModel>
    {
        private readonly IIOCacheService cache;

        private int tabCount;

        public NewsViewController(IntPtr handle)
            : base(handle)
        {
            this.cache = Mvx.Resolve<IIOCacheService>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (this.ViewModel == null)
            {
                return;
            }

            this.LoadItemTabs();

            this.NavigationItem.Title = AppResources.TileNews;

            this.SetRightBarItem(this.ViewModel.UpdateCommand);

            this.ViewModel.Items.CollectionChanged += (sender, args) => this.LoadItemTabs();
        }

        private void LoadItemTabs()
        {
            if (this.ViewModel.Items.Count == 0)
            {
                var newsFeedViewModel = Mvx.IocConstruct<NewsFeedViewModel>();
                newsFeedViewModel.Init(new Core.Model.SqlNewsFeed()
                {
                    Name = AppResources.NoData,
                    Index = 0,
                    IconPath = null
                });
                this.CreateTabFor(newsFeedViewModel);
                this.ViewModel.Items.Add(newsFeedViewModel);
            }

            var viewControllers = this.ViewModel.Items.Select(this.CreateTabFor);

            this.ViewControllers = viewControllers.ToArray();
            this.CustomizableViewControllers = new UIViewController[] { };

            if (this.ViewControllers?.Any() == true)
            {
                this.SelectedViewController = this.ViewControllers[0];
            }
        }

        private UIViewController CreateTabFor(NewsFeedViewModel viewModel)
        {
            var controller = new UINavigationController();

            if (this.CreateViewControllerFor(viewModel) is UIViewController screen)
            {
            UIImage image = null;

            if (viewModel.Feed.IconPath != null)
            {
                image = UIImage.FromFile(this.cache.Map(viewModel.Feed.IconPath))?.Scale(new CGSize(25, 25), UIScreen.MainScreen.Scale);
            }

            screen.NavigationItem.Title = viewModel.Title;
            controller.TabBarItem = new UITabBarItem(viewModel.Title, image, this.tabCount++);

            controller.PushViewController(screen, false);
            }

            return controller;
        }
    }
}

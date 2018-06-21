// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.Linq;
    using Core.Model;
    using Core.Resources;
    using Core.ViewModels;
    using CoreGraphics;
    using MvvmCross.iOS.Views;
    using MvvmCross.iOS.Views.Presenters.Attributes;
    using MvvmCross.Platform;
    using UIKit;

    [MvxFromStoryboard("Map")]
    [CompositeView]
    [MvxChildPresentation]
    internal partial class MapDetailViewController : MvxTabBarViewController<MapViewModel>
    {
        private int tabCount;

        public MapDetailViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var viewControllers = this.ViewModel.Maps.Select(this.CreateTabFor);

            this.ViewControllers = viewControllers.ToArray();
            this.CustomizableViewControllers = new UIViewController[] { };
            this.SelectedViewController = this.ViewControllers[0];

            this.NavigationItem.Title = string.IsNullOrEmpty(this.ViewModel.Title) ? AppResources.TileCampusMap : this.ViewModel.Title;
        }

        private UIViewController CreateTabFor(MapHashable map)
        {
            var screen = new UIViewController();

            var image = Mvx.IocConstruct<ImageLoader>().LoadImage(map.ImagePath);

            var imageView = new UIImageView(new CGRect(new CGPoint(0, 100), CGSize.Subtract(screen.View.Frame.Size, new CGSize(0, 200))))
            {
                ContentMode = UIViewContentMode.ScaleAspectFit,
                Image = image
            };

            screen.Add(imageView);

            screen.NavigationItem.Title = map.Title;
            screen.TabBarItem = new UITabBarItem(map.Title, null, this.tabCount++);

            return screen;
        }
    }
}

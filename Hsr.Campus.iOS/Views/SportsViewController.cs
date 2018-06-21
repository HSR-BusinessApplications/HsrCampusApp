// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.Linq;
    using Hsr.Campus.Core.Resources;
    using Hsr.Campus.Core.ViewModels;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.iOS.Views;
    using MvvmCross.iOS.Views.Presenters.Attributes;
    using UIKit;

    [MvxFromStoryboard("Events")]
    [MvxChildPresentation]
    internal partial class SportsViewController : MvxTabBarViewController<SportsViewModel>
    {
        private int tabCount = 0;

        public SportsViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (this.ViewModel == null)
            {
                return;
            }

            var viewControllers = this.ViewModel.Items.Select(this.CreateTabFor);

            this.ViewControllers = viewControllers.ToArray();
            this.CustomizableViewControllers = new UIViewController[] { };

            if (this.ViewControllers != null && this.ViewControllers.Any())
            {
                this.SelectedViewController = this.ViewControllers[0];
            }

            this.NavigationItem.Title = AppResources.TileSport;

            this.SetRightBarItem(this.ViewModel.UpdateCommand);
        }

        private UIViewController CreateTabFor(CalendarViewModel viewModel)
        {
            var controller = new UINavigationController();
            var screen = this.CreateViewControllerFor(viewModel) as UIViewController;

            if (screen == null)
            {
                return controller;
            }

            screen.NavigationItem.Title = viewModel.Title;
            controller.TabBarItem = new UITabBarItem(viewModel.Title, UIImage.FromBundle("TabAgenda"), this.tabCount++);

            controller.PushViewController(screen, false);

            return controller;
        }
    }
}

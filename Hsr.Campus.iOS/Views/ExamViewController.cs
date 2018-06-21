// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.Linq;
    using Core.Resources;
    using Core.ViewModels;
    using Foundation;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.iOS.Views;
    using MvvmCross.iOS.Views.Presenters.Attributes;
    using UIKit;

    [Register("ExamViewController")]
    [MvxFromStoryboard("Timetable")]
    [MvxChildPresentation]
    internal partial class ExamViewController : MvxTabBarViewController<ExamViewModel>
    {
        private int tabCount = 0;

        public ExamViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var viewControllers = this.ViewModel.AllExams.Select(this.CreateTabFor);

            this.ViewControllers = viewControllers.ToArray();
            this.CustomizableViewControllers = new UIViewController[] { };

            this.SelectedViewController = this.ViewControllers[0];

            this.NavigationItem.Title = AppResources.TileExams;
        }

        private UIViewController CreateTabFor<TViewModel>(TViewModel viewModel)
            where TViewModel : class, IMvxViewModel, ITitled
        {
            var controller = new UINavigationController();
            var screen = this.CreateViewControllerFor(viewModel) as UIViewController;

            controller.PushViewController(screen, false);
            controller.TabBarItem = new UITabBarItem(viewModel.Title, null, this.tabCount++);

            screen.NavigationItem.Title = viewModel.Title;

            return controller;
        }
    }
}

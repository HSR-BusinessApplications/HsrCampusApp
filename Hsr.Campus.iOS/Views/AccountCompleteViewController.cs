// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using Hsr.Campus.Core.Resources;
    using Hsr.Campus.Core.ViewModels;
    using MvvmCross.iOS.Views;
    using MvvmCross.iOS.Views.Presenters.Attributes;

    [MvxFromStoryboard("Settings")]
    [CompositeView]
    [WrapWith(WrapWithController.NavigationController)]
    [MvxChildPresentation]
    internal partial class AccountCompleteViewController : MvxViewController<AccountViewModel>
    {
        public AccountCompleteViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.ContentText.Text = AppResources.OAuthComplete;
            this.ButtonHome.BackgroundColor = Constants.HsrBlue;
            this.ButtonHome.TouchUpInside += this.ViewModel.GoHome.ToEventHandler();

            this.NavigationItem.Title = AppResources.TileSetupAccount;
        }
    }
}

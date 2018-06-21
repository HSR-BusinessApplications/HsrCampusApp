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
    internal partial class AccountIndexViewController : MvxViewController<AccountViewModel>
    {
        public AccountIndexViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.ContentText.Text = AppResources.OAuthSetup;
            this.ButtonOAuth.BackgroundColor = Constants.HsrBlue;
            this.ButtonOAuth.TouchUpInside += this.ViewModel.GoAuthCommand.ToEventHandler();

            this.NavigationItem.Title = AppResources.SettingAccountTitle;
        }
    }
}

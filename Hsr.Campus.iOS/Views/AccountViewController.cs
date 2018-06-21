// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.Diagnostics;
    using Core.ApplicationServices;
    using Core.Resources;
    using Core.ViewModels;
    using MvvmCross.iOS.Views;
    using MvvmCross.iOS.Views.Presenters.Attributes;
    using MvvmCross.Platform;

    [MvxFromStoryboard("Settings")]
    [MvxChildPresentation]
    internal partial class AccountViewController : MvxCompositeViewController<AccountViewModel>
    {
        public AccountViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.NavigationItem.Title = AppResources.SettingAccountTitle;

            this.Show(this.ViewModel.GetAuthenticationState());

            try
            {
                this.RefreshChildView();
            }
            catch (Exception)
            {
                this.ViewModel.GoHome.Execute(null);
            }
        }

        private void Show(AccountAuthenticationState state)
        {
            this.AddChildViewController<AccountInfoViewController>(t => state == AccountAuthenticationState.Authenticated);
            this.AddChildViewController<AccountCompleteViewController>(t => state == AccountAuthenticationState.Success);
            this.AddChildViewController<AccountIndexViewController>(t => state == AccountAuthenticationState.Unauthenticated);
            this.AddChildViewController<AccountRenewViewController>(t => state == AccountAuthenticationState.Expired);
        }
    }
}

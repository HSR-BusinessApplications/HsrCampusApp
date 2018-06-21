// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using Hsr.Campus.Core.ApplicationServices;
    using Hsr.Campus.Core.Resources;
    using Hsr.Campus.Core.ViewModels;
    using MvvmCross.iOS.Views;
    using MvvmCross.iOS.Views.Presenters.Attributes;
    using MvvmCross.Platform;
    using UIKit;

    [MvxFromStoryboard("Settings")]
    [CompositeView]
    [WrapWith(WrapWithController.NavigationController)]
    [MvxChildPresentation]
    internal partial class AccountInfoViewController : MvxViewController<AccountViewModel>
    {
        private readonly IUserInteractionService userInteraction;

        public AccountInfoViewController(IntPtr handle)
            : base(handle)
        {
            this.userInteraction = Mvx.Resolve<IUserInteractionService>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.LabelIdentityText.Text = AppResources.OAuthIdentity;
            this.LabelIdentity.Text = this.ViewModel.Account.UserName;
            this.LabelRefreshText.Text = AppResources.LastUpdate;
            this.LabelRefresh.Text = this.ViewModel.Account.TokenRetrieved.ToLocalTime().ToString("g");
            this.ButtonOAuth.SetTitle(AppResources.OAuthPermissions, UIControlState.Normal);
            this.ButtonOAuth.BackgroundColor = Constants.HsrBlue;
            this.ButtonOAuth.TouchUpInside += this.ViewModel.GoViewCommand.ToEventHandler();
            this.ButtonRemove.SetTitle(AppResources.OAuthRemove, UIControlState.Normal);
            this.ButtonRemove.TouchUpInside += (sender, e) => this.userInteraction.Dialog(
                AppResources.OAuthRemove,
                AppResources.OAuthRemoveConfirm,
                () => this.ViewModel.ShredData().ContinueWith(t => this.ViewModel.GoHome.Execute(null)),
                () =>
                {
                });

            this.NavigationItem.Title = AppResources.TileSetupAccount;
        }
    }
}

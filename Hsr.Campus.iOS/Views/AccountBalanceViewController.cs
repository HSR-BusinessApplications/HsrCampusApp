// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using Core.Resources;
    using Core.ViewModels;
    using MvvmCross.Binding.BindingContext;
    using MvvmCross.iOS.Views;
    using MvvmCross.iOS.Views.Presenters.Attributes;
    using UIKit;

    [MvxFromStoryboard("Badge")]
    [MvxChildPresentation]
    internal partial class AccountBalanceViewController : MvxViewController
    {
        public AccountBalanceViewController(IntPtr handle)
            : base(handle)
        {
        }

        public new AccountBalanceViewModel ViewModel
        {
            get { return (AccountBalanceViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.BadgeText.Text = AppResources.BadgeText;

            this.CreateBinding(this.BalanceLabel)
                .For(t => t.Text)
                .To<AccountBalanceViewModel>(vm => vm.Balance)
                .WithConversion("BalanceDeposit")
                .Apply();

            this.CreateBinding(this.LastUpdateLabel)
                .For(t => t.Text)
                .To<AccountBalanceViewModel>(vm => vm.Balance)
                .WithConversion("BalanceUpdate")
                .Apply();

            this.NavigationItem.Title = AppResources.TileBadge;

            this.SetRightBarItem(this.ViewModel.UpdateCommand);
        }
    }
}

// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Views
{
    using System;
    using Android.App;
    using Android.Content;
    using Android.Widget;
    using Core.ApplicationServices;
    using Core.ViewModels;
    using Widgets;

    [Activity(Label = "@string/TileSetupAccount", Theme = "@style/Theme.View", Exported = true, Name = "hsr.campus.droid.views.AccountView", Icon = "@drawable/ic_launcher")]
    [IntentFilter(
        new[] { Intent.ActionView },
        DataScheme = "hsrcampus",
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable })]
    public class AccountView : AbstractView<AccountViewModel>
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            if (!string.IsNullOrEmpty(this.Intent.DataString))
            {
                this.ActionBar.SetDisplayHomeAsUpEnabled(false);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (!string.IsNullOrEmpty(this.Intent.DataString))
            {
                this.ViewModel.QueryString = this.Intent.DataString;
            }

            this.Show(this.ViewModel.GetAuthenticationState());

            var btnDelete = this.FindViewById<Button>(Resource.Id.btnDelete);

            if (btnDelete != null)
            {
                btnDelete.Click +=
                    (sender, args) => this.ViewModel.DeAuth(
                        () => this.Show(AccountAuthenticationState.Unauthenticated));
            }
        }

        private void Show(AccountAuthenticationState state)
        {
            switch (state)
            {
                case AccountAuthenticationState.Unauthenticated:
                    this.SetContentView(Resource.Layout.AccountViewIndex);
                    break;
                case AccountAuthenticationState.Success:
                    this.SetContentView(Resource.Layout.AccountViewComplete);
                    break;
                case AccountAuthenticationState.Authenticated:
                    this.SetContentView(Resource.Layout.AccountViewInfo);
                    break;
                case AccountAuthenticationState.Expired:
                    this.SetContentView(Resource.Layout.AccountViewRenew);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}

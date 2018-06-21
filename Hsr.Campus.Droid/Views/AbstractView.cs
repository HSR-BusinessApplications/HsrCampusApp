// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Widgets
{
    using System.Globalization;
    using System.Threading;
    using Android.App;
    using Android.OS;
    using Android.Views;
    using Core.Resources;
    using Core.ViewModels;
    using MvvmCross.Droid.Views;

    public abstract class AbstractView<TVm> : MvxActivity
        where TVm : AbstractViewModel
    {
        protected AbstractView()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(AppResources.ResourceLanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(AppResources.ResourceLanguage);
        }

        protected new TVm ViewModel => (TVm)base.ViewModel;

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    this.Finish();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            menu.BindProgressBar(this.ViewModel);
            return base.OnCreateOptionsMenu(menu);
        }

        public override void OnBackPressed()
        {
            this.Finish();
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            if (!(this.ViewModel is HomeViewModel))
            {
                this.ActionBar.SetDisplayHomeAsUpEnabled(true);
            }

            this.BindProgressBar(this.ViewModel);
        }

        protected override void OnCreate(Bundle bundle)
        {
            this.RequestWindowFeature(WindowFeatures.IndeterminateProgress);

            base.OnCreate(bundle);
        }
    }
}

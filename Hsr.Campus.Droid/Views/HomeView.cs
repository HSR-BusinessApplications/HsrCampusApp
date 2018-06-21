// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Views
{
    using Android.App;
    using Android.OS;
    using Android.Preferences;
    using Android.Views;
    using Core.ViewModels;
    using MvvmCross.Platform;
    using Widgets;

    [Activity(Label = "@string/ApplicationTitle", Theme = "@style/Theme.View", Icon = "@drawable/ic_launcher")]
    public class HomeView : AbstractView<HomeViewModel>
    {
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_settings:
                    this.ViewModel.GoSettings.Execute(null);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.MenuInflater.Inflate(Resource.Menu.home, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.HomeView);

            this.ViewModel.VerifyTimetableSync(() =>
            {
                var pm = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
                if (pm.GetBoolean("integrate_calendar", false))
                {
                    Mvx.IocConstruct<AccountViewModel>().ShredData();
                }
            });

            this.ViewModel.VerifyFirstRun(this.Finish);
        }

        protected override void OnResume()
        {
            this.ViewModel.RaisePropertyChanged(nameof(this.ViewModel.IsAuth));
            base.OnResume();
        }
    }
}

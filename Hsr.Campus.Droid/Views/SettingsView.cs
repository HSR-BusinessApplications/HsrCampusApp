// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Views
{
    using System.Collections.Generic;
    using System.Linq;
    using Android.App;
    using Android.OS;
    using Android.Preferences;
    using Android.Runtime;
    using Android.Views;
    using Hsr.Campus.Core.Resources;
    using Hsr.Campus.Core.ViewModels;
    using Widgets;

    [Activity(Label = "@string/Settings", Theme = "@style/Theme.View", Icon = "@drawable/ic_launcher")]
    public class SettingsView : AbstractView<SettingsViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.SettingsView);

            int title = Resource.String.Settings;

            if (this.ViewModel.Title == AppResources.SettingAppInfoTitle)
        {
                title = Resource.String.SettingAppInfoTitle;
        }
            else if (this.ViewModel.Title == AppResources.SettingStorageTitle)
        {
                title = Resource.String.SettingStorageTitle;
        }
            else if (this.ViewModel.Title == AppResources.ThirdPartyLibraries)
        {
                title = Resource.String.ThirdPartyLibraries;
        }

            this.SetTitle(title);
        }
    }
}

﻿// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Views
{
    using Android.App;
    using Android.OS;
    using Android.Views;
    using Core.ViewModels;
    using Hsr.Campus.Core.Resources;
    using Widgets;
    using Widgets.ViewPager;

    [Activity(Label = "@string/TileSport", Theme = "@style/Theme.View", Icon = "@drawable/ic_launcher")]
    public class SportsView : AbstractView<SportsViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.SportsView);

            this.Title = AppResources.TileSport;
        }
    }
}

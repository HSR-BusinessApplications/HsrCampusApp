// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Views
{
    using Android.App;
    using Android.OS;
    using Core.ViewModels;
    using Widgets;
    using Widgets.ViewPager;

    [Activity(Label = "@string/TileExams", Theme = "@style/Theme.View", Icon = "@drawable/ic_launcher")]
    public class ExamView : TabViewPagerActivity<ExamViewModel, BaseItemView>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.ExamView);
        }
    }
}

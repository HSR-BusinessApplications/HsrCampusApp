// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Views
{
    using System.Threading.Tasks;
    using Android.App;
    using Android.OS;
    using Android.Views;
    using Hsr.Campus.Core.ViewModels;
    using Hsr.Campus.Droid.Widgets;

    [Activity(Label = "@string/TileTimetable", Theme = "@style/Theme.View", Icon = "@drawable/ic_launcher")]
    public class AdunisView
        : AbstractView<AdunisViewModel>
    {
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_refresh:
                    Task.Run(() => this.ViewModel.UpdateAsync(true));
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.MenuInflater.Inflate(Resource.Menu.updateable, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.AdunisView);
        }
    }
}

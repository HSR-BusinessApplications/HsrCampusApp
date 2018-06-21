// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Views.Menu
{
    using System;
    using Android.App;
    using Android.OS;
    using Android.Support.V4.View;
    using Android.Views;
    using Core.ViewModels;
    using Widgets.ViewPager;

    [Activity(Label = "@string/TileMenu", Theme = "@style/CenteredActionBarTheme", Icon = "@drawable/ic_launcher")]
    public class MenuView : TabViewPagerActivity<MenuViewModel, MenuFeedItemView>
    {
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_refresh:
                    this.ViewModel.UpdateCommand.Execute(null);
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

            this.SetContentView(Resource.Layout.MenuView);

            this.ViewPager.AddOnPageChangeListener(new PageChangeListener(this.ViewModel));

            this.ViewModel.OnPropertyChanged(() => this.ViewModel.Items, this.UpdateTitles);
        }

        private class PageChangeListener : ViewPager.SimpleOnPageChangeListener
        {
            private readonly MenuViewModel viewModel;

            public PageChangeListener(MenuViewModel viewModel)
            {
                this.viewModel = viewModel;
            }

            public override void OnPageSelected(int position)
            {
                this.viewModel.UpdateSelectedItem();
            }
        }
    }
}

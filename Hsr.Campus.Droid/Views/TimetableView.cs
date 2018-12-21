﻿// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Views
{
    using System;
    using System.Linq;
    using Android.App;
    using Android.OS;
    using Android.Views;
    using Core.ViewModels;
    using Widgets;
    using Widgets.ViewPager;

    [Activity(Label = "@string/TileTimetable", Theme = "@style/CenteredActionBarTheme", Icon = "@drawable/ic_launcher")]
    public class TimetableView : TabViewPagerActivity<TimetableViewModel, BaseItemView>
    {
        private IMenuItem menuWeekNr;

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_weekNr:
                    this.SelectWeek();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.MenuInflater.Inflate(Resource.Menu.timetable, menu);
            this.menuWeekNr = menu.FindItem(Resource.Id.menu_weekNr);

            this.menuWeekNr.SetTitle(this.ViewModel.CurrentWeek.ToString());
            return base.OnCreateOptionsMenu(menu);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.TimetableView);

            var day = this.ViewModel.AllDays.SingleOrDefault(
                    t => t.Date.HasValue && t.Date.Value.DayOfWeek == DateTime.Now.DayOfWeek);
            if (day != null)
            {
                this.ViewPager.SetCurrentItem(this.ViewModel.AllDays.IndexOf(day), true);
            }

            this.ViewModel.OnPropertyChanged(() => this.ViewModel.CurrentWeek, this.UpdateTitles);
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            this.ViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "CurrentWeek")
                {
                    this.menuWeekNr?.SetTitle(this.ViewModel.CurrentWeek.ToString());
                }
            };
        }

        private void SelectWeek()
        {
            var builder = new AlertDialog.Builder(this);

            AlertDialog dialog = null;

            dialog = builder.SetTitle(Resource.String.WeekSelection)
                .SetSingleChoiceItems(
                    this.ViewModel.Weeks.Select(t => t.ToString()).ToArray(),
                    this.ViewModel.Weeks.IndexOf(this.ViewModel.CurrentWeek),
                    (sender, args) =>
                    {
                        this.ViewModel.CurrentWeek = this.ViewModel.Weeks[args.Which];

                        dialog?.Dismiss();
                    })
                .Create();
            dialog.Show();
        }
    }
}

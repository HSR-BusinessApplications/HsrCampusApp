// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Widgets.ViewPager
{
    using System;
    using Android.App;
    using Android.Support.V4.View;
    using Android.Views;
    using Core.ViewModels;

    public abstract class TabViewPagerActivity<TVm, TItemView> : AbstractView<TVm>
        where TVm : AbstractViewModel
        where TItemView : BaseItemView
    {
        public BaseViewPager<TItemView> ViewPager => this.FindViewById<BaseViewPager<TItemView>>(Resource.Id.tabViewPagerView);

        public override void SetContentView(View view)
        {
            base.SetContentView(view);

            if (this.ViewPager == null)
            {
                return;
            }

            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            this.ViewPager.AddOnPageChangeListener(new PageChangeListener(this.ActionBar));

            var itemsSource = this.ViewPager.ItemsSource;
            if (itemsSource == null)
            {
                return;
            }

            foreach (var item in itemsSource)
            {
                var tab = this.ActionBar.NewTab();

                tab.SetText(ApplyTitle((ITitled)item));
                tab.TabSelected += (sender, args) => this.ViewPager.SetCurrentItem(tab.Position, true);

                this.ActionBar.AddTab(tab);
            }
        }

        protected void UpdateTitles()
        {
            var itemsSource = this.ViewPager.ItemsSource;
            if (itemsSource == null)
            {
                return;
            }

            this.ActionBar.RemoveAllTabs();

            foreach (var item in itemsSource)
            {
                var tab = this.ActionBar.NewTab();

                tab.SetText(ApplyTitle((ITitled)item));
                tab.TabSelected += (sender, args) => this.ViewPager.SetCurrentItem(tab.Position, true);

                this.ActionBar.AddTab(tab);
            }
        }

        private static string ApplyTitle(ITitled item)
        {
            var subItem = item as ISubTitled;

            return string.IsNullOrEmpty(subItem?.SubTitle)
                ? item.Title
                : $"{item.Title}{Environment.NewLine}{subItem.SubTitle}";
        }

        private class PageChangeListener : ViewPager.SimpleOnPageChangeListener
        {
            private readonly ActionBar actionBar;

            public PageChangeListener(ActionBar actionBar)
            {
                this.actionBar = actionBar;
            }

            public override void OnPageSelected(int position)
            {
                this.actionBar.SetSelectedNavigationItem(position);
            }
        }
    }
}

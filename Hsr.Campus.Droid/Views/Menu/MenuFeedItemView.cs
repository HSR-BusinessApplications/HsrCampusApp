// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Views.Menu
{
    using Android.Content;
    using Android.Support.V4.View;
    using Android.Views;
    using Core.ViewModels;
    using MvvmCross.Binding.Droid.Views;
    using Widgets;

    public class MenuFeedItemView : BaseItemView
    {
        private readonly MenuFeedViewModel viewModel;

        public MenuFeedItemView(Context context, IMvxLayoutInflaterHolder layoutInflaterHolder, object dataContext, ViewGroup parent, int templateId)
            : base(context, layoutInflaterHolder, dataContext, parent, templateId)
        {
            this.viewModel = dataContext as MenuFeedViewModel;
            if (this.viewModel == null)
            {
                return;
            }

            this.LoadCurrentItem();
            this.viewModel.MenuViewModel.UpdateSelectedItemAction += this.LoadCurrentItem;

            this.ViewPager.AddOnPageChangeListener(new PageChangeListener(this.viewModel));
        }

        private MenuWebViewPager ViewPager => this.Content.FindViewById<MenuWebViewPager>(Resource.Id.menuWebViewPager);

        public void LoadCurrentItem()
        {
            this.ViewPager.SetCurrentItem(this.ViewPager.IndexOfMenu(this.viewModel.FindItemClosestToSelectedDay()), false);
        }

        private class PageChangeListener : ViewPager.SimpleOnPageChangeListener
        {
            private readonly MenuFeedViewModel viewModel;

            public PageChangeListener(MenuFeedViewModel viewModel)
            {
                this.viewModel = viewModel;
            }

            public override void OnPageSelected(int position)
            {
                this.viewModel.MenuViewModel.SelectedDay = this.viewModel.Items[position].Date;
            }
        }
    }
}

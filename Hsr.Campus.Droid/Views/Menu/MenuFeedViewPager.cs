// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Views.Menu
{
    using Android.Content;
    using Android.Util;
    using Android.Views;
    using Widgets.ViewPager;

    public class MenuFeedViewPager : BaseViewPager<MenuFeedItemView>
    {
        public MenuFeedViewPager(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        public MenuFeedViewPager(Context context, IAttributeSet attrs, BindablePagerAdapter<MenuFeedItemView> adapter)
            : base(context, attrs, adapter)
        {
        }

        // With the return value "true" the ViewPager knows the content is scrollable
        // The Scroll-Event will never be processed in the ViewPager itself
        // If the end of the Child-Viewpager is reached it will not automatically switch between the feeds
        protected override bool CanScroll(View v, bool checkV, int dx, int x, int y)
        {
            return true;
        }
    }
}

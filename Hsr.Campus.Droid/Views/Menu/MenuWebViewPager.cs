// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Views.Menu
{
    using Android.Content;
    using Android.Util;
    using Android.Views;
    using Core.Model;
    using Widgets.ViewPager;

    public class MenuWebViewPager : BaseViewPager<MenuWebItemView>
    {
        public MenuWebViewPager(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        public MenuWebViewPager(Context context, IAttributeSet attrs, BindablePagerAdapter<MenuWebItemView> adapter)
            : base(context, attrs, adapter)
        {
        }

        public int IndexOfMenu(SqlMenu menu)
        {
            return this.Adapter.GetPosition(menu);
        }
    }
}

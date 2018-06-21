// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Widgets.ViewPager
{
    using Android.Content;
    using Android.Util;

    public sealed class BindableViewPager : BaseViewPager<BaseItemView>
    {
        public BindableViewPager(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        public BindableViewPager(Context context, IAttributeSet attrs, BindablePagerAdapter<BaseItemView> adapter)
            : base(context, attrs, adapter)
        {
        }
    }
}

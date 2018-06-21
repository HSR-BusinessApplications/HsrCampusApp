// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Widgets
{
    using Android.Content;
    using Android.Runtime;
    using Android.Util;
    using Android.Widget;

    public class ScaledImageView : ImageView
    {
        [Register(".ctor", "(Landroid/content/Context;)V", "")]
        public ScaledImageView(Context context)
            : base(context)
        {
            this.Setup();
        }

        [Register(".ctor", "(Landroid/content/Context;Landroid/util/AttributeSet;)V", "")]
        public ScaledImageView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            this.Setup();
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);

            Log.Debug("ScaledImageView", "OnMeasure begin");

            if (this.Drawable == null || this.Drawable.IntrinsicWidth <= 1)
            {
                this.SetImageDrawable(null);
                this.SetMeasuredDimension(0, 0);
            }
            else
            {
                var i = MeasureSpec.GetSize(widthMeasureSpec) / ((double)this.Drawable.IntrinsicWidth);
                var imageHeight = i * this.Drawable.IntrinsicHeight;
                this.SetMeasuredDimension(widthMeasureSpec, ResolveSizeAndState((int)imageHeight, heightMeasureSpec, 0));
            }

            Log.Debug("ScaledImageView", "OnMeasure End");
        }

        private void Setup()
        {
            this.SetScaleType(ScaleType.FitCenter);

            this.SetAdjustViewBounds(true);
        }
    }
}

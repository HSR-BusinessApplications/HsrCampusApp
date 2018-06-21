// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Widgets
{
    using Android.Content;
    using Android.Runtime;
    using Android.Util;
    using Android.Widget;

    public class WebButtonView
        : Button
    {
        [Register(".ctor", "(Landroid/content/Context;)V", "")]
        public WebButtonView(Context context)
            : base(context)
        {
            this.Init(context);
        }

        [Register(".ctor", "(Landroid/content/Context;Landroid/util/AttributeSet;)V", "")]
        public WebButtonView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            this.Init(context);
        }

        public string Href { get; set; }

        private void Init(Context context)
        {
            this.Click += (s, e) =>
            {
                if (string.IsNullOrEmpty(this.Href))
                {
                    return;
                }

                var intent = new Intent();
                intent.SetAction(Intent.ActionView);
                intent.AddCategory(Intent.CategoryBrowsable);
                intent.SetData(Android.Net.Uri.Parse(this.Href));
                context.StartActivity(intent);
            };
        }
    }
}

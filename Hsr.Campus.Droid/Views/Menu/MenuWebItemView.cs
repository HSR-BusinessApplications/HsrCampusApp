// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Views.Menu
{
    using Android.Content;
    using Android.Views;
    using Android.Webkit;
    using Core.Model;
    using MvvmCross.Binding.Droid.Views;
    using Widgets;

    public class MenuWebItemView : BaseItemView
    {
        public MenuWebItemView(Context context, IMvxLayoutInflaterHolder layoutInflaterHolder, object dataContext, ViewGroup parent, int templateId)
            : base(context, layoutInflaterHolder, dataContext, parent, templateId)
        {
            var webView = this.Content.FindViewById<WebView>(Resource.Id.webView1);

            this.Menu = dataContext as SqlMenu;
            if (this.Menu != null)
            {
                webView.LoadData(this.Menu.HtmlPage, "text/html", null);
            }
        }

        public SqlMenu Menu { get; }
    }
}

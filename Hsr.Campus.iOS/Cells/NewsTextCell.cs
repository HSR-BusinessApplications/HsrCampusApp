// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.Globalization;
    using Hsr.Campus.Core.Model;
    using MvvmCross.Binding.BindingContext;
    using MvvmCross.Binding.iOS.Views;
    using UIKit;

    internal partial class NewsTextCell : MvxTableViewCell
    {
        public const string Identifier = "NewsTextCell";

        public NewsTextCell(IntPtr handle)
            : base(handle)
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<NewsTextCell, SqlNews>();

                set.Bind(this.TitleLabel).To(t => t.Title);
                set.Bind(this.MessageText).For(t => t.Text).To(t => t.Message);
                set.Bind(this.MessageText).For(t => t.Hidden).To(t => t.Message).WithConversion("IsEmpty");
                set.Bind(this.Date).For(t => t.Text).To(t => t.Date).WithConversion("LocalTime");

                set.Apply();
            });
        }

        public static nfloat HeightFor(SqlNews item)
        {
            return item.Date.ToString(CultureInfo.CurrentCulture).StringHeight(UIFont.SystemFontOfSize(11), 280)
                + item.Title.StringHeight(UIFont.SystemFontOfSize(18), 280)
                + item.Message.StringHeight(UIFont.SystemFontOfSize(12), 280) + 24;
        }

        public void MarkAsDirty(SqlNews news)
        {
            this.MessageText.Text = news.Message;
            this.SetNeedsDisplay();
            this.ReloadInputViews();
        }
    }
}

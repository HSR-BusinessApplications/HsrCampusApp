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

    internal partial class NewsTitleCell : MvxTableViewCell
    {
        public const string Identifier = "NewsTitleCell";

        public NewsTitleCell(IntPtr handle)
            : base(handle)
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<NewsTitleCell, SqlNews>();

                set.Bind(this.TitleLabel).To(t => t.Title);
                set.Bind(this.Date).For(t => t.Text).To(t => t.Date).WithConversion("LocalTime");

                set.Apply();
            });
        }

        public static nfloat HeightFor(SqlNews item) => item.Date.ToString(CultureInfo.CurrentCulture).StringHeight(UIFont.SystemFontOfSize(11), 280)
                + item.Title.StringHeight(UIFont.SystemFontOfSize(18), 280) + 24;
    }
}

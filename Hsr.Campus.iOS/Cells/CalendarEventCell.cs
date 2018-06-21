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

    [CellDefinition("CalendarEventCell", 167)]
    internal partial class CalendarEventCell : MvxTableViewCell
    {
        public CalendarEventCell(IntPtr handle)
            : base(handle)
        {
            this.SelectionStyle = UITableViewCellSelectionStyle.None;

            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<CalendarEventCell, SqlCalendarEvent>();

                set.Bind(this.TitleLabel).To(t => t.Summary);
                set.Bind(this.TitleLabel).For(t => t.Hidden).To(t => t.Summary).WithConversion("IsEmpty");
                set.Bind(this.LocationLabel).To(t => t.Location);
                set.Bind(this.LocationLabel).For(t => t.Hidden).To(t => t.Location).WithConversion("IsEmpty");
                set.Bind(this.TimeLabel).To(t => t.Start);
                set.Bind(this.DescriptionLabel).To(t => t.Description);
                set.Bind(this.DescriptionLabel).For(t => t.Hidden).To(t => t.Description).WithConversion("IsEmpty");

                set.Apply();
            });
        }

        public static nfloat HeightFor(SqlCalendarEvent item)
        {
            var total = (nfloat)0.0;

            if (!string.IsNullOrEmpty(item.Summary))
            {
                total += item.Summary.StringHeight(UIFont.SystemFontOfSize(17), 280) + 20;
            }

            if (!string.IsNullOrEmpty(item.Location))
            {
                total += item.Location.StringHeight(UIFont.SystemFontOfSize(12), 280) + 20;
            }

            total += item.Start.ToString(CultureInfo.CurrentCulture).StringHeight(UIFont.SystemFontOfSize(12), 280) + 20;

            if (!string.IsNullOrEmpty(item.Description))
            {
                total += item.Description.StringHeight(UIFont.SystemFontOfSize(12), 280) + 20;
            }

            return total;
        }
    }
}

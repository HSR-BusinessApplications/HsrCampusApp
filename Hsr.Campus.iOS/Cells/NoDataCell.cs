// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using Foundation;
    using Hsr.Campus.Core.Resources;
    using MvvmCross.Binding.BindingContext;
    using MvvmCross.Binding.iOS.Views;
    using UIKit;

    public partial class NoDataCell : MvxTableViewCell
    {
        public static readonly UINib Nib = UINib.FromName("NoDataCell", NSBundle.MainBundle);
        public static readonly NSString Key = new NSString("NoDataCell");

        public NoDataCell(IntPtr handle)
            : base(handle)
        {
            this.SelectionStyle = UITableViewCellSelectionStyle.None;

            this.DelayBind(() => this.TitleLabel.Text = AppResources.NoData);
        }

        public static NoDataCell Create() => (NoDataCell)Nib.Instantiate(null, null)[0];
    }
}

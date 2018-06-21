// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using Core.ViewModels;
    using MvvmCross.Binding.BindingContext;
    using UIKit;

    [CellDefinition("SettingsCell", 70)]
    internal partial class SettingsCell : DefaultImageStyleTableViewCell
    {
        public SettingsCell(IntPtr handle)
            : base(handle)
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<SettingsCell, Setting>();

                set.Bind(this.TitleLabel).To(t => t.Title);
                set.Bind(this.SubtitleLabel).To(t => t.SubTitle);
                set.Bind(this.ImageView).For(t => t.Image).To(t => t.Image).WithConversion("BundleIcon");

                set.Apply();
            });

            this.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
        }

        public static nfloat HeightFor(Setting item)
        {
            return item.Title.StringHeight(UIFont.SystemFontOfSize(18), 280)
                + item.SubTitle.StringHeight(UIFont.SystemFontOfSize(12), 280) + 20;
        }
    }
}

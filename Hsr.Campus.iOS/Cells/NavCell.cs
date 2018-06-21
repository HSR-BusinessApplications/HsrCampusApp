// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using Core.ViewModels;
    using MvvmCross.Binding.BindingContext;

    [CellDefinition("NavCell", 44)]
    internal partial class NavCell : DefaultImageStyleTableViewCell
    {
        public NavCell(IntPtr handle)
            : base(handle)
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<NavCell, NavItem>();

                set.Bind(this.TitleLabel).To(t => t.Title);
                set.Bind(this.ImageView).To(t => t.IconPath).WithConversion("BundleIcon");
                set.Apply();
            });
        }
    }
}

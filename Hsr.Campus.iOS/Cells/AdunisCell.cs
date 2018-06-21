// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using Core.Model;
    using MvvmCross.Binding.BindingContext;

    [CellDefinition("AdunisCell", 50)]
    internal partial class AdunisCell : DefaultImageStyleTableViewCell
    {
        public AdunisCell(IntPtr handle)
            : base(handle)
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<AdunisCell, SqlTimeperiod>();

                set.Bind(this.TitleLabel).To(t => t.Name);
                set.Bind(this.SubTitleLabel).To(t => t).WithConversion("Timeperiod");
                set.Bind(this.ImageView).For(t => t.Image).To(t => t.Type).WithConversion("AdunisIcon");

                set.Apply();
            });
        }
    }
}

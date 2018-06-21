// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using Core.Model;
    using MvvmCross.Binding.BindingContext;

    [CellDefinition("FilerCell", 44)]
    internal partial class FilerCell : DefaultImageStyleTableViewCell
    {
        public FilerCell(IntPtr handle)
            : base(handle)
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<FilerCell, IOListing>();

                set.Bind(this.TitleLabel).To(t => t.Name);
                set.Bind(this.TitleLabel).For(t => t.TextColor).To(t => t.IsLocal).WithConversion("FilerLocal");
                set.Bind(this.ImageView).To(t => t).WithConversion("FilerIcon");

                set.Apply();
            });
        }
    }
}

// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using Hsr.Campus.Core.Model;
    using MvvmCross.Binding.BindingContext;
    using MvvmCross.Binding.iOS.Views;
    using UIKit;

    [CellDefinition("TimetableCell", 50)]
    internal partial class TimetableCell : MvxTableViewCell
    {
        public TimetableCell(IntPtr handle)
            : base(handle)
        {
            this.SelectionStyle = UITableViewCellSelectionStyle.None;
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<TimetableCell, SqlAdunisAppointment>();

                set.Bind(this.TitleLabel).To(t => t.CourseName);
                set.Bind(this.ProfLabel).To(t => t.Lecturers);
                set.Bind(this.RoomLabel).To(t => t.AppointmentRooms);

                set.Apply();
            });
        }
    }
}

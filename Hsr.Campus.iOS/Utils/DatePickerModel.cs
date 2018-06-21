// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS.Utils
{
    using System;
    using System.Collections.Generic;
    using Core.Model;
    using UIKit;

    public class DatePickerModel : UIPickerViewModel
    {
        private readonly IList<SqlMenu> values;

        public DatePickerModel(IList<SqlMenu> values)
        {
            this.values = values;
        }

        public event EventHandler<DatePickerChangedEventArgs> PickerChanged;

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return this.values.Count;
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return this.values[(int)row].Title;
        }

        public override nfloat GetRowHeight(UIPickerView pickerView, nint component)
        {
            return 40f;
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            this.PickerChanged?.Invoke(this, new DatePickerChangedEventArgs { SelectedValue = this.values[(int)row] });
        }
    }
}

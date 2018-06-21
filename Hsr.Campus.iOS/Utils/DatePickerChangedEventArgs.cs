// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS.Utils
{
    using System;
    using Core.Model;

    public class DatePickerChangedEventArgs : EventArgs
    {
        public SqlMenu SelectedValue { get; set; }
    }
}

// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using Hsr.Campus.Core.Model;
    using MvvmCross.Platform.Converters;

    public class AppointmentTimeConverter
        : MvxValueConverter<SqlAdunisAppointment>
    {
        protected override object Convert(SqlAdunisAppointment value, Type targetType, object parameter, System.Globalization.CultureInfo culture) => $"{value.StartTime:t} - {value.EndTime:t}";
    }
}

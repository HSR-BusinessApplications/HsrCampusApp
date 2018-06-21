// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Converter
{
    using System;
    using System.Globalization;
    using MvvmCross.Platform.Converters;

    public class AppointmentDayConverter
        : MvxValueConverter<DateTime>
    {
        protected override object Convert(DateTime value, Type targetType, object parameter, CultureInfo culture) => value.DayOfWeek != DayOfWeek.Sunday;
    }
}

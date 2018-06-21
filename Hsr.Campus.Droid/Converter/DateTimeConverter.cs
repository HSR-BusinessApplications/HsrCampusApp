// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Converter
{
    using System;
    using System.Globalization;
    using MvvmCross.Platform.Converters;

    public class DateTimeConverter
        : MvxValueConverter<DateTime, string>
    {
        protected override string Convert(DateTime value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string formatValue)
            {
                return value.ToString(formatValue);
            }

            return value.ToString();
        }

        protected override DateTime ConvertBack(string value, Type targetType, object parameter, CultureInfo culture) => DateTime.Parse(value);
    }
}

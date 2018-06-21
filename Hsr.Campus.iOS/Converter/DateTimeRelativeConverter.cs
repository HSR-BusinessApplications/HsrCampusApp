// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.Globalization;
    using Core.Resources;
    using MvvmCross.Platform.Converters;

    public class DateTimeRelativeConverter : MvxValueConverter<DateTime>
    {
        protected override object Convert(DateTime value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = value;
            var timeSpan = DateTime.Now - date;
            var output = string.Empty;

            if (timeSpan.Days > 0)
            {
                return string.Format(AppResources.DateTimeRelativeDays, date);
            }

            if (timeSpan.Hours > 0)
            {
                return string.Format(new PluralFormatProvider(), AppResources.DateTimeRelativeHours, timeSpan.Hours);
            }

            if (timeSpan.Minutes > 0)
            {
                return string.Format(new PluralFormatProvider(), AppResources.DateTimeRelativeMinutes, timeSpan.Minutes);
            }

            return timeSpan.Seconds < 30 ? AppResources.DateTimeRealtiveNow : string.Format(new PluralFormatProvider(), AppResources.DateTimeRealtiveSeconds, timeSpan.Seconds);
        }

        public class PluralFormatProvider : IFormatProvider, ICustomFormatter
        {
            public object GetFormat(Type formatType) => this;

            public string Format(string format, object arg, IFormatProvider formatProvider)
            {
                var forms = format.Split(';');
                var value = (int)arg;
                var form = value == 1 ? 0 : 1;
                return "{0} {1}".FormatWith(value, forms[form]);
            }
        }
    }
}

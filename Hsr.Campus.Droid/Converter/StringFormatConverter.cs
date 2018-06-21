// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Converter
{
    using System;
    using System.Globalization;
    using System.Threading;
    using MvvmCross.Platform.Converters;

    public class StringFormatConverter
        : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter == null ? value : string.Format(Thread.CurrentThread.CurrentUICulture, (string)parameter, value);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

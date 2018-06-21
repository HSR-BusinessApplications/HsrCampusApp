// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using Core.Model;
    using MvvmCross.Platform.Converters;

    public class TimeperiodConverter
        : MvxValueConverter<SqlTimeperiod>
    {
        protected override object Convert(SqlTimeperiod value, Type targetType, object parameter, System.Globalization.CultureInfo culture) => $"{value.Begin:d} - {value.End:d}";
    }
}

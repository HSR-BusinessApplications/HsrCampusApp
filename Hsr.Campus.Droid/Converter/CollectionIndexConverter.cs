// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Converter
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using Hsr.Campus.Core.Model;
    using MvvmCross.Platform.Converters;

    public class CollectionIndexConverter
        : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            var index = parameter == null ? default(int) : (int)((long)parameter);

            var collection = (ObservableCollection<SqlAdunisAppointment>[])value;

            return collection[index];
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

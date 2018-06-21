// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Converter
{
    using System;
    using System.Globalization;
    using MvvmCross.Platform.Converters;

    public class AdunisIconConverter
        : MvxValueConverter<int>
    {
        protected override object Convert(int value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case 2:
                    return Resource.Drawable.ic_adunis_timetable;
                case 4:
                    return Resource.Drawable.ic_adunis_exam;
                default:
                    return null;
            }
        }
    }
}

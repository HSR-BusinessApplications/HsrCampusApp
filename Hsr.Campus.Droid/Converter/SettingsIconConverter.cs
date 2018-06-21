// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Converter
{
    using System;
    using System.Globalization;
    using MvvmCross.Platform.Converters;

    public class SettingsIconConverter
        : MvxValueConverter<string>
    {
        protected override object Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case "InfoAccount.png":
                    return Resource.Drawable.ic_act_account;
                case "InfoStorage.png":
                    return Resource.Drawable.ic_act_storage;
                case "InfoHeart.png":
                    return Resource.Drawable.ic_act_heart;
                default:
                    return null;
            }
        }
    }
}

// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.Globalization;
    using Hsr.Campus.Core.Model;
    using Hsr.Campus.Core.Resources;
    using MvvmCross.Platform.Converters;

    public class BalanceUpdateConverter
    : MvxValueConverter<AccountBalance>
    {
        protected override object Convert(AccountBalance value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? null : $"{AppResources.LastUpdate} {new DateTimeRelativeConverter().Convert(value.LastUpdate, typeof(string), null, CultureInfo.CurrentCulture)}";
        }
    }
}

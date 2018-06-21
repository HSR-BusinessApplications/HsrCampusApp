// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.Globalization;
    using MvvmCross.Platform.Converters;
    using UIKit;

    public class FilerLocalConverter
        : MvxValueConverter<bool>
    {
        protected override object Convert(bool value, Type targetType, object parameter, CultureInfo culture) => value ? Constants.HsrBlue : new UIColor((nfloat)0.0, (nfloat)0, (nfloat)0, (nfloat)1.0);
    }
}

// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using Foundation;
    using MvvmCross.Platform.Converters;
    using SpriteKit;
    using UIKit;

    public class BundleIconConverter
        : MvxValueConverter<string>
    {
        protected override object Convert(string value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.IsNullOrEmpty(value) ? null : UIImage.FromBundle(value);
        }
    }
}

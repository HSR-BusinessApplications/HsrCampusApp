﻿// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using Hsr.Campus.Core.Model;
    using MvvmCross.Platform.Converters;
    using UIKit;

    public class AdunisIconConverter
        : MvxValueConverter<AdunisType>
    {
        protected override object Convert(AdunisType value, Type targetType, object parameter, System.Globalization.CultureInfo culture) => UIImage.FromBundle($"{value}.png");
    }
}

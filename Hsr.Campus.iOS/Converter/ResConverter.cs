// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using Hsr.Campus.Core.ApplicationServices;
    using MvvmCross.Platform;
    using MvvmCross.Platform.Converters;

    public class ResConverter
        : MvxValueConverter<string>
    {
        private readonly IIOCacheService cacheService;

        public ResConverter()
        {
            this.cacheService = Mvx.Resolve<IIOCacheService>();
        }

        public ResConverter(IIOCacheService cacheService)
        {
            this.cacheService = cacheService;
        }

        protected override object Convert(string value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return this.cacheService.Map(value).Replace("file://", "res://");
        }
    }
}

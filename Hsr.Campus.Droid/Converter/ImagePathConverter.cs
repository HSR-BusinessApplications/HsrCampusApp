// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.Converter
{
    using System;
    using System.Globalization;
    using Android.Graphics;
    using Hsr.Campus.Core.ApplicationServices;
    using MvvmCross.Platform;
    using MvvmCross.Platform.Converters;

    public class ImagePathConverter
        : MvxValueConverter<string>
    {
        private readonly IIOCacheService cache;

        public ImagePathConverter()
        {
            this.cache = Mvx.Resolve<IIOCacheService>();
        }

        protected override object Convert(string value, Type targetType, object parameter, CultureInfo culture) => value == null ? Bitmap.CreateBitmap(1, 1, Bitmap.Config.Argb8888) : BitmapFactory.DecodeFile(this.cache.Map(value));
    }
}

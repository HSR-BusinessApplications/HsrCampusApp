// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using Core.DomainServices;
    using MvvmCross.Platform;
    using MvvmCross.Platform.Converters;

    public class FbImageConverter
        : MvxValueConverter<string>
    {
        private readonly INewsWebHandler newsWebHandler;

        public FbImageConverter()
        {
            this.newsWebHandler = Mvx.Resolve<INewsWebHandler>();
        }

        public FbImageConverter(INewsWebHandler newsWebHandler)
        {
            this.newsWebHandler = newsWebHandler;
        }

        protected override object Convert(string value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.IsNullOrEmpty(value) ? null : this.newsWebHandler.PictureUrl(value);
        }
    }
}

// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System.IO;
    using Core.ApplicationServices;
    using MvvmCross.Platform;
    using UIKit;

    public class ImageLoader
    {
        private readonly IIOCacheService cacheService;

        public ImageLoader(IIOCacheService cacheService)
        {
            this.cacheService = cacheService;
        }

        public UIImage LoadImage(string path)
        {
            if (path == null)
            {
                return null;
            }

            var file = this.cacheService.Map(path);
            var img = UIImage.FromFile(file);

            if (img == null)
            {
                Mvx.Trace("file: {0}", file);
                Mvx.Trace("exists: {0}", File.Exists(file));
            }

            return img;
        }
    }
}

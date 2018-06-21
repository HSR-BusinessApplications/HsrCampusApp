// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.IO;
    using CoreGraphics;
    using Hsr.Campus.Core.Model;
    using MvvmCross.Platform.Converters;
    using UIKit;

    public class FilerIconConverter
        : MvxValueConverter<IOListing>
    {
        protected override object Convert(IOListing value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            UIImage icon;

            if (value.IsDirectory)
            {
                icon = UIImage.FromBundle("IO/_folder.png");
            }
            else
            {
                var ext = Path.GetExtension(value.FullPath)?.Replace(".", string.Empty).ToLower();

                icon = UIImage.FromBundle($"IO/{ext}.png") ?? UIImage.FromBundle("IO/_blank.png");
            }

            return ResizeFilerIcon(icon);
        }

        /// <summary>
        /// Scales the icon to the requestes size and centers it
        /// </summary>
        /// <param name="icon">Icon which shall be customized</param>
        /// <returns>The customized icon</returns>
        private static UIImage ResizeFilerIcon(UIImage icon)
        {
            if (icon == null)
            {
                return null;
            }

            const float desiredSize = 40;

            var imageSize = icon.Size;

            var scale = Math.Min(desiredSize / imageSize.Width, desiredSize / imageSize.Height);
            var imageScaledSize = new CGSize(imageSize.Width * scale, imageSize.Height * scale);
            var imageLocation = new CGPoint((desiredSize - imageScaledSize.Width) / 2, (desiredSize - imageScaledSize.Height) / 2);

            UIGraphics.BeginImageContextWithOptions(new CGSize(desiredSize, desiredSize), false, UIScreen.MainScreen.Scale);

            icon.Draw(new CGRect(imageLocation, imageScaledSize));

            icon = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            return icon;
        }
    }
}

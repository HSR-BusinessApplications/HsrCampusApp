// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using Core.Model;
    using Foundation;
    using MvvmCross.Binding.BindingContext;
    using MvvmCross.Binding.iOS.Views;
    using MvvmCross.Platform;
    using MvvmCross.Plugins.DownloadCache;
    using UIKit;

    internal partial class NewsImageCell : MvxTableViewCell
    {
        public const string Identifier = "NewsImageCell";
        public static readonly nfloat Height = 182;

        private readonly MvxImageViewLoader imageViewLoader;

        public NewsImageCell(IntPtr handle)
            : base(handle)
        {
            Mvx.RegisterType<IMvxHttpFileDownloader, MvxNativeHttpFileDownloader>();

            var imageName = UIScreen.MainScreen.Scale <= 2
                ? (UIScreen.MainScreen.Scale <= 1 ? "LogoSmallBottom@1x" : "LogoSmallBottom@2x")
                : "LogoSmallBottom@3x";

            this.imageViewLoader = new MvxImageViewLoader(() => this.Image, () => Mvx.Trace("Image changed"))
            {
                DefaultImagePath = NSBundle.MainBundle.PathForResource(imageName, "png"),
                ErrorImagePath = NSBundle.MainBundle.PathForResource(imageName, "png")
            };

            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<NewsImageCell, SqlNews>();

                set.Bind(this.imageViewLoader).For(t => t.ImageUrl).To(t => t.NewsId).WithConversion("FbImage");
                set.Bind(this.Date).For(t => t.Text).To(t => t.Date).WithConversion("LocalTime");

                set.Apply();
            });
        }
    }
}

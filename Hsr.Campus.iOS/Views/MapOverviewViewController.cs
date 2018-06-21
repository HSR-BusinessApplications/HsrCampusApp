// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.Diagnostics;
    using Core.Resources;
    using Core.ViewModels;
    using CoreGraphics;
    using MvvmCross.iOS.Views;
    using MvvmCross.iOS.Views.Presenters.Attributes;
    using MvvmCross.Platform;
    using UIKit;

    [MvxFromStoryboard("Map")]
    [CompositeView]
    [MvxChildPresentation]
    internal partial class MapOverviewViewController : MvxViewController<MapViewModel>
    {
        public MapOverviewViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.ViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(this.ViewModel.CurrentMap))
                {
                    this.UpdateImageFrame();
                }
            };

            this.UpdateImageFrame();

            var tap = new UITapGestureRecognizer(this.MapImageTabHandler) { NumberOfTapsRequired = 1 };
            this.MapContainer.AddGestureRecognizer(tap);

            this.NavigationItem.Title = string.IsNullOrEmpty(this.ViewModel.Title) ? AppResources.TileCampusMap : this.ViewModel.Title;
        }

        private void UpdateImageFrame()
        {
            if (this.ViewModel.CurrentMap == null)
            {
                return;
            }

            var image = Mvx.IocConstruct<ImageLoader>().LoadImage(this.ViewModel.CurrentMap.ImagePath);

            if (image == null)
            {
                return;
            }

            var containerFrameSize = this.MapContainer.Frame.Size;
            var imageSize = image.Size;

            var scale = Math.Min(containerFrameSize.Width / imageSize.Width, containerFrameSize.Height / imageSize.Height);
            var imageFrameSize = new CGSize(imageSize.Width * scale, imageSize.Height * scale);
            var imageFramePosition = new CGPoint((containerFrameSize.Width - imageFrameSize.Width) / 2, (containerFrameSize.Height - imageFrameSize.Height) / 2);

            this.MapImage.Frame = new CGRect(imageFramePosition, imageFrameSize);

            this.MapImage.Image = image;
        }

        private void MapImageTabHandler(UITapGestureRecognizer e)
        {
            var location = e.LocationInView(this.MapImage);
            var xFactor = location.X / this.MapImage.Frame.Size.Width;
            var yFactor = location.Y / this.MapImage.Frame.Size.Height;
            Mvx.Trace("X:{0}, X1:{1}, Y:{2}, Y1:{3}", location.X, this.MapImage.Image.Size.Width * xFactor, location.Y, this.MapImage.Image.Size.Height * yFactor);

            if (this.MapImage.Image != null)
            {
                this.ViewModel.Click(new MapViewModel.Point(this.MapImage.Image.Size.Width * xFactor, this.MapImage.Image.Size.Height * yFactor));
            }
        }
    }
}

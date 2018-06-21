// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using CoreGraphics;
    using MvvmCross.Binding.iOS.Views;
    using UIKit;

    internal class DefaultImageStyleTableViewCell : MvxTableViewCell
    {
        public DefaultImageStyleTableViewCell(IntPtr handle)
            : base(handle)
        {
        }

        /// <summary>
        /// Changes the display frame for the icon to the requested size and centers the icon
        /// The icon should not be sacled. That is why it is important that the resource files should not be bigger then the value which is defined here (in pixels)
        /// This function ensures that there is a uniform picture in the table view with icons
        /// </summary>
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            if (this.ImageView.Image == null)
            {
                return;
            }

            const float desiredSize = 40;
            const float textStartPos = 72;

            var frameLocation = this.ImageView.Frame.Location + new CGSize(0, (this.ImageView.Frame.Height - desiredSize) / 2);
            var textWidth = this.Frame.Width - textStartPos;

            this.ImageView.Frame = new CGRect(frameLocation, new CGSize(desiredSize, desiredSize));
            this.TextLabel.Frame = new CGRect(textStartPos, this.TextLabel.Frame.Location.Y, textWidth, this.TextLabel.Frame.Size.Height);

            if (this.DetailTextLabel != null)
            {
                this.DetailTextLabel.Frame = new CGRect(textStartPos, this.DetailTextLabel.Frame.Location.Y, textWidth, this.DetailTextLabel.Frame.Size.Height);
            }

            this.SeparatorInset = new UIEdgeInsets(0, 68, 0, 4);
            this.ImageView.ContentMode = UIViewContentMode.Center;
        }
    }
}

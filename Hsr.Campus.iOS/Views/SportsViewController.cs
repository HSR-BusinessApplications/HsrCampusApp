// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.Linq;
    using Hsr.Campus.Core.Resources;
    using Hsr.Campus.Core.ViewModels;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.iOS.Views;
    using MvvmCross.iOS.Views.Presenters.Attributes;
    using UIKit;

    [MvxFromStoryboard("Events")]
    [MvxChildPresentation]
    internal partial class SportsViewController : MvxTableViewController<SportsViewModel>
    {
        private int tabCount = 0;

        public SportsViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (this.ViewModel == null)
            {
                return;
            }

            this.NavigationItem.Title = AppResources.TileSport;
        }
    }
}

// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using Core.Resources;
    using Core.ViewModels;
    using MvvmCross.iOS.Views;
    using MvvmCross.iOS.Views.Presenters.Attributes;

    [MvxFromStoryboard("Map")]
    [MvxChildPresentation]
    internal partial class MapViewController : MvxCompositeViewController<MapViewModel>
    {
        public MapViewController(IntPtr handle)
            : base(handle)
        {
            this.AddChildViewController<MapOverviewViewController>(t => t.Id == null);
            this.AddChildViewController<MapDetailViewController>(t => t.Id != null);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.NavigationItem.Title = AppResources.TileCampusMap;

            this.SetRightBarItem(this.ViewModel.UpdateCommand);

            this.RefreshChildView();
        }
    }
}

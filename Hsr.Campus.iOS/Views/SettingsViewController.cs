// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.
using UIKit;

namespace Hsr.Campus.iOS
{
    using System;
    using Hsr.Campus.Core.Resources;
    using Hsr.Campus.Core.ViewModels;
    using Hsr.Campus.iOS.Utils;
    using MvvmCross.Binding.BindingContext;
    using MvvmCross.iOS.Views;
    using MvvmCross.iOS.Views.Presenters.Attributes;

    [MvxFromStoryboard("Settings")]
    [WrapWith(WrapWithController.NavigationController)]
    [MvxChildPresentation]
    internal partial class SettingsViewController : MvxTableViewController<SettingsViewModel>
    {
        public SettingsViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var source = new SingleCellTableSource<SettingsCell>(this.TableView);

            this.CreateBinding(source)
                .To<SettingsViewModel>(vm => vm.Settings)
                .Apply();

            this.CreateBinding(source)
                .For(t => t.SelectionChangedCommand)
                .To<SettingsViewModel>(t => t.ShowDetailCommand)
                .Apply();

            this.TableView.TrimEmptyCells();
            this.TableView.Source = source;
            this.TableView.ReloadData();

            if (string.IsNullOrEmpty(this.ViewModel.Title))
            {
                this.NavigationItem.Title = AppResources.Settings;
            }
            else
            {
                this.NavigationItem.Title = this.ViewModel.Title;
            }
        }
    }
}

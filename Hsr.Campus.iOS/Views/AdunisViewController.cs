// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using Hsr.Campus.Core.Resources;
    using Hsr.Campus.Core.ViewModels;
    using Hsr.Campus.iOS.Utils;
    using MvvmCross.Binding.BindingContext;
    using MvvmCross.iOS.Views;
    using MvvmCross.iOS.Views.Presenters.Attributes;

    [MvxFromStoryboard("Timetable")]
    [MvxChildPresentation]
    internal partial class AdunisViewController : MvxTableViewController<AdunisViewModel>
    {
        public AdunisViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var source = new SingleCellTableSource<AdunisCell>(this.TableView);

            this.CreateBinding(source)
                .To<AdunisViewModel>(vm => vm.OwnPeriods)
                .Apply();

            this.CreateBinding(source)
                .For(t => t.SelectionChangedCommand)
                .To<AdunisViewModel>(t => t.ShowPeriodCommand)
                .Apply();

            this.CreateBinding(source)
                .For(t => t.HasContent)
                .To<AbstractViewModel>(t => t.HasContent)
                .Apply();

            this.TableView.TrimEmptyCells();
            this.TableView.Source = source;
            this.TableView.ReloadData();

            this.NavigationItem.Title = AppResources.TileTimetable;

            this.SetRightBarItem(this.ViewModel.UpdateCommand);
        }
    }
}

// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.Collections.ObjectModel;
    using Core.Model;
    using Core.ViewModels;
    using Foundation;
    using MvvmCross.Binding.BindingContext;
    using MvvmCross.iOS.Views;
    using MvvmCross.iOS.Views.Presenters.Attributes;
    using UIKit;
    using Utils;

    [MvxFromStoryboard("Timetable")]
    [MvxChildPresentation]
    internal partial class TimetableEntriesViewController : MvxTableViewController<TimetableEntriesViewModel>
    {
        public TimetableEntriesViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SingleCellTableSource<TimetableCell> source;

            if (!this.ViewModel.Date.HasValue)
            {
                source = new SingleCellTableSource<TimetableCell>(this.TableView)
                {
                    ItemsSource = this.ViewModel.Entries[0]
                }; // Div
            }
            else
            {
                source = new TableSource(this.TableView); // Mo-Fr

                this.CreateBinding(source)
                    .To<TimetableEntriesViewModel>(vm => vm.Entries)
                    .Apply();
            }

            this.TableView.TrimEmptyCells();
            this.TableView.Source = source;
            this.TableView.ReloadData();
        }

        public class TableSource : SingleCellTableSource<TimetableCell>
        {
            public TableSource(UITableView tableView)
                : base(tableView)
            {
            }

            public override nint NumberOfSections(UITableView tableView) => SqlAdunisAppointment.Rows.Length;

            public override nint RowsInSection(UITableView tableview, nint section) => (this.ItemsSource as ObservableCollection<SqlAdunisAppointment>[])?[(int)section]?.Count ?? 0;

            public override string TitleForHeader(UITableView tableView, nint section) => (this.ItemsSource as ObservableCollection<SqlAdunisAppointment>[])?[(int)section] == null ? null : SqlAdunisAppointment.Rows[(int)section];

            protected override object GetItemAt(NSIndexPath indexPath) => (this.ItemsSource as ObservableCollection<SqlAdunisAppointment>[])?[indexPath.Section]?[indexPath.Row];
        }
    }
}

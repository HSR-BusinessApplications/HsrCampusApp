// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
    internal partial class ExamEntriesViewController : MvxTableViewController<ExamEntriesViewModel>
    {
        public ExamEntriesViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var source = new TableSource(this.TableView);

            this.CreateBinding(source)
                .To<ExamEntriesViewModel>(vm => vm.Entries)
                .Apply();

            this.TableView.TrimEmptyCells();
            this.TableView.Source = source;
            this.TableView.ReloadData();
        }

        public class TableSource : SingleCellTableSource<ExamCell>
        {
            private List<DateTime> dates = new List<DateTime>();

            public TableSource(UITableView tableView)
                : base(tableView)
            {
            }

            public override nint NumberOfSections(UITableView tableView) => this.dates.Count;

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                var date = this.dates[(int)section];

                return this.ItemsSource.OfType<SqlAdunisAppointment>().Count(t => t.StartTime.Date == date);
            }

            public override string TitleForHeader(UITableView tableView, nint section) => $"{this.dates[(int)section] :D}";

            public override void ReloadTableData()
            {
                base.ReloadTableData();

                this.dates = this.ItemsSource.OfType<SqlAdunisAppointment>().Select(t => t.StartTime.Date).Distinct().ToList();
            }

            protected override object GetItemAt(NSIndexPath indexPath)
            {
                var date = this.dates[indexPath.Section];

                return this.ItemsSource.OfType<SqlAdunisAppointment>().Where(t => t.StartTime.Date == date).Skip(indexPath.Row).FirstOrDefault();
            }
        }
    }
}

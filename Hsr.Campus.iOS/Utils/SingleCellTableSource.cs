// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS.Utils
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Foundation;
    using MvvmCross.Binding.iOS.Views;
    using UIKit;

    public class SingleCellTableSource<T> : MvxTableViewSource, INotifyPropertyChanged
        where T : MvxTableViewCell
    {
        private readonly nfloat height;
        private readonly string identifier;
        private readonly MethodInfo heightForMethod;

        private bool hasContentField = true;

        public SingleCellTableSource(UITableView tableView)
            : base(tableView)
        {
            tableView.RegisterNibForCellReuse(LoadingCell.Nib, LoadingCell.Key);
            tableView.RegisterNibForCellReuse(NoDataCell.Nib, NoDataCell.Key);

            var attr = typeof(T).GetCustomAttributes(false).OfType<CellDefinitionAttribute>().FirstOrDefault();

            this.heightForMethod = typeof(T).GetMethod("HeightFor");

            if (attr == null)
            {
                throw new CellDefinitionAttributeException("TableCell must have CellDefinitionAttribute");
            }

            this.height = attr.Height;
            this.identifier = attr.Identifier;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool HasContent
        {
            get
            {
                return this.hasContentField;
            }

            set
            {
                this.hasContentField = value;
                this.OnPropertyChanged(nameof(this.HasContent));
                this.OnPropertyChanged("ItemsSource");
            }
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            var original = base.RowsInSection(tableview, section);

            return original > 0 ? original : 1;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            if (base.RowsInSection(tableView, 0) == 0)
            {
                return 44;
            }

            if (this.heightForMethod == null)
            {
                return this.height;
            }

            return (nfloat)this.heightForMethod.Invoke(null, new[] { this.GetItemAt(indexPath) });
        }

        protected override object GetItemAt(NSIndexPath indexPath)
        {
            return base.RowsInSection(this.TableView, indexPath.Section) == 0 ? null : base.GetItemAt(indexPath);
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var original = base.RowsInSection(tableView, 0);

            if (original <= 0)
            {
                return this.TableView.DequeueReusableCell(this.HasContent ? LoadingCell.Key : NoDataCell.Key, indexPath);
            }

            var cell = this.TableView.DequeueReusableCell(this.identifier, indexPath);
            cell.SetNeedsDisplay();
            return cell;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

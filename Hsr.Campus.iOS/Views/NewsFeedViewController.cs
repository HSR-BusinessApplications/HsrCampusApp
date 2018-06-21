// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Core.Model;
    using Core.ViewModels;
    using Foundation;
    using MvvmCross.Binding.BindingContext;
    using MvvmCross.Binding.iOS.Views;
    using MvvmCross.iOS.Views;
    using MvvmCross.iOS.Views.Presenters.Attributes;
    using UIKit;

    [MvxFromStoryboard("News")]
    [MvxChildPresentation]
    internal partial class NewsFeedViewController : MvxTableViewController<NewsFeedViewModel>
    {
        public NewsFeedViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var source = new TableSource(this.TableView);

            this.CreateBinding(source)
                .To<NewsFeedViewModel>(vm => vm.Items)
                .Apply();

            this.CreateBinding(source)
                .For(t => t.SelectionChangedCommand)
                .To<NewsFeedViewModel>(t => t.ShowDetailCommand)
                .Apply();

            this.CreateBinding(source)
                .For(t => t.ShowNoContent)
                .To<NewsFeedViewModel>(t => t.ShowNoContent)
                .Apply();

            this.TableView.TrimEmptyCells();
            this.TableView.Source = source;
            this.TableView.ReloadData();
        }

        public class TableSource : MvxTableViewSource, INotifyPropertyChanged
        {
            private readonly MethodInfo heightForTextMethod;
            private readonly MethodInfo heightForTitleMethod;

            private bool showNoContent = true;

            public TableSource(UITableView tableView)
                : base(tableView)
            {
                tableView.RegisterNibForCellReuse(LoadingCell.Nib, LoadingCell.Key);
                tableView.RegisterNibForCellReuse(NoDataCell.Nib, NoDataCell.Key);

                this.heightForTextMethod = typeof(NewsTextCell).GetMethod("HeightFor");
                this.heightForTitleMethod = typeof(NewsTitleCell).GetMethod("HeightFor");
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public override IEnumerable ItemsSource
            {
                get
                {
                    return base.ItemsSource;
                }

                set
                {
                    base.ItemsSource = value;
                    this.OnPropertyChanged();
                }
            }

            public bool ShowNoContent
            {
                get
                {
                    return this.showNoContent;
                }

                set
                {
                    this.showNoContent = value;
                    this.OnPropertyChanged();
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

                var news = (SqlNews)this.GetItemAt(indexPath);

                if (string.IsNullOrEmpty(news.Title) && !string.IsNullOrEmpty(news.PicturePath))
                {
                    return NewsImageCell.Height;
                }

                if (string.IsNullOrEmpty(news.Message))
                {
                    return (nfloat)this.heightForTitleMethod.Invoke(null, new[] { this.GetItemAt(indexPath) });
                }

                return (nfloat)this.heightForTextMethod.Invoke(null, new[] { this.GetItemAt(indexPath) });
            }

            protected override object GetItemAt(NSIndexPath indexPath)
            {
                return base.RowsInSection(this.TableView, indexPath.Section) == 0 ? null : base.GetItemAt(indexPath);
            }

            protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
            {
                var original = base.RowsInSection(tableView, 0);

                if (original == 0)
                {
                    return this.TableView.DequeueReusableCell(this.ShowNoContent ? NoDataCell.Key : LoadingCell.Key, indexPath);
                }

                var news = (SqlNews)item;

                if (string.IsNullOrEmpty(news.Title) && !string.IsNullOrEmpty(news.PicturePath))
                {
                    return this.TableView.DequeueReusableCell(NewsImageCell.Identifier, indexPath);
                }

                if (string.IsNullOrEmpty(news.Message))
                {
                    return this.TableView.DequeueReusableCell(NewsTitleCell.Identifier, indexPath);
                }

                var cell = this.TableView.DequeueReusableCell(NewsTextCell.Identifier, indexPath);

                cell.SetNeedsDisplay();
                cell.SetNeedsLayout();

                return cell;
            }

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

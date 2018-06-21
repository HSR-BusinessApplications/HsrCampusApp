// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.
using Hsr.Campus.Core.Resources;

namespace Hsr.Campus.iOS
{
    using System;
    using System.Linq;
    using CoreGraphics;
    using Foundation;
    using Hsr.Campus.Core.ViewModels;
    using Hsr.Campus.iOS.Utils;
    using MvvmCross.Binding.BindingContext;
    using MvvmCross.iOS.Views;
    using MvvmCross.iOS.Views.Presenters.Attributes;
    using UIKit;

    [MvxFromStoryboard("Home")]
    [MvxRootPresentation(WrapInNavigationController = true)]
    internal partial class HomeViewController : MvxTableViewController<HomeViewModel>
    {
        public HomeViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var source = new TableSource(this.TableView);

            this.CreateBinding(source)
                .To<HomeViewModel>(vm => vm.Items)
                .Apply();

            this.CreateBinding(source)
                .For(t => t.SelectionChangedCommand)
                .To<HomeViewModel>(t => t.ShowDetailCommand)
                .Apply();

            this.TableView.TrimEmptyCells();
            this.TableView.Source = source;
            this.TableView.ReloadData();

            this.Title = AppResources.ApplicationTitle;

            this.ViewModel.VerifyFirstRun(null);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            this.ViewModel.Refresh();

            this.NavigationController.View.BackgroundColor = UIColor.White;
        }

        public class TableSource : SingleCellTableSource<NavCell>
        {
            public TableSource(UITableView tableView)
                : base(tableView)
            {
            }

            public override nint NumberOfSections(UITableView tableView) => this.ItemsSource.OfType<NavSection>().Count();

            public override nint RowsInSection(UITableView tableview, nint section) => this.ItemsSource.OfType<NavSection>().Skip((int)section).First().Items.Count;

            public override string TitleForHeader(UITableView tableView, nint section) => this.ItemsSource.OfType<NavSection>().Skip((int)section).First().Title;

            protected override object GetItemAt(NSIndexPath indexPath) => this.ItemsSource.OfType<NavSection>().Skip(indexPath.Section).First().Items[indexPath.Row];
        }
    }
}

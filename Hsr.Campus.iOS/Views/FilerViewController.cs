// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using Hsr.Campus.Core.ViewModels;
    using Hsr.Campus.iOS.Utils;
    using MvvmCross.Binding.BindingContext;
    using MvvmCross.iOS.Views;
    using MvvmCross.iOS.Views.Presenters.Attributes;

    [MvxFromStoryboard("Filer")]
    [MvxChildPresentation]
    internal partial class FilerViewController : MvxTableViewController<FilerViewModel>
    {
        public FilerViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var source = new SingleCellTableSource<FilerCell>(this.TableView);

            this.CreateBinding(source)
                .To<FilerViewModel>(vm => vm.Listings)
                .Apply();

            this.CreateBinding(source)
                .For(t => t.SelectionChangedCommand)
                .To<FilerViewModel>(t => t.ShowDetailCommand)
                .Apply();

            this.CreateBinding(source)
                .For(t => t.HasContent)
                .To<AbstractViewModel>(t => t.HasContent)
                .Apply();

            this.TableView.TrimEmptyCells();
            this.TableView.Source = source;
            this.TableView.ReloadData();

            this.NavigationItem.Title = this.ViewModel.Title;
        }
    }
}

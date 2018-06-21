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

    [MvxFromStoryboard("Events")]
    [MvxChildPresentation]
    internal partial class CalendarViewController : MvxTableViewController<CalendarViewModel>
    {
        public CalendarViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var source = new SingleCellTableSource<CalendarEventCell>(this.TableView);

            this.CreateBinding(source)
                .To<CalendarViewModel>(vm => vm.Items)
                .Apply();

            this.CreateBinding(source)
                .For(t => t.HasContent)
                .To<AbstractViewModel>(t => t.HasContent)
                .Apply();

            this.TableView.Source = source;
            this.TableView.ReloadData();
        }
    }
}

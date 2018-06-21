// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Android.App
{
    using Android.Views;
    using Hsr.Campus.Core.ViewModels;

    public static class ActivityExtensions
    {
        public static void BindProgressBar(this Activity act, AbstractViewModel vmModel)
        {
            vmModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != nameof(AbstractViewModel.IsWorking))
                {
                    return;
                }

                act.SetProgressBarVisibility(vmModel.IsWorking);
                act.SetProgressBarIndeterminateVisibility(vmModel.IsWorking);
                act.SetProgressBarIndeterminate(vmModel.IsWorking);
            };
        }

        public static void BindProgressBar(this IMenu menu, AbstractViewModel vmModel)
        {
            vmModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != nameof(AbstractViewModel.IsWorking))
                {
                    return;
                }

                var item = menu.FindItem(Hsr.Campus.Droid.Resource.Id.menu_refresh);

                item?.SetVisible(!vmModel.IsWorking);
            };

            vmModel.RaisePropertyChanged(nameof(AbstractViewModel.IsWorking));
        }
    }
}

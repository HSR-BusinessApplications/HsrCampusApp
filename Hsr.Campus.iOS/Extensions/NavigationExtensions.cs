// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System.Windows.Input;
    using Hsr.Campus.Core.ViewModels;
    using MvvmCross.Binding.BindingContext;
    using UIKit;

    public static class NavigationExtensions
    {
        public static void SetRightBarItem(this IMvxBindingContextOwner controller, ICommand command = null)
        {
            var refresh = new UIBarButtonItem(UIBarButtonSystemItem.Refresh, (o, e) =>
            {
                if (command != null)
                {
                    command.Execute(null);
                }
            });

            controller.CreateBinding(refresh)
                .For(t => t.Enabled)
                .To<AbstractViewModel>(vm => vm.IsWorking)
                .WithConversion("Negate")
                .Apply();

            (controller as UIViewController).NavigationItem.SetRightBarButtonItem(refresh, false);
        }
    }
}

// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core
{
    using System;
    using MvvmCross.Core.Navigation;
    using MvvmCross.Core.ViewModels;
    using ViewModels;

    public class CustomAppStart : MvxNavigatingObject, IMvxAppStart
    {
        public CustomAppStart()
        {
        }

        public void Start(object hint = null)
        {
            try
            {
                if (hint != null && hint is Type)
                {
                    this.ShowViewModel((Type)hint);
                }
                else if (hint != null && hint is StartUpHint)
                {
                    var s = (StartUpHint)hint;

                    this.ShowViewModel(s.ViewModel, s.Parameter);
                }
                else
                {
                    this.ShowViewModel<HomeViewModel>();
                }
            }
            catch
            {
                // Could not navigate to the view
            }
        }
    }
}

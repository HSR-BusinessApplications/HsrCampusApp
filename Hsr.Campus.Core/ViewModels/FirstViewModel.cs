// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ViewModels
{
    using MvvmCross.Core.ViewModels;

    public class FirstViewModel
        : MvxViewModel
    {
        private string hello = "Hello MvvmCross";

        public string Hello
        {
            get
            {
                return this.hello;
            }

            set
            {
                this.hello = value;
                this.RaisePropertyChanged();
            }
        }
    }
}

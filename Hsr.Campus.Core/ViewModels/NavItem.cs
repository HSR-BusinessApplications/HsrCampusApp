// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ViewModels
{
    using System;
    using MvvmCross.Core.ViewModels;

    public class NavItem
        : MvxNotifyPropertyChanged, ITitled
    {
        private string iconPath;

        public string Title { get; set; }

        public string IconPath
        {
            get
            {
                return this.iconPath;
            }

            set
            {
                if (value == this.iconPath)
                {
                    return;
                }

                this.iconPath = value;
                this.RaisePropertyChanged();
            }
        }

        public Action Detail { get; set; }
    }
}

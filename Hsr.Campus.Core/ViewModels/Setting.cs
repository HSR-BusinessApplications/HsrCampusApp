// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class Setting : INotifyPropertyChanged
    {
        private string subTitle;
        private string title;
        private string image;
        private Action act;

        public event PropertyChangedEventHandler PropertyChanged;

        public Action Act
        {
            get
            {
                return this.act;
            }

            set
            {
                this.act = value;
                this.RaisePropertyChanged();
            }
        }

        public string Image
        {
            get
            {
                return this.image;
            }

            set
            {
                this.image = value;
                this.RaisePropertyChanged();
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
                this.RaisePropertyChanged();
            }
        }

        public string SubTitle
        {
            get
            {
                return this.subTitle;
            }

            set
            {
                this.subTitle = value;
                this.RaisePropertyChanged();
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

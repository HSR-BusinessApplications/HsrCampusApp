// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ViewModels
{
    using System.Collections.ObjectModel;

    public abstract class AbstractCollectionViewModel<T>
        : AbstractViewModel
    {
        private ObservableCollection<T> items = new ObservableCollection<T>();

        protected AbstractCollectionViewModel()
        {
            this.Items.CollectionChanged += (sender, args) => this.RaisePropertyChanged(nameof(this.Items));
        }

        public ObservableCollection<T> Items
        {
            get
            {
                return this.items;
            }

            protected set
            {
                this.items = value;
                this.RaisePropertyChanged();
            }
        }
    }
}

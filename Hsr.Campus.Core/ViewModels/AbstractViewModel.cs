// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows.Input;
    using ApplicationServices;
    using MvvmCross.Core.Navigation;
    using MvvmCross.Core.Platform;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.Platform;

    public abstract class AbstractViewModel : MvxViewModel
    {
        private readonly List<CancellationTokenSource> cancellationTokenSources = new List<CancellationTokenSource>();
        private bool isLoading;
        private bool isUpdating;
        private bool hasContent = true;

        public ICommand GoBack => new MvxCommand(() => this.Close(this));

        public ICommand CancelCommand => new MvxCommand(() => this.CancelAll());

        public IMvxNavigationService NavigationService { get; set; }

        public bool IsLoading
        {
            get
            {
                return this.isLoading;
            }

            protected set
            {
                if (value == this.isLoading)
                {
                    return;
                }

                this.isLoading = value;
                this.RaisePropertyChanged();

                if (!this.IsUpdating)
                {
                    this.RaisePropertyChanged(nameof(this.IsWorking));
                }

                if (!this.IsUpdating && !this.HasContent)
                {
                    this.RaisePropertyChanged(nameof(this.ShowNoContent));
                }

                if (this.IsUpdating && !this.HasContent)
                {
                    this.RaisePropertyChanged(nameof(this.ShowUpdating));
                }
            }
        }

        public virtual bool IsUpdating
        {
            get
            {
                return this.isUpdating;
            }

            protected set
            {
                if (value == this.isUpdating)
                {
                    return;
                }

                this.isUpdating = value;
                this.RaisePropertyChanged();

                if (!this.IsLoading)
                {
                    this.RaisePropertyChanged(nameof(this.IsWorking));
                }

                if (!this.IsLoading && !this.HasContent)
                {
                    this.RaisePropertyChanged(nameof(this.ShowNoContent));
                }

                if (!this.IsLoading && !this.HasContent)
                {
                    this.RaisePropertyChanged(nameof(this.ShowUpdating));
                }
            }
        }

        public bool HasContent
        {
            get
            {
                return this.hasContent;
            }

            protected set
            {
                if (value == this.hasContent)
                {
                    return;
                }

                this.hasContent = value;
                this.RaisePropertyChanged();

                if (!this.IsWorking)
                {
                    this.RaisePropertyChanged(nameof(this.ShowNoContent));
                }

                if (this.IsUpdating && !this.IsLoading)
                {
                    this.RaisePropertyChanged(nameof(this.ShowUpdating));
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the ViewModel is currently working (<see cref="IsLoading"/> or <see cref="IsUpdating"/>)
        /// </summary>
        public bool IsWorking => this.IsUpdating || this.IsLoading;

        /// <summary>
        /// Gets a value indicating whether it should be displayed that there is no content
        /// </summary>
        public bool ShowNoContent => !this.IsWorking && !this.HasContent;

        /// <summary>
        /// Gets a value indicating whether it should be displayed it is currently updating
        /// </summary>
        public bool ShowUpdating => this.IsUpdating && !this.IsLoading && !this.HasContent;

        public virtual DateTime LastUpdated => DateTime.MinValue;

        public void CancelAll(bool throwOnFirstException = false)
        {
            for (var i = this.cancellationTokenSources.Count - 1; i >= 0; i--)
            {
                this.cancellationTokenSources[i].Cancel(throwOnFirstException);
            }

            this.cancellationTokenSources.Clear();
        }

        protected void Navigate<TViewModel>()
            where TViewModel : IMvxViewModel
        {
            this.ShowViewModel<TViewModel>();
        }

        protected void Navigate<TViewModel, TParameter>(TParameter parametersValueObject)
            where TViewModel : IMvxViewModel<TParameter>
        {
            this.ShowViewModel<TViewModel>(parametersValueObject);
        }

        protected CancellationToken ObtainCancellationToken()
        {
            var tokenSource = new CancellationTokenSource();

            this.cancellationTokenSources.Add(tokenSource);

            tokenSource.Token.Register(() => this.cancellationTokenSources.Remove(tokenSource));

            return tokenSource.Token;
        }
    }
}

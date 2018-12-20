// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Hsr.Campus.Core.ApplicationServices;
    using Hsr.Campus.Core.Resources;
    using MvvmCross.Core.Navigation;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.Platform;
    using MvvmCross.Plugins.WebBrowser;

    public class SportsViewModel : AbstractViewModel
    {
        private readonly IMvxWebBrowserTask browser;
        private readonly IServiceApi serviceApi;
        private string text;

        public SportsViewModel(IMvxNavigationService navigationService, IMvxWebBrowserTask browser, IServiceApi serviceApi)
        {
            this.NavigationService = navigationService;
            this.browser = browser;
            this.serviceApi = serviceApi;
        }

        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
                this.RaisePropertyChanged();
            }
        }

        public ICommand GoSportsCommand => new MvxCommand(() => this.browser.ShowWebPage(this.serviceApi.SportsUri));

        public void Init()
        {
            this.Text = AppResources.SportsText;
        }
    }
}

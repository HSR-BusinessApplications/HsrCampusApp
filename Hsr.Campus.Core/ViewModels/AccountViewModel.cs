// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ViewModels
{
    using System;
    using System.Security;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using ApplicationServices;
    using DomainServices;
    using Hsr.Campus.Core.OAuth;
    using Model;
    using MvvmCross.Core.Navigation;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.Plugins.WebBrowser;
    using Resources;

    public class AccountViewModel : AbstractParameterizedViewModel<AccountViewModel.Args>
    {
        private readonly IAccountService account;
        private readonly IIOCacheService cache;
        private readonly IAdunisRepository adunisRepository;
        private readonly IAccountBalanceRepository accountBalanceRepository;
        private readonly IOAuthUtils oAuth;
        private readonly IUserInteractionService userInteraction;

        public AccountViewModel(
            IMvxNavigationService navigationService,
            IAccountService account,
            IIOCacheService cache,
            IAdunisRepository adunisRepository,
            IAccountBalanceRepository accountBalanceRepository,
            IOAuthUtils oAuth,
            IUserInteractionService userInteraction)
        {
            this.NavigationService = navigationService;
            this.account = account;
            this.cache = cache;
            this.adunisRepository = adunisRepository;
            this.accountBalanceRepository = accountBalanceRepository;
            this.oAuth = oAuth;
            this.userInteraction = userInteraction;
        }

        public enum Mode
        {
            Default = 0,
            View = 1,
            Renew = 2
        }

        public ICommand GoHome => new MvxCommand(this.Navigate<HomeViewModel>);

        public ICommand GoAuthCommand => new MvxCommand(() => this.oAuth.DisplayCodeFlowUrl(this));

        public ICommand GoViewCommand => new MvxCommand(() => this.oAuth.DisplalyWebGuiUri());

        public Account Account => this.account.Retrieve();

        public Mode CurrentMode { get; private set; }

        public Type ReturnTo { get; private set; }

        public string QueryString { get; set; }

        public void Init(Args args)
        {
            if (args == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(args.UriPart))
            {
                this.QueryString = args.UriPart;
            }

            this.CurrentMode = args.Mode;
            this.ReturnTo = args.ReturnTo;
        }

        public override void Prepare(Args parameter)
        {
        }

        public AccountAuthenticationState GetAuthenticationState()
        {
            if (!string.IsNullOrEmpty(this.QueryString))
            {
                try
                {
                    var response = this.oAuth.AuthToken(this.QueryString);

                    if (response.Error == OAuthErrors.AccessDenied)
                    {
                        this.AccessDenied();
                        return AccountAuthenticationState.Unauthenticated;
                    }

                    this.oAuth.OAuthSetupAsync(response);
                    return AccountAuthenticationState.Success;
                }
                catch (SecurityException)
                {
                    if (this.account.HasAccount)
                    {
                        this.userInteraction.Dialog("OAuth", AppResources.OAuthErrorStateAuthenticated);
                        return AccountAuthenticationState.Authenticated;
                    }

                    this.userInteraction.Dialog("OAuth", AppResources.OAuthErrorStateUnauthenticated);
                    return AccountAuthenticationState.Unauthenticated;
                }
            }

            if (this.account.HasAccount && this.CurrentMode != Mode.Renew)
            {
                return AccountAuthenticationState.Authenticated;
            }

            return this.CurrentMode == Mode.Renew ? AccountAuthenticationState.Expired : AccountAuthenticationState.Unauthenticated;
        }

        public void AccessDenied()
        {
            this.userInteraction.Toast(AppResources.OAuthErrorDenied, ToastTime.Medium);
            this.Navigate<HomeViewModel>();
        }

        public void DeAuth(Action callback)
        {
            this.userInteraction.Dialog(AppResources.ApplicationTitle, AppResources.OAuthRemoveConfirm, () =>
            {
                this.ShredData();
                callback?.Invoke();
            });
        }

        public Task ShredData()
        {
            return Task.Factory.StartNew(() =>
            {
                this.IsUpdating = true;

                this.account.Delete();
                this.cache.ClearAll();

                this.adunisRepository.Truncate();
                this.accountBalanceRepository.Remove();

                this.IsUpdating = false;
            });
        }

        public class Args
        {
            public string UriPart { get; set; }

            public Mode Mode { get; set; }

            public Type ReturnTo { get; set; }
        }
    }
}

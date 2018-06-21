// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ViewModels
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using ApplicationServices;
    using DomainServices;
    using Model;
    using MvvmCross.Core.Navigation;
    using MvvmCross.Core.ViewModels;
    using Resources;

    public class AccountBalanceViewModel : AbstractViewModel
    {
        private readonly IUserInteractionService userInteraction;
        private readonly IAccountBalanceRepository accountBalanceRepository;
        private readonly IAccountBalanceSync accountBalanceSync;

        private AccountBalance balance;

        public AccountBalanceViewModel(IMvxNavigationService navigationService, IAccountBalanceRepository accountBalanceRepository, IAccountBalanceSync accountBalanceSync, IUserInteractionService userInteraction)
        {
            this.NavigationService = navigationService;
            this.accountBalanceRepository = accountBalanceRepository;
            this.accountBalanceSync = accountBalanceSync;
            this.userInteraction = userInteraction;
        }

        public ICommand LoadCacheCommand => new MvxCommand(this.LoadFromCache);

        public ICommand UpdateCommand => new MvxAsyncCommand(this.UpdateAsync);

        public AccountBalance Balance
        {
            get
            {
                return this.balance;
            }

            set
            {
                this.balance = value;
                this.RaisePropertyChanged();
            }
        }

        public override DateTime LastUpdated => this.accountBalanceRepository.RetrieveCurrentState()?.LastUpdate.ToLocalTime() ?? DateTime.MinValue;

        public async Task Init()
        {
            this.LoadFromCache();

            await this.UpdateAsync();
        }

        public void LoadFromCache()
        {
            if (this.IsLoading)
            {
                return;
            }

            this.IsLoading = true;

            this.Balance = this.accountBalanceRepository.RetrieveCurrentState();

            this.IsLoading = false;
        }

        public async Task UpdateAsync()
        {
            if (this.IsUpdating)
            {
                return;
            }

            this.IsUpdating = true;

            var status = await this.accountBalanceSync.UpdateAsync(this.ObtainCancellationToken());

            switch (status)
            {
                case ResultState.Success:
                case ResultState.NoData:

                    this.RaisePropertyChanged(nameof(this.LastUpdated));

                    this.LoadFromCache();
                    break;
                case ResultState.Error:
                case ResultState.ErrorNetwork:
                    this.userInteraction.Toast(AppResources.ConnectionFailed, ToastTime.Short);
                    break;
                case ResultState.OAuthExpired:
                    this.IsUpdating = false;
                    this.Navigate<AccountViewModel, AccountViewModel.Args>(new AccountViewModel.Args
                    {
                        Mode = AccountViewModel.Mode.Renew,
                        ReturnTo = typeof(AccountBalanceViewModel)
                    });
                    return;
                case ResultState.NotModified:
                case ResultState.Canceled:
                    break;
                default:
                    this.IsUpdating = false;
                    Debug.Assert(false, $"ResultState: {status}, not handled");
                    return;
            }

            this.IsUpdating = false;
        }
    }
}

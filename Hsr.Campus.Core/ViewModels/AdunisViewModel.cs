// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using ApplicationServices;
    using DomainServices;
    using Model;
    using MvvmCross.Core.Navigation;
    using MvvmCross.Core.ViewModels;
    using Resources;

    public class AdunisViewModel : AbstractViewModel
    {
        private readonly IAdunisRepository adunisRepository;
        private readonly IAdunisSync adunisSync;
        private readonly IUserInteractionService userInteraction;

        private ObservableCollection<SqlTimeperiod> periods;

        public AdunisViewModel(IMvxNavigationService navigationService, IAdunisRepository adunisRepository, IAdunisSync adunisSync, IUserInteractionService userInteraction)
        {
            this.NavigationService = navigationService;
            this.adunisRepository = adunisRepository;
            this.adunisSync = adunisSync;
            this.userInteraction = userInteraction;
        }

        public ICommand UpdateCommand => new MvxAsyncCommand(() => this.UpdateAsync(true));

        public ICommand ShowPeriodCommand => new MvxCommand<SqlTimeperiod>(this.ShowPeriod);

        public ObservableCollection<SqlTimeperiod> OwnPeriods
        {
            get
            {
                return this.periods;
            }

            private set
            {
                this.periods = value;
                this.RaisePropertyChanged();
            }
        }

        public override DateTime LastUpdated => this.adunisRepository.LastUpdated.ToLocalTime();

        public async Task Init()
        {
            this.LoadFromCache();
            await this.UpdateAsync(false);
        }

        public async Task UpdateAsync(bool force)
        {
            if (this.IsUpdating)
            {
                return;
            }

            this.IsUpdating = true;

            var state = await this.adunisSync.UpdateAsync(force, this.ObtainCancellationToken());

            switch (state)
            {
                case ResultState.Success:
                case ResultState.NoData:

                    this.RaisePropertyChanged(nameof(this.LastUpdated));

                    this.LoadFromCache();
                    break;
                case ResultState.Error:
                case ResultState.ErrorNetwork:
                    if (force)
                    {
                        this.Dispatcher.RequestMainThreadAction(() => this.userInteraction.Toast(AppResources.ConnectionFailed, ToastTime.Short));
                    }

                    break;
                case ResultState.OAuthExpired:
                    this.IsUpdating = false;
                    this.Navigate<AccountViewModel, AccountViewModel.Args>(new AccountViewModel.Args
                    {
                        Mode = AccountViewModel.Mode.Renew,
                        ReturnTo = typeof(AdunisViewModel)
                    });
                    return;
                case ResultState.NotModified:
                case ResultState.Canceled:
                    break;
                default:
                    this.IsUpdating = false;
                    Debug.Assert(false, $"ResultState: {state}, not handled");
                    return;
            }

            this.IsUpdating = false;
        }

        public void ShowPeriod(SqlTimeperiod timeperiod)
        {
            if (timeperiod == null)
            {
                return;
            }

            switch (timeperiod.Type)
            {
                case AdunisType.Timetable:
                    this.Navigate<TimetableViewModel, object>(new { termId = timeperiod.TermID });
                    break;
                case AdunisType.Exam:
                    this.Navigate<ExamViewModel, object>(new { termId = timeperiod.TermID });
                    break;
                case AdunisType.Semester:
                case AdunisType.ExamPreparation:
                    break;
                default:
                    Debug.Assert(false, $"AdunisType: {timeperiod.Type}, not handled");
                    break;
            }
        }

        private void LoadFromCache()
        {
            if (this.IsLoading)
            {
                return;
            }

            this.IsLoading = true;

            this.HasContent = false;

            this.OwnPeriods = new ObservableCollection<SqlTimeperiod>();

            this.OwnPeriods.CollectionChanged += (o, e) => this.RaisePropertyChanged(nameof(this.OwnPeriods));

            foreach (var t in this.adunisRepository.RetrieveNonEmptyTimeperiods())
            {
                this.Dispatcher.RequestMainThreadAction(() => this.OwnPeriods.Add(t));
                this.HasContent = true;
            }

            this.IsLoading = false;
        }
    }
}

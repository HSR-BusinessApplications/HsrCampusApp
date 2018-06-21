// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using ApplicationServices;
    using DomainServices;
    using Model;
    using MvvmCross.Core.Navigation;
    using MvvmCross.Core.ViewModels;
    using Resources;

    public class ExamViewModel : AbstractParameterizedViewModel<object>
    {
        private readonly IAdunisRepository adunisRepository;
        private readonly IAdunisSync adunisSync;
        private readonly ILogging logging;

        private ObservableCollection<ExamEntriesViewModel> exams;
        private SqlTimeperiod period;

        public ExamViewModel(IMvxNavigationService navigationService, IAdunisSync adunisSync, IAdunisRepository adunisRepository, ILogging logging)
        {
            this.NavigationService = navigationService;
            this.adunisSync = adunisSync;
            this.adunisRepository = adunisRepository;
            this.logging = logging;
        }

        public ObservableCollection<ExamEntriesViewModel> AllExams
        {
            get
            {
                return this.exams;
            }

            set
            {
                this.exams = value;
                this.RaisePropertyChanged();
            }
        }

        public void Init(int termId)
        {
            this.period = this.adunisRepository.RetrieveTimeperiodById(termId);
            this.LoadWeeks();
        }

        public override void Prepare(object parameter)
        {
        }

        public override async void Start()
        {
            base.Start();
            await this.UpdateAsync();
        }

        private async Task UpdateAsync()
        {
            if (this.IsUpdating)
            {
                return;
            }

            this.IsUpdating = true;

            var state = await this.adunisSync.UpdateAppointmentsAsync(false, this.period, this.ObtainCancellationToken());

            switch (state)
            {
                case ResultState.Error:
                case ResultState.ErrorNetwork:
                case ResultState.NotModified:
                case ResultState.Canceled:
                    break;
                case ResultState.Success:
                    this.LoadWeeks();
                    break;
                case ResultState.OAuthExpired:
                    this.IsUpdating = false;
                    this.Navigate<AccountViewModel, AccountViewModel.Args>(new AccountViewModel.Args
                    {
                        Mode = AccountViewModel.Mode.Renew,
                        ReturnTo = typeof(AdunisViewModel)
                    });
                    return;
                case ResultState.NoData:
                    this.IsUpdating = false;
                    this.GoBack.Execute(null);
                    return;
                default:
                    this.IsUpdating = false;
                    Debug.Assert(false, $"ResultState: {state}, not handled");
                    return;
            }

            this.IsUpdating = false;
        }

        private void LoadWeeks()
        {
            if (this.IsLoading)
            {
                return;
            }

            this.IsLoading = true;

            var allExams = new ObservableCollection<ExamEntriesViewModel>();

            try
            {
                this.HasContent = false;

                var dateStart = this.period.Begin.Date;
                var weekNr = 0;
                while (dateStart < this.period.End)
                {
                    var entries = new ObservableCollection<SqlAdunisAppointment>(
                        this.adunisRepository.RetrieveAppointmentsForRange(
                            this.period,
                            dateStart,
                            dateStart.AddDays(7)));

                    if (entries.Any())
                    {
                        this.HasContent = true;
                    }

                    allExams.Add(new ExamEntriesViewModel
                    {
                        Name = string.Format(AppResources.PivotWeek, ++weekNr),
                        Entries = entries
                    });

                    dateStart = dateStart.AddDays(7);
                }
            }
            catch (Exception ex)
            {
                this.logging.Exception(this, ex);
                this.HasContent = false;
            }

            this.AllExams = allExams;

            this.IsLoading = false;
        }
    }
}

// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using ApplicationServices;
    using DomainServices;
    using Model;
    using MvvmCross.Core.Navigation;
    using MvvmCross.Core.ViewModels;
    using Resources;

    public class TimetableViewModel : AbstractParameterizedViewModel<object>
    {
        private readonly IAdunisRepository adunisRepository;
        private readonly IAdunisSync adunisSync;
        private readonly ILogging logging;

        private ObservableCollection<TimetableEntriesViewModel> days;
        private ObservableCollection<WeekViewModel> weeks;
        private WeekViewModel current;
        private SqlTimeperiod period;

        public TimetableViewModel(IMvxNavigationService navigationService, IAdunisRepository adunisRepository, IAdunisSync adunisSync, ILogging logging)
        {
            this.NavigationService = navigationService;
            this.adunisRepository = adunisRepository;
            this.adunisSync = adunisSync;
            this.logging = logging;

            this.OnPropertyChanged(() => this.CurrentWeek, () =>
            {
                if (this.CurrentWeek == null)
                {
                    return;
                }

                this.LoadFromCache(this.CurrentWeek.Start);
            });
        }

        public ObservableCollection<TimetableEntriesViewModel> AllDays
        {
            get
            {
                return this.days;
            }

            private set
            {
                this.days = value;
                this.RaisePropertyChanged();
            }
        }

        public ObservableCollection<WeekViewModel> Weeks
        {
            get
            {
                return this.weeks;
            }

            private set
            {
                this.weeks = value;
                this.RaisePropertyChanged();
            }
        }

        public WeekViewModel CurrentWeek
        {
            get
            {
                return this.current;
            }

            set
            {
                this.current = value;
                this.RaisePropertyChanged();
            }
        }

        public void Init(int termId)
        {
            this.period = this.adunisRepository.RetrieveTimeperiodById(termId);
            this.InitWeeks();
        }

        public override void Prepare(object parameter)
        {
        }

        public override async void Start()
        {
            base.Start();

            await this.UpdateAsync();
        }

        private void InitWeeks()
        {
            this.Weeks = new ObservableCollection<WeekViewModel>();

            var startDay = this.period.Begin.Date.SyncWeekday(DayOfWeek.Monday);
            var periodWeek = 0;
            var currWeekIdx = 0;

            while (startDay < this.period.End.Date)
            {
                this.Weeks.Add(new WeekViewModel()
                {
                    Start = startDay,
                    CalendarWeek = startDay.WeekNr(),
                    PeriodWeek = ++periodWeek
                });

                startDay = startDay.AddDays(7);

                if (startDay <= DateTime.Now.Date)
                {
                    currWeekIdx++;
                }
            }

            if (this.Weeks.Count <= 0)
            {
                return;
            }

            // LoadFromCache gets executed because of the OnPropertyChanged
            this.CurrentWeek = currWeekIdx < this.Weeks.Count ? this.Weeks[currWeekIdx] : this.Weeks[this.Weeks.Count - 1];
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
                    this.InitWeeks();
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

        private void LoadFromCache(DateTime week)
        {
            if (this.IsLoading)
            {
                return;
            }

            this.IsLoading = true;

            var allDays = new ObservableCollection<TimetableEntriesViewModel>();

            try
            {
                var divDayViewModel = new TimetableEntriesViewModel
                {
                    Name = AppResources.ResourceManager.GetString("DayDiv"),
                    Entries = new ObservableCollection<SqlAdunisAppointment>[SqlAdunisAppointment.Rows.Length],
                };

                divDayViewModel.Entries[0] = new ObservableCollection<SqlAdunisAppointment>();

                var dateDiv = this.period.Begin.AddDays(-7).SyncWeekday(DayOfWeek.Sunday).Date;
                foreach (var appointment in this.adunisRepository.RetrieveAppointmentsForDay(this.period, dateDiv))
                {
                    divDayViewModel.Entries[0].Add(appointment);
                }

                if (divDayViewModel.Entries[0].Count > 0)
                {
                    allDays.Add(divDayViewModel);
                }

                foreach (var dayOfWeek in this.period.GetUniqueDays())
                {
                    var dateDay = week.SyncWeekday(dayOfWeek);

                    var dayViewModel = new TimetableEntriesViewModel
                    {
                        Name = AppResources.ResourceManager.GetString("Day" + dayOfWeek),
                        Entries = new ObservableCollection<SqlAdunisAppointment>[SqlAdunisAppointment.Rows.Length],
                        Date = dateDay
                    };

                    foreach (var appointment in this.adunisRepository.RetrieveAppointmentsForDay(this.period, dateDay))
                    {
                        var row = appointment.Row;

                        if (dayViewModel.Entries[row] == null)
                        {
                            dayViewModel.Entries[row] = new ObservableCollection<SqlAdunisAppointment>();
                        }

                        dayViewModel.Entries[row].Add(appointment);
                    }

                    allDays.Add(dayViewModel);
                }

                this.HasContent = allDays.Count > 0;
            }
            catch (Exception ex)
            {
                this.logging.Exception(this, ex);
                this.HasContent = false;
            }

            this.AllDays = allDays;

            this.IsLoading = false;
        }
    }
}

// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ViewModels
{
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using MvvmCross.Core.Navigation;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.Platform;

    public class SportsViewModel : AbstractCollectionViewModel<CalendarViewModel>
    {
        private CalendarViewModel calendarEvents;
        private int subUpdating;
        private bool isLocalUpdating;

        public SportsViewModel(IMvxNavigationService navigationService)
        {
            this.NavigationService = navigationService;

            this.CalendarEvents = Mvx.IocConstruct<CalendarViewModel>();

            this.CalendarEvents.PropertyChanged += this.OnSubIsUpdating;

            this.Items.Add(this.CalendarEvents);
        }

        public ICommand UpdateCommand => new MvxAsyncCommand(() => this.UpdateAsync(true));

        private bool IsLocalUpdating
        {
            get
            {
                return this.isLocalUpdating;
            }

            set
            {
                this.isLocalUpdating = value;
                this.IsUpdating = this.isLocalUpdating || this.subUpdating > 0;
            }
        }

        private CalendarViewModel CalendarEvents
        {
            get
            {
                return this.calendarEvents;
            }

            set
            {
                this.calendarEvents = value;
                this.RaisePropertyChanged();
            }
        }

        public void Init()
        {
            this.CalendarEvents.Init("Agenda");
        }

        public override async void Start()
        {
            base.Start();

            this.CalendarEvents.Start();
            await this.UpdateAsync(false);
        }

        public async Task UpdateAsync(bool force)
        {
            if (this.IsLocalUpdating)
            {
                return;
            }

            this.IsLocalUpdating = true;

            await this.CalendarEvents.UpdateAsync(force, false);

            this.IsLocalUpdating = false;
        }

        private void OnSubIsUpdating(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(CalendarViewModel.IsUpdating))
            {
                this.subUpdating += ((CalendarViewModel)sender).IsUpdating ? 1 : -1;

                this.IsUpdating = this.IsLocalUpdating || this.subUpdating > 0;
            }
        }
    }
}

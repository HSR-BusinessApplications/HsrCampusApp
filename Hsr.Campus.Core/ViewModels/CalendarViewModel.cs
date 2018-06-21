// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using ApplicationServices;
    using DomainServices;
    using Model;
    using MvvmCross.Core.Navigation;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.Plugins.WebBrowser;
    using Resources;

    public class CalendarViewModel
        : AbstractCollectionViewModel<SqlCalendarEvent>, ITitled
    {
        private readonly ICalendarEventRepository calendarEventRepository;
        private readonly ICalendarEventSync calendarEventSync;
        private readonly IUserInteractionService userInteraction;
        private readonly IMvxWebBrowserTask webBrowser;

        private readonly TimeSpan timeBuffer = new TimeSpan(2, 0, 0);

        private string title;
        private bool hasMoreItems;

        public CalendarViewModel(IMvxNavigationService navigationService, ICalendarEventRepository calendarEventRepository, ICalendarEventSync calendarEventSync, IUserInteractionService userInteraction, IMvxWebBrowserTask webBrowser)
        {
            this.NavigationService = navigationService;
            this.calendarEventRepository = calendarEventRepository;
            this.calendarEventSync = calendarEventSync;
            this.userInteraction = userInteraction;
            this.webBrowser = webBrowser;
        }

        public MvxAsyncCommand LoadMoreCommand => new MvxAsyncCommand(() => this.UpdateAsync(false, true));

        public ICommand ShowDetailCommand => new MvxCommand<SqlCalendarEvent>(this.ShowDetail);

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

        public bool HasMoreItems
        {
            get
            {
                return this.hasMoreItems;
            }

            private set
            {
                this.hasMoreItems = value;
                this.RaisePropertyChanged();
            }
        }

        public void Init(string name)
        {
            this.Title = name;
            this.HasMoreItems = true;
            this.Items = new ObservableCollection<SqlCalendarEvent>();
        }

        public override void Start()
        {
            base.Start();

            this.LoadFromCache(DateTime.Now.Subtract(this.timeBuffer));
        }

        public async Task UpdateAsync(bool force, bool loadMore)
        {
            if (loadMore && !this.hasMoreItems)
            {
                return;
            }

            if (this.IsUpdating)
            {
                return;
            }

            this.IsUpdating = true;

            DateTime dateFrom;
            if (loadMore && this.Items.Count > 0)
            {
                dateFrom = this.Items.Last().Start;
                this.LoadFromCache(dateFrom);
            }
            else
            {
                dateFrom = DateTime.Now.Subtract(this.timeBuffer);
                this.calendarEventRepository.DeleteEventEntries(dateFrom);
            }

            this.HasMoreItems = false;

            var updateResult = await this.calendarEventSync.UpdateAsync(force, dateFrom, this.ObtainCancellationToken());

            switch (updateResult.Item1)
            {
                case ResultState.OAuthExpired:
                    break;
                case ResultState.NoData:
                    if (force)
                    {
                        this.Items = new ObservableCollection<SqlCalendarEvent>();
                    }

                    this.HasMoreItems = false;
                    break;
                case ResultState.Error:
                case ResultState.ErrorNetwork:
                    this.userInteraction.Toast(AppResources.ConnectionFailed, ToastTime.Short);

                    break;
                case ResultState.NotModified:
                case ResultState.Canceled:
                    this.HasMoreItems = true;
                    break;
                case ResultState.Success:
                    this.MergeUpdateResult(updateResult.Item2, dateFrom);
                    this.HasMoreItems = true;
                    break;
                default:
                    this.IsUpdating = false;
                    Debug.Assert(false, $"ResultState: {updateResult.Item1}, not handled");
                    return;
            }

            this.IsUpdating = false;
        }

        private void ShowDetail(SqlCalendarEvent item)
        {
            try
            {
                this.webBrowser.ShowWebPage(item.Link);
            }
            catch
            {
                // ignored
            }
        }

        private void LoadFromCache(DateTime dateFrom)
        {
            if (this.IsLoading)
            {
                return;
            }

            this.IsLoading = true;

            var calendarEvents = this.calendarEventRepository.RetrieveEventsAfter(dateFrom, 20);

            foreach (var calendarEvent in calendarEvents)
            {
                this.Items.Add(calendarEvent);
            }

            this.HasContent = this.Items.Count > 0;

            this.IsLoading = false;
        }

        private void MergeUpdateResult(IEnumerable<SqlCalendarEvent> calendarEvents, DateTime dateFrom)
        {
            if (this.IsLoading)
            {
                return;
            }

            this.IsLoading = true;

            var first = this.Items.FirstOrDefault(calendarEvent => calendarEvent.Start > dateFrom);

            var itemsIndex = first == default(SqlCalendarEvent) ? this.Items.Count : this.Items.IndexOf(first);

            calendarEvents = calendarEvents.Where(calendarEvent => calendarEvent.Start > dateFrom);

            foreach (var calendarEvent in calendarEvents)
            {
                if (this.Items.Any(calEvent => calEvent.EventID == calendarEvent.EventID))
                {
                    var oldIndex = this.Items.IndexOf(this.Items.First(calEvent => calEvent.EventID == calendarEvent.EventID));
                    this.Items[oldIndex] = calendarEvent;

                    if (oldIndex != itemsIndex)
                    {
                        if (itemsIndex < this.Items.Count)
                        {
                            this.Items.Move(oldIndex, itemsIndex);
                        }
                        else
                        {
                            var removedItem = this.Items[oldIndex];

                            this.Items.RemoveAt(oldIndex);
                            this.Items.Add(removedItem);
                        }
                    }
                }
                else
                {
                    if (itemsIndex < this.Items.Count)
                    {
                        this.Items.Insert(itemsIndex, calendarEvent);
                    }
                    else
                    {
                        this.Items.Add(calendarEvent);
                    }
                }

                itemsIndex++;
            }

            while (itemsIndex < this.Items.Count)
            {
                this.Items.RemoveAt(itemsIndex);
            }

            this.HasContent = this.Items.Count > 0;

            this.IsLoading = false;
        }
    }
}

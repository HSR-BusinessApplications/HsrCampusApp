// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Infrastructure.Sync
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ApplicationServices;
    using DomainServices;
    using Model;

    public class CalendarEventSync : ICalendarEventSync
    {
        private readonly IDevice device;
        private readonly ILogging logging;
        private readonly ICalendarEventWebHandler handler;
        private readonly ICalendarEventRepository repo;

        private readonly TimeSpan updatePauseTime = new TimeSpan(1, 0, 0);

        public CalendarEventSync(ICalendarEventWebHandler handler, ICalendarEventRepository repo, IDevice device, ILogging logging)
        {
            this.handler = handler;
            this.repo = repo;
            this.device = device;
            this.logging = logging;
        }

        public async Task<Tuple<ResultState, IEnumerable<SqlCalendarEvent>>> UpdateAsync(bool force, DateTime afterDate, CancellationToken cancellationToken)
        {
            if (!this.device.HasNetworkConnectivity)
            {
                return new Tuple<ResultState, IEnumerable<SqlCalendarEvent>>(ResultState.ErrorNetwork, null);
            }

            var storedPosts = this.repo.RetrieveEventsAfter(afterDate, 20);
            if (!force && storedPosts.Any() && storedPosts.All(entry => entry.LastUpdated > DateTime.UtcNow.Subtract(this.updatePauseTime)))
            {
                return new Tuple<ResultState, IEnumerable<SqlCalendarEvent>>(ResultState.NotModified, null);
            }

            try
            {
                var events = await this.handler.GetCalendarEventsAsync(afterDate, cancellationToken);

                if (cancellationToken.IsCancellationRequested)
                {
                    return new Tuple<ResultState, IEnumerable<SqlCalendarEvent>>(ResultState.Canceled, null);
                }

                if (events == null || !events.Any())
                {
                    if (force)
                    {
                        this.repo.DeleteEventEntries();
                    }

                    return new Tuple<ResultState, IEnumerable<SqlCalendarEvent>>(ResultState.NoData, null);
                }

                var result = this.repo.UpdateEventRange(events);

                return new Tuple<ResultState, IEnumerable<SqlCalendarEvent>>(ResultState.Success, result);
            }
            catch (Exception ex)
            {
                // most likely network connectivity problems
                this.logging.Exception(this, ex);
                return new Tuple<ResultState, IEnumerable<SqlCalendarEvent>>(ResultState.Error, null);
            }
        }
    }
}

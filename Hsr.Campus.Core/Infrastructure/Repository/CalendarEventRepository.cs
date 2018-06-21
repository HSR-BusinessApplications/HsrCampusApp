// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Infrastructure.Repository
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using DomainServices;
    using Model;
    using SQLite;

    public class CalendarEventRepository
        : ICalendarEventRepository, IDisposable
    {
        private const string DbName = "calendar.sqlite";

        private static readonly object Locking = new object();
        private readonly SQLiteConnection conn;

        private bool disposedValue;

        public CalendarEventRepository()
        {
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var dbFilePath = Path.Combine(folderPath, DbName);
            this.conn = new SQLiteConnection(dbFilePath);
            lock (Locking)
            {
                this.conn.CreateTable<SqlCalendarEvent>();
            }
        }

        public bool HasEventsAfter(DateTime afterDate)
        {
            lock (Locking)
            {
                return this.conn.Table<SqlCalendarEvent>().Any(t => t.Start > afterDate);
            }
        }

        public IEnumerable<SqlCalendarEvent> RetrieveEventsAfter(DateTime afterDate, int amount)
        {
            lock (Locking)
            {
                return this.conn.Table<SqlCalendarEvent>()
                    .Where(calendarEvent => calendarEvent.Start > afterDate)
                    .OrderBy(calendarEvent => calendarEvent.Start)
                    .Take(amount);
            }
        }

        public IEnumerable<SqlCalendarEvent> UpdateEventRange(IEnumerable<WhCalendarEvent> events)
        {
            if (!events.Any())
            {
                return Enumerable.Empty<SqlCalendarEvent>();
            }

            events = events.OrderBy(calendarEvent => calendarEvent.Start).ToList();

            var result = new List<SqlCalendarEvent>();

            lock (Locking)
            {
                var minDate = events.Min(news => news.Start);
                var maxDate = events.Max(news => news.Start);

                this.DeleteEvents(calendarEvent => calendarEvent.Start > minDate
                                        && calendarEvent.Start < maxDate);

                result.AddRange(events.Select(ConvertCalenderEvents));

                this.conn.InsertAll(result, "OR REPLACE");
            }

            return result;
        }

        public void DeleteEventEntries(DateTime beforeDate)
        {
            lock (Locking)
            {
                this.DeleteEvents(calendarEvent => calendarEvent.Start < beforeDate);
            }
        }

        public void DeleteEventEntries()
        {
            lock (Locking)
            {
                this.DeleteEvents();
            }
        }

        public void Truncate()
        {
            lock (Locking)
            {
                this.DeleteEvents();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.conn?.Close();
                }

                this.disposedValue = true;
            }
        }

        private static SqlCalendarEvent ConvertCalenderEvents(WhCalendarEvent calendarEvent)
        {
            return new SqlCalendarEvent
            {
                EventID = calendarEvent.EventID,
                Link = calendarEvent.Link,
                Summary = calendarEvent.Summary,
                Description = calendarEvent.Description,
                Location = calendarEvent.Location,
                Start = calendarEvent.Start.UtcDateTime,
                End = calendarEvent.End.UtcDateTime,
                LastUpdated = DateTime.UtcNow
            };
        }

        private void DeleteEvents(Expression<Func<SqlCalendarEvent, bool>> condition = null)
        {
            lock (Locking)
            {
                if (condition == null)
                {
                    this.conn.DeleteAll<SqlCalendarEvent>();
                }
                else
                {
                    this.conn.Table<SqlCalendarEvent>()
                        .Delete(condition);
                }
            }
        }
    }
}

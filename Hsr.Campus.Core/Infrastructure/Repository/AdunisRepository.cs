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
    using Plugin.Settings.Abstractions;
    using SQLite;

    public class AdunisRepository : IAdunisRepository, IDisposable
    {
        private const string DbName = "adunis.sqlite";

        private static readonly string LastUpdatedKey = typeof(AdunisRepository).FullName + "LastUpdated";
        private static readonly object Locking = new object();
        private readonly SQLiteConnection conn;

        private readonly ISettings settings;

        private bool disposedValue;

        public AdunisRepository(ISettings settings)
        {
            this.settings = settings;
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var dbFilePath = Path.Combine(folderPath, DbName);
            this.conn = new SQLiteConnection(dbFilePath);
            lock (Locking)
            {
                this.conn.CreateTable<SqlTimeperiod>();
                this.conn.CreateTable<SqlAdunisAppointment>();
            }
        }

        public DateTime LastUpdated
        {
            get
            {
                return this.settings.GetValueOrDefault(LastUpdatedKey, default(DateTime));
            }

            private set
            {
                this.settings.AddOrUpdateValue(LastUpdatedKey, value);
            }
        }

        public void PersistTimeperiods(IEnumerable<SqlTimeperiod> periods)
        {
            if (!periods.Any())
            {
                return;
            }

            var marker = Guid.NewGuid().ToString();

            lock (Locking)
            {
                var markedTimeperiods = new List<SqlTimeperiod>();
                foreach (var timeperiod in this.conn.Table<SqlTimeperiod>())
                {
                    timeperiod.Marker = marker;
                    markedTimeperiods.Add(timeperiod);
                }

                this.conn.UpdateAll(markedTimeperiods);

                this.conn.InsertAll(periods, "OR REPLACE");

                this.DeleteTimeperiods(timeperiod => timeperiod.Marker == marker);
            }

            this.LastUpdated = DateTime.UtcNow;
        }

        public void UpdateTimeperiod(SqlTimeperiod period)
        {
            if (period == null)
            {
                return;
            }

            lock (Locking)
            {
                this.conn.Update(period);
            }
        }

        public IEnumerable<SqlTimeperiod> RetrieveAllTimeperiods()
        {
            lock (Locking)
            {
                return this.conn.Table<SqlTimeperiod>().OrderBy(timeperiod => timeperiod.Begin);
            }
        }

        public IEnumerable<SqlTimeperiod> RetrieveNonEmptyTimeperiods()
        {
            lock (Locking)
            {
                return this.conn.Table<SqlTimeperiod>()
                    .Where(timeperiod => timeperiod.HasAppointments)
                    .OrderBy(timeperiod => timeperiod.Begin);
            }
        }

        public SqlTimeperiod RetrieveSingleTimeperiod(DateTime now)
            => this.RetrieveNonEmptyTimeperiods().FirstOrDefault(t => t.Begin <= now && t.End >= now);

        public SqlTimeperiod RetrieveTimeperiodById(int termId)
        {
            lock (Locking)
            {
                return this.conn.Table<SqlTimeperiod>()
                    .OrderBy(timeperiod => timeperiod.Begin)
                    .FirstOrDefault(timeperiod => termId == timeperiod.TermID);
            }
        }

        public void PersistAppointments(IEnumerable<SqlAdunisAppointment> appointments)
        {
            if (!appointments.Any())
            {
                return;
            }

            var first = appointments.FirstOrDefault();
            var marker = Guid.NewGuid().ToString();

            lock (Locking)
            {
                var markedAppointments = new List<SqlAdunisAppointment>();
                foreach (var appointment in this.conn.Table<SqlAdunisAppointment>()
                                                .Where(appointment => appointment.TermID == first.TermID))
                {
                    appointment.Marker = marker;
                    markedAppointments.Add(appointment);
                }

                this.conn.UpdateAll(markedAppointments);

                this.conn.InsertAll(appointments, "OR REPLACE");

                this.DeleteAppointments(appointment => appointment.Marker == marker);
            }
        }

        public IEnumerable<SqlAdunisAppointment> RetrieveAppointments(SqlTimeperiod period)
        {
            lock (Locking)
            {
                return this.conn.Table<SqlAdunisAppointment>()
                    .Where(appointment => appointment.TermID == period.TermID && appointment.Type == period.Type);
            }
        }

        public IEnumerable<SqlAdunisAppointment> RetrieveAppointmentsForDay(SqlTimeperiod period, DateTime date)
            => this.RetrieveAppointmentsForRange(period, date, date.AddDays(1));

        /// <summary>
        /// Retrieves all appointments in range (including dateStart, excluding dateEnd), ordered by StartTime.
        /// </summary>
        /// <param name="period">The Timeperiod the appointments belong to</param>
        /// <param name="dateStart">Start date (inclusive) of the returned appointments</param>
        /// <param name="dateEnd">End date (exclusive) of the returned appointments</param>
        /// <returns>List of all appointments which correspond to the parameters</returns>
        public IEnumerable<SqlAdunisAppointment> RetrieveAppointmentsForRange(SqlTimeperiod period, DateTime dateStart, DateTime dateEnd)
        {
            if (period == null)
            {
                throw new ArgumentNullException(nameof(period));
            }

            lock (Locking)
            {
                return this.conn.Table<SqlAdunisAppointment>()
                    .Where(appointment => appointment.TermID == period.TermID
                                        && appointment.StartTime >= dateStart
                                        && appointment.StartTime < dateEnd)
                    .OrderBy(appointment => appointment.StartTime)
                    .ToList();
            }
        }

        public void DeleteAppointments(SqlTimeperiod period)
        {
            lock (Locking)
            {
                this.DeleteAppointments(appointment => appointment.TermID == period.TermID && appointment.Type == period.Type);
            }
        }

        public void Truncate()
        {
            lock (Locking)
            {
                this.DeleteTimeperiods();
                this.DeleteAppointments();
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

        private void DeleteTimeperiods(Expression<Func<SqlTimeperiod, bool>> condition = null)
        {
            lock (Locking)
            {
                    var oldTimeperiods = condition == null
                        ? this.conn.Table<SqlTimeperiod>()
                        : this.conn.Table<SqlTimeperiod>().Where(condition);

                    foreach (var oldTimeperiod in oldTimeperiods)
                    {
                        this.DeleteAppointments(appointment => appointment.TermID == oldTimeperiod.TermID && appointment.Type == oldTimeperiod.Type);
                    }

                if (condition == null)
                {
                    this.conn.DeleteAll<SqlTimeperiod>();
                }
                else
                {
                    this.conn.Table<SqlTimeperiod>()
                        .Delete(condition);
                }

                if (!this.conn.Table<SqlTimeperiod>().Any())
                {
                    this.LastUpdated = DateTime.MinValue;
                }
            }
        }

        private void DeleteAppointments(Expression<Func<SqlAdunisAppointment, bool>> condition = null)
        {
            lock (Locking)
            {
                if (condition == null)
                {
                    this.conn.DeleteAll<SqlAdunisAppointment>();
                }
                else
                {
                    this.conn.Table<SqlAdunisAppointment>()
                        .Delete(condition);
                }
            }
        }
    }
}

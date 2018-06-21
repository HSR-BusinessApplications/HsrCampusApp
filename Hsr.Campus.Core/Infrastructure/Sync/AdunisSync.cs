// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Infrastructure.Sync
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ApplicationServices;
    using DomainServices;
    using Hsr.Campus.Core.OAuth;
    using Model;

    public class AdunisSync : IAdunisSync
    {
        private const int SpecialTimeSlotId = 1700;

        private readonly ILogging logging;
        private readonly IDevice device;
        private readonly IAccountService account;
        private readonly IAdunisWebHandler handler;
        private readonly IAdunisRepository repo;

        public AdunisSync(IAdunisWebHandler handler, IAdunisRepository repo, IDevice device, IAccountService account, ILogging logging)
        {
            this.handler = handler;
            this.repo = repo;
            this.device = device;
            this.account = account;
            this.logging = logging;
        }

        public async Task<ResultState> UpdateAsync(bool force, CancellationToken cancellationToken)
        {
            if (!force && this.repo.LastUpdated > DateTime.UtcNow.Date)
            {
                return ResultState.NotModified;
            }

            if (!this.account.HasAccount)
            {
                return ResultState.OAuthExpired;
            }

            if (!this.device.HasNetworkConnectivity)
            {
                return ResultState.ErrorNetwork;
            }

            var identity = this.account.Retrieve().UserName;

            try
            {
                var periodData = await this.handler.GetTimeperiodsAsync(identity, cancellationToken);

                if (cancellationToken.IsCancellationRequested)
                {
                    return ResultState.Canceled;
                }

                if (periodData == null || !periodData.Any())
                {
                    this.repo.Truncate();
                    return ResultState.NoData;
                }

                this.repo.PersistTimeperiods(this.ConvertTimeperiods(periodData, identity));

                var timeperiods = force
                    ? this.repo.RetrieveAllTimeperiods()
                    : this.repo.RetrieveAllTimeperiods().Where(tp => tp.LastAppointmentUpdate == default(DateTime));

                foreach (var period in timeperiods)
                {
                    var state = await this.UpdateAppointmentsAsync(true, period, cancellationToken);
                    switch (state)
                    {
                        case ResultState.Error:
                        case ResultState.ErrorNetwork:
                        case ResultState.OAuthExpired:
                        case ResultState.Canceled:
                            return state;
                        case ResultState.Success:
                        case ResultState.NotModified:
                        case ResultState.NoData:
                            break;
                        default:
                            Debug.Assert(false, $"ResultState: {state}, not handled");
                            break;
                    }
                }

                return ResultState.Success;
            }
            catch (OAuthExpiredException)
            {
                return ResultState.OAuthExpired;
            }
            catch (Exception ex)
            {
                // most likely network connectivity problems
                this.logging.Exception(this, ex);
                return ResultState.Error;
            }
        }

        public async Task<ResultState> UpdateAppointmentsAsync(bool force, SqlTimeperiod period, CancellationToken cancellationToken)
        {
            if (!force && period.LastAppointmentUpdate > DateTime.UtcNow.Date)
            {
                return ResultState.NotModified;
            }

            if (!this.account.HasAccount)
            {
                return ResultState.OAuthExpired;
            }

            if (!this.device.HasNetworkConnectivity)
            {
                return ResultState.ErrorNetwork;
            }

            if (period.Type != AdunisType.Timetable && period.Type != AdunisType.Exam)
            {
                return ResultState.NoData;
            }

            try
            {
                var courseData = await this.handler.GetCourseDataAsync(period.Identity, period.ParentTermID, period.Type, cancellationToken);

                if (cancellationToken.IsCancellationRequested)
                {
                    return ResultState.Canceled;
                }

                var appointments = new List<SqlAdunisAppointment>();
                var uniqueDays = new HashSet<DayOfWeek>();

                if (courseData == null || !courseData.Any())
                {
                    this.repo.DeleteAppointments(period);

                    period.SetUniqueDays(uniqueDays);
                    period.HasAppointments = false;
                    period.LastAppointmentUpdate = DateTime.UtcNow;

                    this.repo.UpdateTimeperiod(period);
                    return ResultState.NoData;
                }

                foreach (var course in courseData)
                {
                    foreach (var allocation in course.CourseAllocations)
                    {
                        if (allocation.TimeslotId == SpecialTimeSlotId)
                        {
                            // courses without appointments (SA, BA, Challenge-Projekt)
                            appointments.Add(ConvertDivAppointment(period, course, period.Type));
                        }
                        else
                        {
                            appointments.AddRange(
                                allocation.Appointments.Select(
                                    appointment => ConvertAppointment(period, course, appointment, period.Type)));

                            if (allocation.DayOfWeek.HasValue)
                            {
                                uniqueDays.Add(allocation.DayOfWeek.Value);
                            }
                        }
                    }
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    return ResultState.Canceled;
                }

                this.repo.PersistAppointments(appointments);

                period.SetUniqueDays(uniqueDays.OrderBy(week => ((int)week + 6) % 7));
                period.HasAppointments = appointments.Count > 0;
                period.LastAppointmentUpdate = DateTime.UtcNow;

                this.repo.UpdateTimeperiod(period);

                return ResultState.Success;
            }
            catch (OAuthExpiredException)
            {
                return ResultState.OAuthExpired;
            }
            catch (Exception ex)
            {
                // most likely network connectivity problems
                this.logging.Exception(this, ex);
                return ResultState.Error;
            }
        }

        private static SqlAdunisAppointment ConvertDivAppointment(SqlTimeperiod period, WhAdunisCourse course, AdunisType type)
        {
            // special handling of modules without appointments
            var start = period.Begin.AddDays(-7).SyncWeekday(DayOfWeek.Sunday);

            var appointment = new SqlAdunisAppointment
            {
                Type = type,
                CourseName = course.Name,
                StartTime = start.Date,
                EndTime = period.End,
                TermID = period.TermID
            };

            appointment.SetId();

            return appointment;
        }

        private static SqlAdunisAppointment ConvertAppointment(SqlTimeperiod period, WhAdunisCourse course, WhAdunisAppointment aAppointment, AdunisType type)
        {
            var appointment = new SqlAdunisAppointment
            {
                Type = type,
                CourseName = course.Name,
                StartTime = aAppointment.StartTime,
                EndTime = aAppointment.EndTime,
                TermID = period.TermID
            };

            if (aAppointment.AppointmentRooms != null)
            {
                appointment.AppointmentRooms = string.Join(", ", aAppointment.AppointmentRooms);
            }

            if (aAppointment.Lecturers != null)
            {
                appointment.Lecturers = string.Join(", ", aAppointment.Lecturers);
            }

            appointment.SetId();

            return appointment;
        }

        private IEnumerable<SqlTimeperiod> ConvertTimeperiods(IEnumerable<WhTimeperiod> adunisTimeperiods, string identity)
        {
            return from parent in adunisTimeperiods
                where parent.Children != null
                from period in parent.Children
                let oldTimeperiod = this.repo.RetrieveTimeperiodById(period.TermID)
                where (AdunisType)period.Type != AdunisType.ExamPreparation
                select new SqlTimeperiod
                {
                    TermID = period.TermID,
                    ParentTermID = parent.TermID,
                    Begin = period.Begin,
                    End = period.End,
                    HasAppointments = oldTimeperiod?.HasAppointments ?? false,
                    Type = (AdunisType)period.Type,
                    Identity = identity,
                    LastAppointmentUpdate = oldTimeperiod?.LastAppointmentUpdate ?? default(DateTime),
                    UniqueDaysOfWeekSerialized = oldTimeperiod?.UniqueDaysOfWeekSerialized ?? string.Empty,
                    Name = $"{parent.Name} - {period.Name}"
                };
        }
    }
}

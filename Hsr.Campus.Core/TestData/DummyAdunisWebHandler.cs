// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

// The classes with test data must only be compiled in the Test-Build!
#if TEST_DATA
namespace Hsr.Campus.Core.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using DomainServices;
    using Model;
    using Newtonsoft.Json;

    public class DummyAdunisWebHandler : IAdunisWebHandler
    {
        private int appointmentCallCounter;
        private int timeperiodCallCounter;

        public Task<IEnumerable<WhTimeperiod>> GetTimeperiodsAsync(
            string identity,
            CancellationToken cancellationToken)
        {
            this.timeperiodCallCounter++;
            return Task<IEnumerable<WhTimeperiod>>.Factory.StartNew(
                () => this.timeperiodCallCounter % 3 == 0
                    ? null
                    : JsonConvert.DeserializeObject<IEnumerable<WhTimeperiod>>(TimetableTestData.TpData),
                cancellationToken);
        }

        public Task<IEnumerable<WhAdunisCourse>> GetCourseDataAsync(string identity, int termId, AdunisType type, CancellationToken cancellationToken)
        {
            string content;
            if (type == AdunisType.Timetable)
            {
                switch (termId)
                {
                    case 773:
                        content = TimetableTestData.TimetableData773;
                    break;
                    case 774:
                        content = TimetableTestData.TimetableData774;
                        break;
                    default:
                        content = string.Empty;
                    break;
                }
            }
            else
            {
                switch (termId)
                {
                    case 773:
                        content = TimetableTestData.ExamData;
                        break;
                    default:
                        content = string.Empty;
                        break;
                }
            }

            this.appointmentCallCounter++;
            var delay = this.appointmentCallCounter % 14 == 0;

            return Task<IEnumerable<WhAdunisCourse>>.Factory.StartNew(
                () =>
                {
                    if (delay)
                    {
                        Task.Delay(TimeSpan.FromSeconds(5), cancellationToken).Wait(cancellationToken);
                    }

                    return content?.Length == 0 ? null : JsonConvert.DeserializeObject<Holder>(content).Courses;
                }, cancellationToken);
        }

        private class Holder
        {
            public IEnumerable<WhAdunisCourse> Courses { get; set; }

            public string Person { get; set; }

            public string Semester { get; set; }
        }
    }
}
#endif

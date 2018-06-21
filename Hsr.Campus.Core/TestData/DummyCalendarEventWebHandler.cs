// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

// The classes with test data must only be compiled in the Test-Build!
#if TEST_DATA
namespace Hsr.Campus.Core.TestData
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ApplicationServices;
    using Infrastructure.WebHandler;

    public class DummyCalendarEventWebHandler : CalendarEventWebHandler
    {
        public DummyCalendarEventWebHandler(IDevice device, IServiceApi serviceApi, IHttpClientConfiguration httpClientConfiguration)
            : base(device, serviceApi, httpClientConfiguration)
        {
        }

        protected override Task<string> GetCalendarEventsJsonAsync(DateTime afterDate, CancellationToken cancellationToken)
        {
            string content;
            if (afterDate.Date == DateTime.Now.Date)
            {
                content = CalendarTestData.CalendarFeed1;
            }
            else if (afterDate > DateTimeOffset.Parse("2018-10-05T00:00:00Z") && afterDate < DateTimeOffset.Parse("2018-10-06T19:00:00+02:00"))
            {
                content = CalendarTestData.CalendarFeed2;
            }
            else if (afterDate >= DateTimeOffset.Parse("2018-10-06T19:00:00+02:00") && afterDate < DateTimeOffset.Parse("2018-10-11T12:00:00+02:00"))
            {
                content = CalendarTestData.CalendarFeed3;
            }
            else
            {
                content = "[]";
            }

            return Task<string>.Factory.StartNew(() => content, cancellationToken);
        }
    }
}
#endif

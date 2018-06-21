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

    public class DummyMenuWebHandler : MenuWebHandler
    {
        public DummyMenuWebHandler(IDevice device, IServiceApi serviceApi, IHttpClientConfiguration httpClientConfiguration)
            : base(device, serviceApi, httpClientConfiguration)
        {
        }

        protected override Task<string> GetJsonFeedsAsync(CancellationToken cancellationToken)
        {
            return Task<string>.Factory.StartNew(() => MenuTestData.Feeds, cancellationToken);
        }

        protected override Task<string> GetJsonMenuEntriesAsync(string feedId, DateTime dayDate, CancellationToken cancellationToken)
        {
            var content = string.Empty;

            switch (feedId)
            {
                case "mensa":
                    if (dayDate == DateTime.Today.AddDays(-1))
                    {
                        content = MenuTestData.MensaYesterday;
                    }
                    else if (dayDate == DateTime.Today)
                    {
                        content = MenuTestData.MensaToday;
                    }
                    else if (dayDate == DateTime.Today.AddDays(1))
                    {
                        content = MenuTestData.MensaTomorrow;
                    }
                    else if (dayDate == DateTime.Today.AddDays(3))
                    {
                        content = MenuTestData.MensaIn3Days;
                    }

                    break;
                case "bistro":
                    if (dayDate == DateTime.Today.AddDays(-1))
                    {
                        content = MenuTestData.BistroYesterday;
                    }
                    else if (dayDate == DateTime.Today)
                    {
                        content = MenuTestData.BistroToday;
                    }
                    else if (dayDate == DateTime.Today.AddDays(2))
                    {
                        content = MenuTestData.BistroAfterTomorrow;
                    }
                    else if (dayDate == DateTime.Today.AddDays(3))
                    {
                        content = MenuTestData.BistroIn3Days;
                    }

                    break;
            }

            return Task<string>.Factory.StartNew(() => content);
        }
    }
}
#endif

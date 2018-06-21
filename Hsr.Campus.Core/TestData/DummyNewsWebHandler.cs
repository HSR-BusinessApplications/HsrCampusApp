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
    using Model;

    public class DummyNewsWebHandler
        : NewsWebHandler
    {
        private int callCounter;

        public DummyNewsWebHandler(IDevice device, IServiceApi serviceApi, IHttpClientConfiguration httpClientConfiguration)
            : base(device, serviceApi, httpClientConfiguration)
        {
        }

        protected override Task<string> GetJsonFeedsAsync(CancellationToken cancellationToken)
        {
            return Task<string>.Factory.StartNew(() => NewsTestData.Feeds, cancellationToken);
        }

        protected override Task<byte[]> GetByteArrayIconAsync(SqlNewsFeed feed, int size)
        {
            switch (size)
            {
                case 25:
                    return Task<byte[]>.Factory.StartNew(() => NewsTestData.IconHsr25);
                case 50:
                    return Task<byte[]>.Factory.StartNew(() => NewsTestData.IconHsr50);
                case 75:
                    return Task<byte[]>.Factory.StartNew(() => NewsTestData.IconHsr75);
                default:
                    return Task<byte[]>.Factory.StartNew(() => null);
            }
        }

        /// <summary>
        /// Gibt je nach Parametern vordefinierte Werte zum Testen zurück. Jeder 2. Aufruf wird um 5 Sekunend verzögert.
        /// </summary>
        /// <param name="feed">ID des Feeds.</param>
        /// <param name="before">maximales Datum des neusten Eintrags, der zurückgegeben werden soll.</param>
        /// <param name="cancellationToken">token der zum Abbrechen des vorgangs verwendet werden könnte.</param>
        /// <returns>JSON-String mit den News Einträgen.</returns>
        protected override Task<string> GetJsonNewsEntriesAsync(SqlNewsFeed feed, DateTime before, CancellationToken cancellationToken)
        {
            string content;
            if (feed.Key == "vshsrTest" && before > DateTime.Parse("2016-09-12T11:10:50+02:00"))
            {
                content = NewsTestData.Feed1344Part1;
            }
            else if (feed.Key == "vshsrTest" && before > DateTime.Parse("2016-05-06T17:15:07+02:00"))
            {
                content = NewsTestData.Feed1344Part2;
            }
            else if (feed.Key == "hsrsportTest" && before > DateTime.Parse("2014-05-26T16:20:10+02:00"))
            {
                content = NewsTestData.Feed3058Part1;
            }
            else if (feed.Key == "hsrsportTest" && before > DateTime.Parse("2014-03-24T21:56:19+01:00"))
            {
                content = NewsTestData.Feed3058Part2;
            }
            else
            {
                content = "[]";
            }

            this.callCounter++;
            var delay = this.callCounter % 2 == 0;

            return Task<string>.Factory.StartNew(() =>
                {
                    if (delay)
                    {
                        Task.Delay(TimeSpan.FromSeconds(5)).Wait();
                    }

                    return content;
                });
        }
    }
}
#endif

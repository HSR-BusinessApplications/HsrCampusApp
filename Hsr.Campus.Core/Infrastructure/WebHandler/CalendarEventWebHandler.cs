// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Infrastructure.WebHandler
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using ApplicationServices;
    using DomainServices;
    using Model;
    using Newtonsoft.Json;

    public class CalendarEventWebHandler : ICalendarEventWebHandler
    {
        private readonly IDevice device;
        private readonly IServiceApi serviceApi;
        private readonly IHttpClientConfiguration httpClientConfiguration;

        public CalendarEventWebHandler(IDevice device, IServiceApi serviceApi, IHttpClientConfiguration httpClientConfiguration)
        {
            this.device = device;
            this.serviceApi = serviceApi;
            this.httpClientConfiguration = httpClientConfiguration;
        }

        public async Task<IEnumerable<WhCalendarEvent>> GetCalendarEventsAsync(DateTime afterDate, CancellationToken cancellationToken)
        {
            var response = await this.GetCalendarEventsJsonAsync(afterDate, cancellationToken);

            return cancellationToken.IsCancellationRequested ? null : response.CreateFromJsonString<IEnumerable<WhCalendarEvent>>();
        }

        protected virtual Task<string> GetCalendarEventsJsonAsync(DateTime afterDate, CancellationToken cancellationToken)
        {
            var webClient = new NativeHttpClient(this.httpClientConfiguration);

            webClient
                .AssignAgent(this.device)
                .AssignAcceptTypeJson()
                .AssignAuthToken(this.serviceApi.CalendarEvent);

            return webClient.GetStringAsync(string.Format(this.serviceApi.CalendarUri, afterDate), cancellationToken);
        }
    }
}

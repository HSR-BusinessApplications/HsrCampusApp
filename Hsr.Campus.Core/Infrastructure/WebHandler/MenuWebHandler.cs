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

    public class MenuWebHandler : IMenuWebHandler
    {
        private readonly IDevice device;
        private readonly IServiceApi serviceApi;
        private readonly IHttpClientConfiguration httpClientConfiguration;

        public MenuWebHandler(IDevice device, IServiceApi serviceApi, IHttpClientConfiguration httpClientConfiguration)
        {
            this.device = device;
            this.serviceApi = serviceApi;
            this.httpClientConfiguration = httpClientConfiguration;
        }

        public async Task<IEnumerable<WhMenuFeed>> GetFeedsAsync(CancellationToken cancellationToken)
        {
            var response = await this.GetJsonFeedsAsync(cancellationToken);

            return cancellationToken.IsCancellationRequested ? null : JsonConvert.DeserializeObject<IEnumerable<WhMenuFeed>>(response);
        }

        public async Task<string> GetMenuHtmlAsync(string feedId, DateTime dayDate, CancellationToken cancellationToken)
        {
            var response = await this.GetJsonMenuEntriesAsync(feedId, dayDate, cancellationToken);

            return cancellationToken.IsCancellationRequested ? null : response;
        }

        protected virtual Task<string> GetJsonFeedsAsync(CancellationToken cancellationToken)
        {
            var webClient = new NativeHttpClient(this.httpClientConfiguration);

            webClient
                .AssignAgent(this.device)
                .AssignAcceptTypeJson()
                .AssignAuthToken(this.serviceApi.Menu);

            return webClient.GetStringAsync(this.serviceApi.MenuFeedsUri, cancellationToken);
        }

        protected virtual Task<string> GetJsonMenuEntriesAsync(string feedId, DateTime dayDate, CancellationToken cancellationToken)
        {
            var webClient = new NativeHttpClient(this.httpClientConfiguration);

            webClient
                .AssignAgent(this.device)
                .AssignAcceptTypeJson()
                .AssignAuthToken(this.serviceApi.Menu);

            return webClient.GetStringAsync(string.Format(this.serviceApi.MenuUri, feedId, dayDate), cancellationToken);
        }
    }
}

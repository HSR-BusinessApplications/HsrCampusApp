// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Infrastructure.WebHandler
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using ApplicationServices;
    using DomainServices;
    using Model;
    using Newtonsoft.Json;

    public class MapWebHandler
        : IMapWebHandler
    {
        private readonly IDevice device;
        private readonly IServiceApi serviceApi;
        private readonly IHttpClientConfiguration httpClientConfiguration;

        public MapWebHandler(IDevice device, IServiceApi serviceApi, IHttpClientConfiguration httpClientConfiguration)
        {
            this.device = device;
            this.serviceApi = serviceApi;
            this.httpClientConfiguration = httpClientConfiguration;
        }

        public virtual async Task<MapOverview> GetHashesAsync(CancellationToken cancellationToken)
        {
            var webClient = new NativeHttpClient(this.httpClientConfiguration);

            webClient
                .AssignAgent(this.device)
                .AssignAcceptTypeJson()
                .AssignAuthToken(this.serviceApi.Map);

            var response = await webClient.GetStringAsync(this.serviceApi.BuildingsUri, cancellationToken);

            return response.CreateFromJsonString<MapOverview>();
        }

        public virtual Task<Stream> GetImageStreamAsync(Guid id)
        {
            var webClient = new NativeHttpClient(this.httpClientConfiguration);

            webClient
                .AssignAgent(this.device)
                .AssignAcceptTypeJson()
                .AssignAuthToken(this.serviceApi.Map);

            return webClient.GetStreamAsync(this.serviceApi.ImageUriBase + id);
        }
    }
}

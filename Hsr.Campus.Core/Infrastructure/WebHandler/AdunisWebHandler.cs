// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Infrastructure.WebHandler
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using ApplicationServices;
    using DomainServices;
    using Hsr.Campus.Core.OAuth;
    using Model;
    using Newtonsoft.Json;

    public class AdunisWebHandler
        : IAdunisWebHandler
    {
        private readonly IDevice device;
        private readonly IOAuthUtils oAuth;
        private readonly IHttpClientConfiguration httpClientConfiguration;
        private readonly IServiceApi serviceApi;

        public AdunisWebHandler(IDevice device, IOAuthUtils oAuth, IHttpClientConfiguration httpClientConfiguration, IServiceApi serviceApi)
        {
            this.device = device;
            this.oAuth = oAuth;
            this.httpClientConfiguration = httpClientConfiguration;
            this.serviceApi = serviceApi;
        }

        public async Task<IEnumerable<WhTimeperiod>> GetTimeperiodsAsync(string identity, CancellationToken cancellationToken)
        {
            var webClient = new NativeHttpClient(this.httpClientConfiguration);

            webClient
                .AssignAgent(this.device)
                .AssignAcceptTypeJson()
                .AssignBearerToken(await this.oAuth.BearerTokenAsync());

            var response = await webClient.GetStringAsync(string.Format(this.serviceApi.TimeperiodUri, identity), cancellationToken);

            return cancellationToken.IsCancellationRequested ? null : response.CreateFromJsonString<IEnumerable<WhTimeperiod>>();
        }

        public async Task<IEnumerable<WhAdunisCourse>> GetCourseDataAsync(string identity, int termId, AdunisType type, CancellationToken cancellationToken)
        {
            var requestUrlTemplate = type == AdunisType.Timetable ? this.serviceApi.TimetableUri : this.serviceApi.ExamUri;
            var requestUrl = string.Format(requestUrlTemplate, identity, termId);

            var webClient = new NativeHttpClient(this.httpClientConfiguration);

            webClient
                .AssignAgent(this.device)
                .AssignAcceptTypeJson()
                .AssignBearerToken(await this.oAuth.BearerTokenAsync());

            var response = await webClient.GetStringAsync(requestUrl, cancellationToken);

            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            var holder = response.CreateFromJsonString<Holder>();

            return holder?.Courses;
        }

        private class Holder
        {
            public IEnumerable<WhAdunisCourse> Courses { get; set; }
        }
    }
}

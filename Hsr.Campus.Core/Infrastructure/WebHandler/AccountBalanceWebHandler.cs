// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Infrastructure.WebHandler
{
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using ApplicationServices;
    using DomainServices;
    using Hsr.Campus.Core.OAuth;
    using Model;
    using Newtonsoft.Json;

    public class AccountBalanceWebHandler : IAccountBalanceWebHandler
    {
        private readonly IDevice device;
        private readonly IOAuthUtils oAuth;
        private readonly IHttpClientConfiguration httpClientConfiguration;
        private readonly IServiceApi serviceApi;

        public AccountBalanceWebHandler(IDevice device, IOAuthUtils oAuth, IHttpClientConfiguration httpClientConfiguration, IServiceApi serviceApi)
        {
            this.device = device;
            this.oAuth = oAuth;
            this.httpClientConfiguration = httpClientConfiguration;
            this.serviceApi = serviceApi;
        }

        public async Task<AccountBalance> GetCurrentStateAsync(CancellationToken cancellationToken)
        {
            var response = await this.GetBalanceJsonAsync(cancellationToken);

            return cancellationToken.IsCancellationRequested ? null : response.CreateFromJsonString<AccountBalance>();
        }

        protected virtual async Task<string> GetBalanceJsonAsync(CancellationToken cancellationToken)
        {
            var webClient = new NativeHttpClient(this.httpClientConfiguration);

            webClient
                .AssignAgent(this.device)
                .AssignBearerToken(await this.oAuth.BearerTokenAsync())
                .AssignAcceptTypeJson();

            return await webClient.GetStringAsync(this.serviceApi.AccountApiUri, cancellationToken);
        }
    }
}

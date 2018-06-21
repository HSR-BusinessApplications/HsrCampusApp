// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Infrastructure.WebHandler
{
    using System.Collections.Generic;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Threading;
    using System.Threading.Tasks;
    using ApplicationServices;
    using DomainServices;
    using Hsr.Campus.Core.OAuth;
    using Model;
    using Newtonsoft.Json;
    using ViewModels;

    public class FilerWebHandler : IFilerWebHandler
    {
        private readonly IDevice device;
        private readonly IAccountService account;
        private readonly IOAuthUtils oAuth;
        private readonly IHttpClientConfiguration httpClientConfiguration;
        private readonly IServiceApi serviceApi;

        public FilerWebHandler(IDevice device, IAccountService account, IOAuthUtils oAuth, IHttpClientConfiguration httpClientConfiguration, IServiceApi serviceApi)
        {
            this.device = device;
            this.account = account;
            this.oAuth = oAuth;
            this.httpClientConfiguration = httpClientConfiguration;
            this.serviceApi = serviceApi;
        }

        public async Task<IEnumerable<IOListing>> GetDirectoryListingAsync(FilerViewModel.FilerArgs args, CancellationToken cancellationToken)
        {
            if (!this.account.HasAccount)
            {
                return null;
            }

            var response = await this.GetDirectoryListingJsonAsync(args, cancellationToken);

            return cancellationToken.IsCancellationRequested ? null : response.CreateFromJsonString<Holding>().Data;
        }

        protected virtual async Task<string> GetDirectoryListingJsonAsync(FilerViewModel.FilerArgs args, CancellationToken cancellationToken)
        {
            var webClient = new NativeHttpClient(this.httpClientConfiguration);

            webClient
                .AssignAgent(this.device)
                .AssignAcceptTypeJson()
                .AssignBearerToken(await this.oAuth.BearerTokenAsync());

            return await webClient.GetStringAsync(this.serviceApi.FilerApiUriBase + args.CurrentDirectory, cancellationToken);
        }

        private class Holding
        {
            [DataMember(Name = "data")]
            public IEnumerable<IOListing> Data { get; set; }
        }
    }
}

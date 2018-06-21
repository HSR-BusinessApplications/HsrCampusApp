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
    using Hsr.Campus.Core.OAuth;
    using Infrastructure.WebHandler;

    public class DummyAccountBalanceWebHandler : AccountBalanceWebHandler
    {
        public DummyAccountBalanceWebHandler(IDevice device, IOAuthUtils oAuth, IHttpClientConfiguration httpClientConfiguration, IServiceApi serviceApi)
            : base(device, oAuth, httpClientConfiguration, serviceApi)
        {
        }

        protected override Task<string> GetBalanceJsonAsync(CancellationToken cancellationToken)
        {
            return Task<string>.Factory.StartNew(() => $"{{\"badgeSaldo\":{DateTime.Now:m.ss5}}}", cancellationToken);
        }
    }
}
#endif

// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.PlatformServices
{
    using System.Net;
    using System.Net.Http;
    using Core.ApplicationServices;
    using ModernHttpClient;

    public class HttpClientConfiguration
        : IHttpClientConfiguration
    {
        public HttpMessageHandler MessageHandler => new NativeMessageHandler();

        public void RegisterServerCertificateValidationCallback()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) =>
            {
                System.Diagnostics.Debug.WriteLine("Callback Server Certificate: " + sslPolicyErrors);

                foreach (var el in chain.ChainElements)
                {
                    System.Diagnostics.Debug.WriteLine(el.Certificate.GetCertHashString());
                    System.Diagnostics.Debug.WriteLine(el.Information);
                }

                return true;
            };
        }
    }
}

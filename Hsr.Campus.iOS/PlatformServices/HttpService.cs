// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS.PlatformServices
{
    using Hsr.Campus.Core.ApplicationServices;
    using ModernHttpClient;

    public class HttpService
        : IHttpClientConfiguration
    {
        public System.Net.Http.HttpMessageHandler MessageHandler => new NativeMessageHandler();

        public void RegisterServerCertificateValidationCallback()
        {
        }
    }
}

// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS.PlatformServices
{
    using Hsr.Campus.Core.ApplicationServices;

    public class ApiTokenService
        : IAuth
    {
        public string CalendarEvent => "1d3a4be4-5350-4af3-ba53-e97fcc4ee8ff";

        public string News => "cc58a938-ea31-498b-bcd0-de3ce8ad0a1a";

        public string Map => "9802c402-f815-4169-bf92-f80d092d2519";

        public string Menu => "fdb5c4d0-0780-4bdf-8d32-6524e116089c";

        public string Person => "e086c4af-05b6-4597-9b0c-6f6e6bb29612";
    }
}

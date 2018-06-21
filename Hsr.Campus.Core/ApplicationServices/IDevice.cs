// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ApplicationServices
{
    public interface IDevice
    {
        string Version { get; }

        string Model { get; }

        DevicePlatform Platform { get; }

        int TapIconSize { get; }

        bool HasNetworkConnectivity { get; }
    }
}

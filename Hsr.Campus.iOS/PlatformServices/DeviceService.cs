// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS.PlatformServices
{
    using System;
    using Core.ApplicationServices;
    using Plugin.Settings.Abstractions;
    using UIKit;
    using Utils;

    public class DeviceService : IDevice
    {
        public string Version => UIDevice.CurrentDevice.SystemVersion;

        public string Model => UIDevice.CurrentDevice.Model;

        public DevicePlatform Platform => DevicePlatform.iOS;

        public int TapIconSize => (int)(UIScreen.MainScreen.Scale * 25);

        public bool HasNetworkConnectivity => Reachability.InternetConnectionStatus() != NetworkStatus.NotReachable;
    }
}

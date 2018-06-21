// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.PlatformServices
{
    using System;
    using Android.Graphics;
    using Android.Net;
    using Android.OS;
    using Core.ApplicationServices;
    using MvvmCross.Platform;
    using MvvmCross.Platform.Droid.Platform;
    using Plugin.Settings.Abstractions;

    public class DeviceService : IDevice
    {
        public string Version => Build.VERSION.Release;

        public string Model => "{0}/{1}".FormatWith(Build.Manufacturer, Build.Model);

        public DevicePlatform Platform => DevicePlatform.Android;

        public int TapIconSize => 25;

        public bool HasNetworkConnectivity
        {
            get
            {
                var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
                var cm = ConnectivityManager.FromContext(activity.Activity);
                var activeNetwork = cm.ActiveNetworkInfo;

                return activeNetwork?.IsConnectedOrConnecting == true;
            }
        }
    }
}

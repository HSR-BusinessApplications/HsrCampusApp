// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.Collections.Generic;
    using Core;
    using Core.ApplicationServices;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.iOS.Platform;
    using MvvmCross.iOS.Views;
    using MvvmCross.iOS.Views.Presenters;
    using MvvmCross.Platform;
    using MvvmCross.Platform.Plugins;
    using MvvmCross.Plugins.DownloadCache;
    using PlatformServices;
    using UIKit;

    public class Setup : MvxIosSetup
    {
        public Setup(IMvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }

        public Setup(IMvxApplicationDelegate applicationDelegate, IMvxIosViewPresenter presenter)
            : base(applicationDelegate, presenter)
        {
        }

        protected override List<Type> ValueConverterHolders
        {
            get
            {
                var list = base.ValueConverterHolders;
                list.Add(typeof(Converters));
                return list;
            }
        }

        protected override void InitializePlatformServices()
        {
            base.InitializePlatformServices();

            Mvx.RegisterType<IAccountService, AccountService>();
            Mvx.RegisterType<IUserInteractionService, UserInteractionService>();
            Mvx.RegisterType<IIOCacheService, IioCacheService>();
            Mvx.RegisterType<IIOStorageService, IioStorageService>();
            Mvx.RegisterType<ILogging, LogService>();
            Mvx.RegisterType<IDevice, DeviceService>();
            Mvx.RegisterType<IHttpClientConfiguration, HttpService>();
            Mvx.RegisterType<IMvxHttpFileDownloader, MvxNativeHttpFileDownloader>();
        }

        protected override IMvxIosViewsContainer CreateIosViewsContainer() => new MvxStoryBoardViewsContainer();

        protected override IMvxApplication CreateApp() => new App();
    }
}

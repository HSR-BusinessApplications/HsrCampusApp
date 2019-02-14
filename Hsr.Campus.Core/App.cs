// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core
{
    using ApplicationServices;
    using DomainServices;
    using Hsr.Campus.Core.OAuth;
    using Infrastructure.Repository;
    using Infrastructure.Sync;
    using Infrastructure.WebHandler;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.Platform;
    using Plugin.Settings;

    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.RegisterType<IMenuRepository, MenuRepository>();
            Mvx.RegisterType<IMenuSync, MenuSync>();

            Mvx.RegisterType<IAccountBalanceRepository, AccountBalanceRepository>();
            Mvx.RegisterType<IAccountBalanceSync, AccountBalanceSync>();

            Mvx.RegisterType<INewsRepository, NewsRepository>();
            Mvx.RegisterType<INewsSync, NewsSync>();

#if TEST_DATA
            Mvx.RegisterType<IMenuWebHandler, TestData.DummyMenuWebHandler>();
            Mvx.RegisterType<IAccountBalanceWebHandler, TestData.DummyAccountBalanceWebHandler>();
            Mvx.RegisterType<IAdunisWebHandler, TestData.DummyAdunisWebHandler>();
            Mvx.RegisterType<IMapWebHandler, TestData.DummyMapWebHandler>();
            Mvx.RegisterType<INewsWebHandler, TestData.DummyNewsWebHandler>();
            Mvx.RegisterType<IFilerWebHandler, TestData.DummyFilerWebHandler>();
            Mvx.RegisterType<IServiceApi, TestData.DummyServiceApi>();
            Mvx.RegisterType<IOAuthUtils, TestData.DummyOAuthUtils>();
#else
            Mvx.RegisterType<IMenuWebHandler, MenuWebHandler>();
            Mvx.RegisterType<IAccountBalanceWebHandler, AccountBalanceWebHandler>();
            Mvx.RegisterType<IAdunisWebHandler, AdunisWebHandler>();
            Mvx.RegisterType<IMapWebHandler, MapWebHandler>();
            Mvx.RegisterType<INewsWebHandler, NewsWebHandler>();
            Mvx.RegisterType<IFilerWebHandler, FilerWebHandler>();
            Mvx.RegisterType<IServiceApi, ServiceApi>();
            Mvx.RegisterType<IOAuthUtils, OAuthUtils>();
#endif
            Mvx.RegisterType<IAdunisRepository, AdunisRepository>();
            Mvx.RegisterType<IAdunisSync, AdunisSync>();

            Mvx.RegisterType<IMapRepository, MapRepository>();
            Mvx.RegisterType<IMapSync, MapSync>();

            Mvx.RegisterSingleton(CrossSettings.Current);

            this.RegisterCustomAppStart<CustomAppStart>();
        }
    }
}

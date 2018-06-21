// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Android.Content;
    using Android.Widget;
    using Bindings;
    using Core;
    using Core.ApplicationServices;
    using MvvmCross.Binding.Bindings.Target.Construction;
    using MvvmCross.Binding.Droid.Views;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.Droid.Platform;
    using MvvmCross.Platform;
    using MvvmCross.Platform.Plugins;
    using MvvmCross.Plugins.File;
    using MvvmCross.Plugins.File.Droid;
    using PlatformServices;
    using Widgets;

    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override IEnumerable<Assembly> AndroidViewAssemblies
        {
            get
            {
                var assemblies = base.AndroidViewAssemblies;
                var toReturn = assemblies.ToList();
                toReturn.Add(this.GetType().Assembly);
                return toReturn;
            }
        }

        public override void LoadPlugins(IMvxPluginManager pluginManager)
        {
            pluginManager.EnsurePluginLoaded<PluginLoader>();
            pluginManager.EnsurePluginLoaded<MvvmCross.Plugins.DownloadCache.PluginLoader>();
            base.LoadPlugins(pluginManager);
        }

        protected override IMvxApplication CreateApp() => new App();

        protected override void InitializePlatformServices()
        {
            base.InitializePlatformServices(); // required android
            // register plattform dependent services here
            Mvx.RegisterType<IAccountService, AccountService>();
            Mvx.RegisterType<IUserInteractionService, UserInteractionService>();
            Mvx.RegisterType<IIOCacheService, IOCacheService>();
            Mvx.RegisterType<IIOStorageService, IOStorageService>();
            Mvx.RegisterType<ILogging, LogService>();
            Mvx.RegisterType<IDevice, DeviceService>();
            Mvx.RegisterType<IHttpClientConfiguration, HttpClientConfiguration>();
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            registry.RegisterCustomBindingFactory<WebButtonView>(
                            "Href",
                            t => new HrefViewTargetBinding(t));
            registry.RegisterCustomBindingFactory<MvxListView>(
                            "LoadMore",
                            t => new LoadMoreTargetBinding(t));
            registry.RegisterCustomBindingFactory<TouchImageView>(
                            "ClickLocation",
                            t => new ClickLocationTargetBinding(t));
            registry.RegisterCustomBindingFactory<TextView>(
                            "DrawableLeft",
                            t => new DrawableLeftTargetBinding(t));

            base.FillTargetFactories(registry);
        }

        protected override void AddPluginsLoaders(MvxLoaderPluginRegistry registry)
        {
            registry.Register<PluginLoader, Plugin>();
            registry.Register<MvvmCross.Plugins.DownloadCache.PluginLoader, MvvmCross.Plugins.DownloadCache.Droid.Plugin>();
            base.AddPluginsLoaders(registry);
        }
    }
}

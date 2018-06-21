// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Test
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using ApplicationServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using Moq;
    using MvvmCross.Core;
    using MvvmCross.Core.Platform;
    using MvvmCross.Core.Views;
    using MvvmCross.Platform.Core;
    using MvvmCross.Platform.IoC;
    using MvvmCross.Platform.Platform;
    using MvvmCross.Plugins.WebBrowser;

    public abstract class MvxIoCSupportingTest
    {
        protected Mock<IDevice> MockDevice { get; set; }

        protected ILogging MockLogging { get; set; }

        protected Mock<IMvxWebBrowserTask> MockWebBrowserTask { get; set; }

        protected Mock<IUserInteractionService> MockUserInteractionService { get; set; }

        protected IMvxIoCProvider Ioc { get; private set; }

        protected MockDispatcher MockDispatcher { get; private set; }

        [TestInitialize]
        public void Setup()
        {
            this.ClearAll();
        }

        protected virtual void ClearAll()
        {
            // fake set up of the IoC
            MvxSingleton.ClearAllSingletons();
            this.Ioc = MvxSimpleIoCContainer.Initialize();
            this.Ioc.RegisterSingleton(this.Ioc);
            this.Ioc.RegisterSingleton<IMvxTrace>(new TestTrace());
            InitializeSingletonCache();
            this.InitializeMvxSettings();
            MvxTrace.Initialize();
            this.AdditionalSetup();
        }

        protected virtual void InitializeMvxSettings()
        {
            this.Ioc.RegisterSingleton<IMvxSettings>(new MvxSettings());
        }

        protected virtual void AdditionalSetup()
        {
            this.MockDispatcher = new MockDispatcher();
            this.Ioc.RegisterSingleton<IMvxViewDispatcher>(this.MockDispatcher);
            this.Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(this.MockDispatcher);

            // for navigation parsing
            this.Ioc.RegisterSingleton<IMvxStringToTypeParser>(new MvxStringToTypeParser());

            this.MockLogging = new LogService();
            this.Ioc.RegisterSingleton(this.MockLogging);

            this.MockWebBrowserTask = new Mock<IMvxWebBrowserTask>(MockBehavior.Strict);
            this.MockWebBrowserTask.Setup(t => t.ShowWebPage(It.IsNotNull<string>()));
            this.Ioc.RegisterSingleton(this.MockWebBrowserTask.Object);

            this.MockDevice = new Mock<IDevice>(MockBehavior.Strict);

            this.MockDevice.SetupGet(t => t.HasNetworkConnectivity).Returns(true);
            this.MockDevice.SetupGet(t => t.Version).Returns("0.0.0-test");
            this.MockDevice.SetupGet(t => t.Platform).Returns(DevicePlatform.Neutral);
            this.MockDevice.SetupGet(t => t.Model).Returns("TestFramework");

            this.Ioc.RegisterSingleton(this.MockDevice.Object);

            this.MockUserInteractionService = new Mock<IUserInteractionService>(MockBehavior.Loose);
            this.Ioc.RegisterSingleton(this.MockUserInteractionService.Object);
        }

        protected async Task<T> GetCancellableAsync<T>(CancellationToken token)
        {
            return await Task.Factory.StartNew(() =>
            {
                Trace.WriteLine("Start waiting");
                var start = DateTime.Now;
                while ((DateTime.Now - start) < TimeSpan.FromSeconds(10))
                {
                    if (token.IsCancellationRequested)
                    {
                        return default(T);
                    }

                    Thread.Yield();
                    Trace.WriteLine($"waitet for {(DateTime.Now - start).TotalSeconds:0.000} seconds.");
                }

                throw new Exception("The operation was not cancelled.");
            });
        }

        private static void InitializeSingletonCache()
        {
            MvxSingletonCache.Initialize();
        }

        private class TestTrace : IMvxTrace
        {
            public void Trace(MvxTraceLevel level, string tag, Func<string> message)
            {
                Debug.WriteLine(tag + ":" + level + ":" + message());
            }

            public void Trace(MvxTraceLevel level, string tag, string message)
            {
                Debug.WriteLine(tag + ": " + level + ": " + message);
            }

            public void Trace(MvxTraceLevel level, string tag, string message, params object[] args)
            {
                Debug.WriteLine(tag + ": " + level + ": " + message, args);
            }
        }

        private class LogService : ILogging
        {
            public void Exception(object sender, Exception ex)
            {
                Debug.WriteLine($"Exception [{sender}] --> ");
                Debug.WriteLine(ex);
                Debug.WriteLine("<-- Exception");
            }

            public void Info(object sender, string message, params object[] parameters)
            {
                Debug.WriteLine($"Info [{sender}] --> ");
                Debug.WriteLine(message, parameters);
                Debug.WriteLine("<-- Info");
            }

            public void Warning(object sender, string warning, params object[] parameters)
            {
                Debug.WriteLine($"Warning [{sender}] --> ");
                Debug.WriteLine(warning, parameters);
                Debug.WriteLine("<-- Warning");
            }
        }
    }
}

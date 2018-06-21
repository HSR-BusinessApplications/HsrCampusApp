// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Test.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ApplicationServices;
    using Core.Infrastructure.Repository;
    using Core.Infrastructure.Sync;
    using DomainServices;
    using Hsr.Campus.Core.Resources;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model;
    using Moq;
    using MvvmCross.Core.Navigation;
    using Plugin.Settings.Abstractions;
    using Resources;
    using ViewModels;

    [TestClass]
    public class MenuViewModelTest
        : MvxIoCSupportingTest
    {
        private IMenuRepository menuRepository;
        private Mock<ISettings> moqSettings;

        [TestMethod]
        public async Task TestLoadData()
        {
            // Setup
            var webHandler = MockedWebHandler(DateTime.Parse("2017-06-19T00:00:00"));

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(webHandler.Object);

            var viewModel = this.Ioc.IoCConstruct<MenuViewModel>();

            // Exercise - start refreshing
            var task = viewModel.UpdateAsync(true);

            // Verify
            Assert.IsTrue(viewModel.IsWorking);

            // Exercise - wait for refresh to be done
            await task;

            // Verify
            Assert.IsFalse(viewModel.IsWorking);

            Assert.AreEqual(2, viewModel.Items.Count);
            Assert.IsTrue(viewModel.Items.All(t => t.HasContent));
            Assert.AreEqual(6, viewModel.Items.Sum(t => t.Items.Count)); // if successful, LoadCache will have been called

            webHandler.Verify(t => t.GetFeedsAsync(It.IsNotNull<CancellationToken>()), Times.Once);
            webHandler.Verify(t => t.GetMenuHtmlAsync(It.IsNotNull<string>(), It.IsNotNull<DateTime>(), It.IsNotNull<CancellationToken>()), Times.Exactly(6));
        }

        [TestMethod]
        public async Task TestNewData()
        {
            // Setup
            var webHandler = MockedWebHandler(DateTime.Parse("2017-06-19T00:00:00"));

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(webHandler.Object);
            var viewModel = this.Ioc.IoCConstruct<MenuViewModel>();

            // Exercise - Load old data
            await viewModel.UpdateAsync(true);

            // Verify
            Assert.AreEqual(DateTime.Parse("2017-06-19T00:00:00"), viewModel.Items[0].Items[0].Date);

            webHandler.Verify(t => t.GetFeedsAsync(It.IsNotNull<CancellationToken>()), Times.Exactly(1));
            webHandler.Verify(t => t.GetMenuHtmlAsync(It.IsNotNull<string>(), It.IsNotNull<DateTime>(), It.IsNotNull<CancellationToken>()), Times.Exactly(6));

            // Setup
            webHandler = MockedWebHandler(DateTime.Parse("2017-06-26T00:00:00"));

            this.Ioc.RegisterSingleton(webHandler.Object);
            viewModel = this.Ioc.IoCConstruct<MenuViewModel>();

            // Exercise - Load new data
            await viewModel.UpdateAsync(true);

            // Verify
            Assert.AreEqual(2, viewModel.Items.Count);
            Assert.IsTrue(viewModel.Items.All(t => t.HasContent));
            Assert.AreEqual(6, viewModel.Items.Sum(t => t.Items.Count));

            Assert.AreEqual(DateTime.Parse("2017-06-26T00:00:00"), viewModel.Items[0].Items[0].Date);

            webHandler.Verify(t => t.GetFeedsAsync(It.IsNotNull<CancellationToken>()), Times.Exactly(1));
            webHandler.Verify(t => t.GetMenuHtmlAsync(It.IsNotNull<string>(), It.IsNotNull<DateTime>(), It.IsNotNull<CancellationToken>()), Times.Exactly(6));
        }

        [TestMethod]
        public async Task TestNoNetwork()
        {
            // Setup
            var webHandler = new Mock<IMenuWebHandler>(MockBehavior.Strict);
            webHandler
                .Setup(r => r.GetFeedsAsync(It.IsNotNull<CancellationToken>()))
                .Throws<Exception>();

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(webHandler.Object);

            var viewModel = this.Ioc.IoCConstruct<MenuViewModel>();

            // Exercise
            await viewModel.UpdateAsync(true);

            // Verify
            this.MockUserInteractionService.Verify(t => t.Toast(AppResources.ConnectionFailed, ToastTime.Short), Times.Once);
        }

        [TestMethod]
        [Timeout(2000)]
        public async Task TestCancellation()
        {
            // Setup
            var webHandler = new Mock<IMenuWebHandler>(MockBehavior.Strict);
            webHandler
                .Setup(r => r.GetFeedsAsync(It.IsNotNull<CancellationToken>()))
                .Returns<CancellationToken>(this.GetCancellableAsync<IEnumerable<WhMenuFeed>>);

            webHandler
                .Setup(r => r.GetMenuHtmlAsync(It.IsNotNull<string>(), It.IsNotNull<DateTime>(), It.IsNotNull<CancellationToken>()))
                .Returns<CancellationToken>(this.GetCancellableAsync<string>);

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(webHandler.Object);

            var viewModel = this.Ioc.IoCConstruct<MenuViewModel>();

            // Exercise
            var task = viewModel.UpdateAsync(true);

            // Verify
            Assert.IsTrue(viewModel.IsWorking);

            // Exercise
            viewModel.CancelAll();

            await task;

            // Verify
            webHandler.Verify(t => t.GetFeedsAsync(It.IsNotNull<CancellationToken>()), Times.Once);

            Assert.IsFalse(viewModel.IsWorking);
        }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            this.moqSettings = new Mock<ISettings>(MockBehavior.Loose);

            this.menuRepository = new MenuRepository(this.moqSettings.Object);

            this.Ioc.RegisterSingleton(this.menuRepository);

            this.Ioc.RegisterType<IMenuSync, MenuSync>();
        }

        private static Mock<IMenuWebHandler> MockedWebHandler(DateTime date)
        {
            var webHandler = new Mock<IMenuWebHandler>(MockBehavior.Strict);
            webHandler
                .Setup(r => r.GetFeedsAsync(It.IsNotNull<CancellationToken>()))
                .Returns<CancellationToken>(c => GetRelativeDummyMenuFeedsAsync(date));

            webHandler
                .Setup(r => r.GetMenuHtmlAsync(It.IsNotNull<string>(), It.IsNotNull<DateTime>(), It.IsNotNull<CancellationToken>()))
                .Returns<string, DateTime, CancellationToken>((a, b, ct) => GetRelativeDummyMenuAsync(a, b));

            return webHandler;
        }

        private static async Task<IEnumerable<WhMenuFeed>> GetRelativeDummyMenuFeedsAsync(DateTime date)
        {
            return await Task.Factory.StartNew(() => new List<WhMenuFeed>
            {
                new WhMenuFeed
                {
                    Id = "mensa",
                    Name = "Mensa Hochschule Rapperswil",
                    Days = new List<WhMenuDay>
                    {
                        new WhMenuDay
                        {
                            Date = date,
                            ContentHash = "0c21cd24a9c4d1ae72345a688a269eed"
                        },
                        new WhMenuDay
                        {
                            Date = date.AddDays(1),
                            ContentHash = "8d98c3d7899f1e182ecb2825d1edfc07"
                        },
                        new WhMenuDay
                        {
                            Date = date.AddDays(2),
                            ContentHash = "9f6d037c77a7a780dc77110cb78ad894"
                        }
                    }
                },
                new WhMenuFeed
                {
                    Id = "bistro",
                    Name = "Forschungszentrum",
                    Days = new List<WhMenuDay>
                    {
                        new WhMenuDay
                        {
                            Date = date,
                            ContentHash = "b4f759be9fddf677b9de7731ca0b466b"
                        },
                        new WhMenuDay
                        {
                            Date = date.AddDays(1),
                            ContentHash = "d060441bd6668034e1c4a9900c55d218"
                        },
                        new WhMenuDay
                        {
                            Date = date.AddDays(2),
                            ContentHash = "e737e6163376ba834b94d088043b97c6"
                        }
                    }
                }
            });
        }

        private static async Task<string> GetRelativeDummyMenuAsync(string feedId, DateTime dayDate)
        {
            return await Task.Factory.StartNew(() => $"{feedId} {dayDate:dd.MM.yyyy}");
        }
    }
}

// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Test.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ApplicationServices;
    using Core.Infrastructure.Repository;
    using Core.Infrastructure.Sync;
    using DomainServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model;
    using Moq;
    using MvvmCross.Core.Navigation;
    using Plugin.Settings.Abstractions;
    using ViewModels;

    [TestClass]
    public class AdunisViewModelTest
        : MvxIoCSupportingTest
    {
        private IAdunisRepository repository;
        private Mock<IAccountService> account;
        private Mock<ISettings> settings;

        [TestMethod]
        public async Task TestCancellation()
        {
            // Setup
            var webHandler = new Mock<IAdunisWebHandler>(MockBehavior.Strict);

            webHandler
                .Setup(r => r.GetTimeperiodsAsync("test", It.IsAny<CancellationToken>()))
                .Returns<string, CancellationToken>((a, ct) => this.GetCancellableAsync<IEnumerable<WhTimeperiod>>(ct));

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(webHandler.Object);

            var viewModel = this.Ioc.IoCConstruct<AdunisViewModel>();

            // Exercise
            var task = viewModel.UpdateAsync(true);

            // Verify
            Assert.IsTrue(viewModel.IsWorking);

            // Exercise
            viewModel.CancelAll();

            await task;

            // Verify
            Assert.IsFalse(viewModel.IsWorking);
        }

        [TestMethod]
        public async Task TestNoNetwork()
        {
            // Setup
            var webHandler = new Mock<IAdunisWebHandler>(MockBehavior.Strict);

            webHandler
                .Setup(r => r.GetTimeperiodsAsync("test", It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(webHandler.Object);

            var viewModel = this.Ioc.IoCConstruct<AdunisViewModel>();

            // Exercise
            var task = viewModel.UpdateAsync(true);

            await task;

            // Verify
            webHandler.Verify(r => r.GetTimeperiodsAsync("test", It.IsAny<CancellationToken>()), Times.Once);
        }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            this.settings = new Mock<ISettings>(MockBehavior.Strict);

            this.repository = new AdunisRepository(this.settings.Object);

            this.account = new Mock<IAccountService>(MockBehavior.Strict);

            this.account.SetupGet(t => t.HasAccount).Returns(true);
            this.account.Setup(t => t.Retrieve())
                .Returns(new Account
                {
                    RefreshToken = Guid.NewGuid().ToString(),
                    Token = Guid.NewGuid().ToString(),
                    TokenRetrieved = DateTime.Now,
                    TokenValidUntil = DateTime.Now.AddDays(1),
                    UserName = "test"
                });

            this.Ioc.RegisterSingleton(this.account.Object);

            this.Ioc.RegisterSingleton(this.repository);

            this.Ioc.RegisterType<IAdunisSync, AdunisSync>();
        }
    }
}

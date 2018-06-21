// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Test.ViewModel
{
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;
    using Hsr.Campus.Core.ApplicationServices;
    using Hsr.Campus.Core.DomainServices;
    using Hsr.Campus.Core.Infrastructure.Repository;
    using Hsr.Campus.Core.Infrastructure.Sync;
    using Hsr.Campus.Core.Model;
    using Hsr.Campus.Core.OAuth;
    using Hsr.Campus.Core.Resources;
    using Hsr.Campus.Core.ViewModels;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using MvvmCross.Core.Navigation;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Plugin.Settings.Abstractions;

    [TestClass]
    public class AccountBalanceViewModelTest
        : MvxIoCSupportingTest
    {
        private IAccountBalanceRepository accountBalanceRepository;
        private Mock<ISettings> mockSettings;

        [TestMethod]
        [Timeout(2000)]
        public async Task TestCancellation()
        {
            // Setup
            var webHandler = new Mock<IAccountBalanceWebHandler>(MockBehavior.Strict);

            webHandler
                .Setup(t => t.GetCurrentStateAsync(It.IsAny<CancellationToken>()))
                .Returns<CancellationToken>(this.GetCancellableAsync<AccountBalance>);

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(webHandler.Object);

            var viewModel = this.Ioc.IoCConstruct<AccountBalanceViewModel>();

            // Exercise
            var task = viewModel.UpdateAsync();

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
            var webHandler = new Mock<IAccountBalanceWebHandler>(MockBehavior.Strict);

            webHandler
                .Setup(t => t.GetCurrentStateAsync(It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(webHandler.Object);

            var viewModel = this.Ioc.IoCConstruct<AccountBalanceViewModel>();

            // Exercise
            await viewModel.UpdateAsync();

            // Verify
            Assert.IsFalse(viewModel.IsWorking);

            this.MockUserInteractionService.Verify(t => t.Toast(AppResources.ConnectionFailed, ToastTime.Short), Times.Once);
        }

        [TestMethod]
        public async Task TestOAuthExpired()
        {
            // Setup
            var webHandler = new Mock<IAccountBalanceWebHandler>(MockBehavior.Strict);

            webHandler
                .Setup(t => t.GetCurrentStateAsync(It.IsAny<CancellationToken>()))
                .Throws(new OAuthExpiredException());

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);
            navigationService.Setup(r => r.Navigate<AccountViewModel, AccountViewModel.Args>(It.IsAny<AccountViewModel.Args>(), null)).Returns(Task.CompletedTask); // a => a.ReturnTo == typeof(AccountViewModel)

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(webHandler.Object);

            var viewModel = this.Ioc.IoCConstruct<AccountBalanceViewModel>();

            // Exercise
            await viewModel.UpdateAsync();

            // Verify
            Assert.IsFalse(viewModel.IsWorking);
        }

        [TestMethod]
        public async Task TestCache()
        {
            // Setup
            var balance = new AccountBalance { Deposit = 42.0, LastUpdate = DateTime.Now };

            var webHandler = new Mock<IAccountBalanceWebHandler>(MockBehavior.Strict);

            this.mockSettings
                .Setup(t => t.GetValueOrDefault("Hsr.Campus.Core.Model.AccountBalanceState", default(string), null))
                .Returns(JsonConvert.SerializeObject(balance, new IsoDateTimeConverter { Culture = CultureInfo.CurrentUICulture }));

            webHandler
                .Setup(t => t.GetCurrentStateAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(balance);

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(webHandler.Object);

            var viewModel = this.Ioc.IoCConstruct<AccountBalanceViewModel>();

            // Exercise
            await viewModel.UpdateAsync();

            // Verify
            Assert.IsTrue(viewModel.HasContent);
            Assert.AreEqual(balance.Deposit, viewModel.Balance.Deposit);

            this.mockSettings.Verify(t => t.AddOrUpdateValue("Hsr.Campus.Core.Model.AccountBalanceState", It.IsNotNull<string>(), null), Times.Once);
            this.mockSettings.Verify(t => t.GetValueOrDefault("Hsr.Campus.Core.Model.AccountBalanceState", default(string), null), Times.Exactly(1));

            // Exercise
            viewModel.LoadFromCache();

            // Verify
            this.mockSettings.Verify(t => t.GetValueOrDefault("Hsr.Campus.Core.Model.AccountBalanceState", default(string), null), Times.Exactly(2));
        }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            this.mockSettings = new Mock<ISettings>(MockBehavior.Strict);

            this.mockSettings.Setup(t => t.AddOrUpdateValue(It.IsNotNull<string>(), It.IsNotNull<string>(), null))
                .Returns(true);

            this.accountBalanceRepository = new AccountBalanceRepository(this.mockSettings.Object);

            this.Ioc.RegisterSingleton(this.accountBalanceRepository);

            this.Ioc.RegisterType<IAccountBalanceSync, AccountBalanceSync>();
        }
    }
}

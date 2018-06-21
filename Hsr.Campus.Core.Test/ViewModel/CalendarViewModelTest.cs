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
    using Resources;
    using ViewModels;

    [TestClass]
    public class CalendarViewModelTest
        : MvxIoCSupportingTest
    {
        private ICalendarEventRepository eventRepository;

        [TestMethod]
        [Timeout(2000)]
        public async Task TestCancellation()
        {
            // Setup
            var webHandler = new Mock<ICalendarEventWebHandler>(MockBehavior.Strict);

            webHandler.Setup(t => t.GetCalendarEventsAsync(It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
                .Returns<DateTime, CancellationToken>((a, b) => this.GetCancellableAsync<IEnumerable<WhCalendarEvent>>(b));

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(webHandler.Object);

            var viewModel = this.Ioc.IoCConstruct<CalendarViewModel>();

            // Exercise
            var task = viewModel.UpdateAsync(true, false);

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
            var webHandler = new Mock<ICalendarEventWebHandler>(MockBehavior.Strict);
            webHandler
                .Setup(t => t.GetCalendarEventsAsync(It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(webHandler.Object);

            var viewModel = this.Ioc.IoCConstruct<CalendarViewModel>();

            // Exercise
            var task = viewModel.UpdateAsync(true, false);

            await task;

            // Verify
            Assert.IsFalse(viewModel.IsWorking);

            this.MockUserInteractionService.Verify(t => t.Toast(AppResources.ConnectionFailed, ToastTime.Short), Times.Once);
        }

        [TestMethod]
        public async Task TestCache()
        {
            // Setup
            var webHandler = new Mock<ICalendarEventWebHandler>(MockBehavior.Strict);

            webHandler.Setup(t => t.GetCalendarEventsAsync(It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
                .Returns<DateTime, CancellationToken>((a, b) => this.GetRelativeDummyEventsAsync(a.Date));

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(webHandler.Object);

            var viewModel = this.Ioc.IoCConstruct<CalendarViewModel>();

            // Exercise
            await viewModel.UpdateAsync(true, false);

            // Verify
            Assert.IsFalse(viewModel.IsWorking);
            Assert.AreEqual(3, viewModel.Items.Count);

            webHandler.Verify(t => t.GetCalendarEventsAsync(It.IsAny<DateTime>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task TestLoadMore()
        {
            // Setup
            var webHandler = new Mock<ICalendarEventWebHandler>(MockBehavior.Strict);

            webHandler.Setup(t => t.GetCalendarEventsAsync(It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
                .Returns<DateTime, CancellationToken>((a, b) => this.GetRelativeDummyEventsAsync(a.Date));

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(webHandler.Object);

            var viewModel = this.Ioc.IoCConstruct<CalendarViewModel>();

            // Exercise
            await viewModel.UpdateAsync(true, false);

            // Verify
            Assert.IsFalse(viewModel.IsWorking);
            Assert.AreEqual(3, viewModel.Items.Count);

            webHandler.Verify(t => t.GetCalendarEventsAsync(It.IsAny<DateTime>(), It.IsAny<CancellationToken>()), Times.Once);

            // Exercise
            await viewModel.UpdateAsync(false, true);

            // Verify
            Assert.AreEqual(6, viewModel.Items.Count);

            webHandler.Verify(t => t.GetCalendarEventsAsync(It.IsAny<DateTime>(), It.IsAny<CancellationToken>()), Times.Exactly(2));

            // Setup
            // simulate receiving old entries
            webHandler.Setup(t => t.GetCalendarEventsAsync(viewModel.Items.Last().Start, It.IsAny<CancellationToken>()))
                .Returns<DateTime, CancellationToken>((a, b) => this.GetRelativeDummyEventsAsync(a.Date.AddDays(-7)));

            // Exercise
            await viewModel.UpdateAsync(false, true);

            // Verify
            webHandler.Verify(t => t.GetCalendarEventsAsync(It.IsAny<DateTime>(), It.IsAny<CancellationToken>()), Times.Exactly(3));

            Assert.AreEqual(6, viewModel.Items.Count);
        }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            this.eventRepository = new CalendarEventRepository();
            this.eventRepository.Truncate();

            this.Ioc.RegisterSingleton(this.eventRepository);

            this.Ioc.RegisterType<ICalendarEventSync, CalendarEventSync>();
        }

        private async Task<IEnumerable<WhCalendarEvent>> GetRelativeDummyEventsAsync(DateTime date)
        {
            return await Task.Factory.StartNew(() => new List<WhCalendarEvent>
            {
                new WhCalendarEvent
                {
                    Description = "Lorem ipsum",
                    Start = date.AddDays(1),
                    End = date.AddDays(1).AddHours(1),
                    EventID = Guid.NewGuid() + "@google.com",
                    Link = "http://www.example.com/",
                    Location = "Loc",
                    Summary = "Lorem"
                },
                new WhCalendarEvent
                {
                    Description = "Lorem ipsum",
                    Start = date.AddDays(2),
                    End = date.AddDays(2).AddHours(1),
                    EventID = Guid.NewGuid() + "@google.com",
                    Link = "http://www.example.com/",
                    Location = "Loc",
                    Summary = "Lorem"
                },
                new WhCalendarEvent
                {
                    Description = "Lorem ipsum",
                    Start = date.AddDays(3),
                    End = date.AddDays(3).AddHours(1),
                    EventID = Guid.NewGuid() + "@google.com",
                    Link = "http://www.example.com/",
                    Location = "Loc",

                    Summary = "Lorem"
                }
            });
        }
    }
}

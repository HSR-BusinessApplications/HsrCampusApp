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
    public class NewsFeedViewModelTest
        : MvxIoCSupportingTest
    {
        private INewsRepository newsRepository;
        private Mock<IIOCacheService> moqCache;
        private Mock<ISettings> moqSettings;

        [TestMethod]
        [Timeout(10000)]
        public async Task TestCancellation()
        {
            // Setup
            var newsHandler = new Mock<INewsWebHandler>(MockBehavior.Strict);
            newsHandler
                .Setup(r => r.GetNewsEntriesAsync(It.IsAny<SqlNewsFeed>(), It.IsAny<DateTime>(), It.IsNotNull<CancellationToken>()))
                .Returns<SqlNewsFeed, DateTime, CancellationToken>((a, b, ct) => this.GetCancellableAsync<IEnumerable<WhNews>>(ct));

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(newsHandler.Object);

            var viewModel = this.Ioc.IoCConstruct<NewsFeedViewModel>();

            // Exercise
            viewModel.Init(new SqlNewsFeed());

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
        public async Task TestCache()
        {
            // Setup
            var newsHandler = new Mock<INewsWebHandler>(MockBehavior.Strict);
            newsHandler
                .Setup(r => r.GetNewsEntriesAsync(It.IsAny<SqlNewsFeed>(), It.IsAny<DateTime>(), It.IsNotNull<CancellationToken>()))
                .Returns<SqlNewsFeed, DateTime, CancellationToken>((a, b, c) => this.GetRelativeDummyNewsAsync(b));

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(newsHandler.Object);

            var feedViewModel = this.Ioc.IoCConstruct<NewsFeedViewModel>();

            // Exercise
            feedViewModel.Init(new SqlNewsFeed());

            await feedViewModel.UpdateAsync(true, false);

            // Verify
            Assert.IsFalse(feedViewModel.IsWorking);
            Assert.AreEqual(3, feedViewModel.Items.Count);
            Assert.AreEqual(DateTime.Now.AddDays(-1).Date, feedViewModel.Items.First().Date.Date);
        }

        [TestMethod]
        public async Task TestLoadMore()
        {
            // Setup
            var newsHandler = new Mock<INewsWebHandler>(MockBehavior.Strict);
            newsHandler
                .Setup(r => r.GetNewsEntriesAsync(It.IsAny<SqlNewsFeed>(), It.IsAny<DateTime>(), It.IsNotNull<CancellationToken>()))
                .Returns<SqlNewsFeed, DateTime, CancellationToken>((a, b, c) => this.GetRelativeDummyNewsAsync(b));

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(newsHandler.Object);

            var viewModel = this.Ioc.IoCConstruct<NewsFeedViewModel>();

            // Exercise
            viewModel.Init(new SqlNewsFeed());

            await viewModel.UpdateAsync(true, false);

            // Verify
            Assert.IsFalse(viewModel.IsWorking);
            Assert.AreEqual(3, viewModel.Items.Count);

            // Exercise
            await viewModel.UpdateAsync(false, true);

            // Verify
            Assert.IsFalse(viewModel.IsWorking);
            Assert.AreEqual(5, viewModel.Items.Count); // only load two more
        }

        [TestMethod]
        public async Task TestLastUpdate()
        {
            // Setup
            var newsHandler = new Mock<INewsWebHandler>(MockBehavior.Strict);
            newsHandler
                .Setup(r => r.GetNewsEntriesAsync(It.IsAny<SqlNewsFeed>(), It.IsAny<DateTime>(), It.IsNotNull<CancellationToken>()))
                .Returns<SqlNewsFeed, DateTime, CancellationToken>((a, b, c) => this.GetRelativeDummyNewsAsync(b));

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(newsHandler.Object);

            var viewModel = this.Ioc.IoCConstruct<NewsFeedViewModel>();

            // Exercise
            viewModel.Init(new SqlNewsFeed());

            await viewModel.UpdateAsync(true, false);

            // Verify
            Assert.IsFalse(viewModel.IsWorking);
            Assert.AreEqual(3, viewModel.Items.Count);
            newsHandler.Verify(handler => handler.GetNewsEntriesAsync(It.IsAny<SqlNewsFeed>(), It.IsAny<DateTime>(), It.IsNotNull<CancellationToken>()), Times.Once);

            // Exercise
            await viewModel.UpdateAsync(false, false);

            // Verify
            newsHandler.Verify(handler => handler.GetNewsEntriesAsync(It.IsAny<SqlNewsFeed>(), It.IsAny<DateTime>(), It.IsNotNull<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task TestUrl()
        {
            // Setup
            var data = await this.GetRelativeDummyNewsAsync(DateTime.Now);

            var news = data.First();

            var viewModel = new NewsFeedViewModel(null, null, null, this.MockWebBrowserTask.Object);

            // Exercise
            viewModel.ShowDetail(new SqlNews()
            {
                Date = news.Date,
                FeedKey = "Test",
                NewsId = news.NewsId,
                Title = news.Title,
                Url = news.Url,
                Message = news.Message
            });

            // Verify
            this.MockWebBrowserTask.Verify(t => t.ShowWebPage(news.Url), Times.Once);
        }

        [TestMethod]
        public async Task TestNoNetwork()
        {
            // Setup
            var newsHandler = new Mock<INewsWebHandler>(MockBehavior.Strict);
            newsHandler
                .Setup(r => r.GetNewsEntriesAsync(It.IsAny<SqlNewsFeed>(), It.IsAny<DateTime>(), It.IsNotNull<CancellationToken>()))
                .Throws<Exception>();

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(newsHandler.Object);

            var viewModel = this.Ioc.IoCConstruct<NewsFeedViewModel>();

            // Exercise
            viewModel.Init(new SqlNewsFeed());

            await viewModel.UpdateAsync(true, false);

            // Verify
            this.MockUserInteractionService.Verify(t => t.Toast(AppResources.ConnectionFailed, ToastTime.Short), Times.Never);
        }

        [TestMethod]
        public async Task TestPicture()
        {
            // Setup
            var newsHandler = new Mock<INewsWebHandler>(MockBehavior.Strict);
            newsHandler
                .Setup(r => r.GetNewsEntriesAsync(It.IsAny<SqlNewsFeed>(), It.IsAny<DateTime>(), It.IsNotNull<CancellationToken>()))
                .Returns<SqlNewsFeed, DateTime, CancellationToken>((a, b, c) => this.GetPictureNewsAsync(b));

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(newsHandler.Object);

            var viewModel = this.Ioc.IoCConstruct<NewsFeedViewModel>();

            // Exercise
            viewModel.Init(new SqlNewsFeed());

            await viewModel.UpdateAsync(true, false);

            // Verify
            Assert.IsFalse(viewModel.IsWorking);
            Assert.AreEqual(1, viewModel.Items.Count);

            this.moqCache.Verify(t => t.AssurePath(It.IsNotNull<string>()), Times.Exactly(viewModel.Items.Count));
            this.moqCache.Verify(t => t.WriteBitmap(It.IsNotNull<string>(), It.IsNotNull<byte[]>()), Times.Exactly(viewModel.Items.Count));

            // Exercise
            await viewModel.UpdateAsync(false, true);

            // Verify
            this.moqCache.Verify(t => t.AssurePath(It.IsNotNull<string>()), Times.Exactly(viewModel.Items.Count));
            this.moqCache.Verify(t => t.WriteBitmap(It.IsNotNull<string>(), It.IsNotNull<byte[]>()), Times.Exactly(viewModel.Items.Count));
        }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            this.moqCache = new Mock<IIOCacheService>(MockBehavior.Loose);

            this.moqSettings = new Mock<ISettings>(MockBehavior.Loose);

            this.newsRepository = new NewsRepository(this.moqCache.Object, this.MockDevice.Object, this.moqSettings.Object);
            this.newsRepository.Truncate();

            this.Ioc.RegisterSingleton(this.newsRepository);

            this.Ioc.RegisterType<INewsSync, NewsSync>();
        }

        private async Task<IEnumerable<WhNews>> GetPictureNewsAsync(DateTime dt)
        {
            if (dt.Date == DateTime.Now.Date)
            {
                dt = dt.AddDays(-1);
            }

            return await Task.Factory.StartNew(() => new List<WhNews>
            {
                new WhNews
                {
                    Date = dt.AddHours(1),
                    NewsId = Guid.NewGuid().ToString(),
                    Message = "Lorem ipsum",
                    PictureBitmap = "R0lGODlhAQABAIAAAP///wAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw==",
                    Title = "Lorem",
                    Url = "http://www.example.com/"
                }
            });
        }

        private async Task<IEnumerable<WhNews>> GetRelativeDummyNewsAsync(DateTime dte)
        {
            return await Task.Factory.StartNew(() =>
            {
                var staticDate = new DateTime(2015, 01, 01, 01, 01, 01);
                var news = new List<WhNews>
                {
                    new WhNews
                    {
                        Date = dte.AddDays(-1),
                        NewsId = Guid.NewGuid().ToString(),
                        Message = "Lorem ipsum",
                        Title = "Lorem",
                        Url = "http://www.example.com/"
                    },
                    new WhNews
                    {
                        Date = dte.AddMonths(-1),
                        NewsId = Guid.NewGuid().ToString(),
                        Message = "Ipsum lorem",
                        Title = "Delor",
                        Url = "http://www.example.com/"
                    },
                    new WhNews
                    {
                        Date = staticDate,
                        NewsId = Guid.Empty.ToString(),
                        Message = "Static, Static",
                        Title = "Static",
                        Url = "http://www.example.com/#static"
                    }
                };

                return news;
            });
        }
    }
}

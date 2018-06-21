// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Test.Infrastructure.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ApplicationServices;
    using Core.Infrastructure.Repository;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model;
    using Moq;
    using Plugin.Settings.Abstractions;

    [TestClass]
    public class NewsRepositoryTests
        : MvxIoCSupportingTest
    {
        [TestMethod]
        public void UpdateNewsRange_UpdateExisting()
        {
            // Setup
            var oldNews = new List<WhNews>()
            {
                new WhNews()
                {
                    Date = DateTime.Parse("2016-09-12T11:22:34+02:00"),
                    NewsId = "134468179973906_302726776764354",
                    Message = "Alter Inhalt",
                    Title = "Alte News",
                    Url = "https://www.facebook.com/events/302726776764354/",
                }
            };

            var newNews = new List<WhNews>()
            {
                new WhNews
                {
                    Date = DateTime.Parse("2016-09-12T11:22:34+02:00"),
                    NewsId = "134468179973906_302726776764354",
                    Message = "Neuer Inhalt",
                    Title = "Neue News",
                    Url = "https://www.facebook.com/events/302726776764354/",
                }
            };

            var moqCache = new Mock<IIOCacheService>(MockBehavior.Loose);
            var mockSettings = new Mock<ISettings>(MockBehavior.Loose);
            var newsRepository = new NewsRepository(moqCache.Object, this.MockDevice.Object, mockSettings.Object);

            var feed = new SqlNewsFeed()
            {
                Key = "vshsr",
                Name = "VSHSR",
                HsrSport = false
            };

            newsRepository.UpdateNewsRange(feed, oldNews);

            // Exercise
            newsRepository.UpdateNewsRange(feed, newNews);

            var news = newsRepository.RetrieveNewsBefore(feed, DateTime.Now, 3);

            // Verfiy
            Assert.AreEqual(1, news.Count());

            Assert.AreEqual("Neue News", news.First().Title);
            Assert.AreEqual("Neuer Inhalt", news.First().Message);
        }
    }
}

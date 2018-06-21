// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Test.ViewModel
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Hsr.Campus.Core.ApplicationServices;
    using Hsr.Campus.Core.DomainServices;
    using Hsr.Campus.Core.Infrastructure.Repository;
    using Hsr.Campus.Core.Infrastructure.Sync;
    using Hsr.Campus.Core.Model;
    using Hsr.Campus.Core.Test.TestData;
    using Hsr.Campus.Core.ViewModels;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using MvvmCross.Core.Navigation;
    using Newtonsoft.Json;
    using Plugin.Settings.Abstractions;

    [TestClass]
    public class MapViewModelTest
        : MvxIoCSupportingTest
    {
        private const string ByteOrderMark = "\uFEFF";

        private IMapRepository repository;
        private Mock<ISettings> mockSettings;
        private Mock<IIOCacheService> cacheService;

        [TestMethod]
        [Timeout(2000)]
        public async Task TestCancellation()
        {
            // Setup
            var webHandler = new Mock<IMapWebHandler>(MockBehavior.Strict);

            webHandler
                .Setup(t => t.GetHashesAsync(It.IsAny<CancellationToken>()))
                .Returns<CancellationToken>(this.GetCancellableAsync<MapOverview>);

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(webHandler.Object);

            var viewModel = this.Ioc.IoCConstruct<MapViewModel>();

            // Exercise
            viewModel.Init(null, "Main");

            var task = viewModel.UpdateAsync();

            // Verify
            Assert.IsTrue(viewModel.IsWorking);

            // Exercise
            viewModel.CancelAll();
            await task;

            // Verify
            Assert.IsFalse(viewModel.IsWorking);
        }

        /// <summary>
        /// Tests the loading of a building in the ViewModel from the cache
        /// The Web-access from the service is beeing mocked
        /// </summary>
        /// <returns>Async UnitTest</returns>
        [TestMethod]
        public async Task LoadCache()
        {
            // Setup
            var webHandler = new Mock<IMapWebHandler>(MockBehavior.Strict);

            webHandler
                .Setup(t => t.GetHashesAsync(It.IsAny<CancellationToken>()))
                .Returns<CancellationToken>(c => GetExampleMapAsync()); // Gibt immer die ExampleMap zurück.
            webHandler
                .Setup(t => t.GetImageStreamAsync(It.IsAny<Guid>()))
                .Returns<Guid>(guid => GetExampleImage());

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(webHandler.Object);

            this.cacheService.Setup(t => t.FileExists(It.IsAny<string>())).Returns(true); // Jede Datei existiert.

            var viewModelRoot = this.Ioc.IoCConstruct<MapViewModel>();

            // Exercise
            viewModelRoot.Init(null, "Main");

            await viewModelRoot.UpdateAsync();

            // Verify Setup
            Assert.IsNotNull(viewModelRoot.Maps);
            Assert.IsNotNull(viewModelRoot.Sub);
            Assert.AreEqual(1, viewModelRoot.Maps.Count);
            Assert.AreEqual(6, viewModelRoot.Sub.Count());

            // Setup
            var gebaeudeMap = viewModelRoot.Sub.First();

            var viewModelGeb = this.Ioc.IoCConstruct<MapViewModel>();

            // Exercise
            viewModelGeb.Init(gebaeudeMap.Id.ToString(), "Test");

            viewModelGeb.LoadCache(gebaeudeMap.Id);

            // Verify
            Assert.IsTrue(viewModelGeb.HasContent);
            Assert.IsNotNull(viewModelGeb.Maps);
            Assert.IsNull(viewModelGeb.Sub);
            Assert.AreEqual(gebaeudeMap.Id, viewModelGeb.Id);
            Assert.AreEqual("Test", viewModelGeb.Title);
            Assert.IsTrue(viewModelGeb.Maps.Count > 0);
        }

        [TestMethod]
        public async Task TestNoNetwork()
        {
            // Setup
            var webHandler = new Mock<IMapWebHandler>(MockBehavior.Strict);

            webHandler
                .Setup(t => t.GetHashesAsync(It.IsAny<CancellationToken>()))
                .Throws<TestException>();

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(webHandler.Object);

            var viewModel = this.Ioc.IoCConstruct<MapViewModel>();

            // Exercise
            viewModel.Init(null, "Main");

            await viewModel.UpdateAsync();

            // Verify
            Assert.IsFalse(viewModel.IsLoading);
        }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            this.mockSettings = new Mock<ISettings>(MockBehavior.Loose);
            this.cacheService = new Mock<IIOCacheService>(MockBehavior.Loose);

            this.repository = new MapRepository();

            this.Ioc.RegisterSingleton(this.mockSettings.Object);
            this.Ioc.RegisterSingleton(this.repository);
            this.Ioc.RegisterSingleton(this.cacheService.Object);

            this.Ioc.RegisterType<IMapSync, MapSync>();
        }

        /// <summary>
        /// Returns the stream (MemoryStream) from a PNG image
        /// </summary>
        /// <returns>MemoryStream with the data from the PNG image</returns>
        private static async Task<Stream> GetExampleImage()
        {
            return await Task.Factory.StartNew(() => new MemoryStream(TestDataBinaries.ExamplePng));
        }

        /// <summary>
        /// Returns a <see cref="MapOverview"/> object with 6 buildings
        /// </summary>
        /// <returns>MapOverview with 6 buildings</returns>
        private static async Task<MapOverview> GetExampleMapAsync()
        {
            return await Task.Factory.StartNew(() =>
            {
                var jsonString = Encoding.UTF8.GetString(TestDataBinaries.ExampleMap);
                if (jsonString.StartsWith(ByteOrderMark))
                {
                    jsonString = jsonString.Substring(1);
                }
                return JsonConvert.DeserializeObject<MapOverview>(jsonString);
            });
        }

        /// <summary>
        /// Exception thrown for testing
        /// </summary>
        private class TestException : Exception
        {
            public TestException()
            {
            }

            public TestException(string message)
                : base(message)
            {
            }

            public TestException(string message, Exception innerException)
                : base(message, innerException)
            {
            }

            protected TestException(
                System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context)
                : base(info, context)
            {
            }
        }
    }
}

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
    using DomainServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model;
    using Moq;
    using MvvmCross.Core.Navigation;
    using ViewModels;

    [TestClass]
    public class FilerViewModelTest : MvxIoCSupportingTest
    {
        /// <summary>
        /// The <see cref="FilerViewModel.Init(FilerViewModel.FilerArgs)"/>-Method must cancel when the method <see cref="AbstractViewModel.CancelAll(bool)"/> is called.
        /// </summary>
        /// <returns>Async test operation</returns>
        [TestMethod]
        [Timeout(2000)]
        public async Task Init_should_cancel_when_calling_CancelAll()
        {
            // Setup
            var storageService = new Mock<IIOStorageService>(MockBehavior.Strict);

            storageService.Setup(r => r.DirectoryExists(It.IsNotNull<string>())).Returns(true);
            storageService.Setup(r => r.GetDirectoryNames(It.IsAny<string>())).Returns<string>(a => new List<string>());
            storageService.Setup(r => r.GetFileNames(It.IsAny<string>())).Returns<string>(a => new List<string>());

            var filerWebHandler = new Mock<IFilerWebHandler>(MockBehavior.Strict);

            filerWebHandler
                .Setup(r => r.GetDirectoryListingAsync(It.IsAny<FilerViewModel.FilerArgs>(), It.IsNotNull<CancellationToken>()))
                .Returns<FilerViewModel.FilerArgs, CancellationToken>((a, ct) => this.GetCancellableAsync<IEnumerable<IOListing>>(ct));

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(filerWebHandler.Object);
            this.Ioc.RegisterSingleton(storageService.Object);
            this.Ioc.RegisterSingleton(navigationService.Object);

            var viewModel = this.Ioc.IoCConstruct<FilerViewModel>();

            // Exercise Start
            var task = viewModel.Init(new FilerViewModel.FilerArgs { CurrentDirectory = string.Empty });

            // Verify
            Assert.IsTrue(viewModel.IsWorking);

            // Exercise Cancel
            viewModel.CancelAll();
            await task;

            // Verify
            Assert.IsFalse(viewModel.IsWorking);
        }

        /// <summary>
        /// If a network error occurs the locally stored data should be shown
        /// </summary>
        /// <returns>Async test operation</returns>
        [TestMethod]
        public async Task Init_should_load_local_values_on_networkerror()
        {
            // Setup
            var rootDirectory = string.Empty;
            var storageService = new Mock<IIOStorageService>(MockBehavior.Strict);
            storageService.Setup(r => r.DirectoryExists(It.IsNotNull<string>())).Returns(true);
            storageService.Setup(r => r.GetDirectoryNames(rootDirectory)).Returns<string>(a => RandomEntries(2));
            storageService.Setup(r => r.GetFileNames(rootDirectory)).Returns<string>(a => RandomEntries(3));

            var filerWebHandler = new Mock<IFilerWebHandler>(MockBehavior.Strict);

            filerWebHandler
                .Setup(r => r.GetDirectoryListingAsync(It.IsAny<FilerViewModel.FilerArgs>(), It.IsNotNull<CancellationToken>()))
                .Throws<Exception>();

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(filerWebHandler.Object);
            this.Ioc.RegisterSingleton(storageService.Object);

            var viewModel = this.Ioc.IoCConstruct<FilerViewModel>();

            // Exercise
            await viewModel.Init(new FilerViewModel.FilerArgs { CurrentDirectory = rootDirectory });

            // Verify
            Assert.AreEqual(5, viewModel.Listings.Count);
        }

        [TestMethod]
        public async Task TestDeepLink()
        {
            // Setup
            var path = "/This/is/a/deeplink/";
            var storageService = new Mock<IIOStorageService>(MockBehavior.Strict);
            storageService.Setup(r => r.DirectoryExists(path)).Returns(true);
            storageService.Setup(r => r.StartOpenFile(It.IsNotNull<string>()));

            storageService.Setup(r => r.GetDirectoryNames(path)).Returns<string>(a => RandomEntries(2));
            storageService.Setup(r => r.GetFileNames(path)).Returns<string>(a => RandomEntries(3));
            storageService.Setup(r => r.Download(It.IsAny<IOListing>(), It.IsNotNull<Action<ResultState>>()))
                .Callback((IOListing a, Action<ResultState> b) => b(ResultState.Success));

            var args = new FilerViewModel.FilerArgs { CurrentDirectory = path };

            var filerWebHandler = new Mock<IFilerWebHandler>(MockBehavior.Strict);

            filerWebHandler
                .Setup(r => r.GetDirectoryListingAsync(args, It.IsNotNull<CancellationToken>()))
                .Returns<FilerViewModel.FilerArgs, CancellationToken>((a, b) => this.GetRandomEntriesAsync(path));

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);
            navigationService.Setup(r => r.Navigate<FilerViewModel, FilerViewModel.FilerArgs>(It.IsAny<FilerViewModel.FilerArgs>(), null)).Returns(Task.CompletedTask);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(filerWebHandler.Object);
            this.Ioc.RegisterSingleton(storageService.Object);

            var viewModel = this.Ioc.IoCConstruct<FilerViewModel>();

            // Exercise
            await viewModel.Init(args);

            // Verify
            storageService.Verify(r => r.DirectoryExists(path), Times.Once);

            Assert.AreEqual(7, viewModel.Listings.Count);

            // Exercise
            viewModel.ShowDetailCommand.Execute(viewModel.Listings.First(t => t.IsDirectory));

            // Open local file
            viewModel.ShowDetailCommand.Execute(viewModel.Listings.First(t => !t.IsDirectory && t.IsLocal));

            // Verify
            storageService.Verify(t => t.StartOpenFile(It.IsNotNull<string>()), Times.Once);

            // Download Remote
            Assert.AreEqual(5, viewModel.Listings.Count(t => t.IsLocal));

            // Exercise
            viewModel.ShowDetailCommand.Execute(viewModel.Listings.First(t => !t.IsDirectory && !t.IsLocal));

            // Verify
            Assert.AreEqual(6, viewModel.Listings.Count(t => t.IsLocal));
        }

        [TestMethod]
        public async Task TestExisting()
        {
            // Setup
            var path = "/This/is/a/deeplink/";
            var storageService = new Mock<IIOStorageService>(MockBehavior.Strict);
            storageService.Setup(r => r.DirectoryExists(path)).Returns(true);

            storageService.Setup(r => r.GetDirectoryNames(path)).Returns<string>(a => RandomEntries(2));
            storageService.Setup(r => r.GetFileNames(path)).Returns<string>(a => RandomEntries(3, true));

            var args = new FilerViewModel.FilerArgs { CurrentDirectory = path };

            var filerWebHandler = new Mock<IFilerWebHandler>(MockBehavior.Strict);

            filerWebHandler
                .Setup(r => r.GetDirectoryListingAsync(args, It.IsNotNull<CancellationToken>()))
                .Returns<FilerViewModel.FilerArgs, CancellationToken>((a, b) => this.GetRandomEntriesAsync(path, true));

            var navigationService = new Mock<IMvxNavigationService>(MockBehavior.Strict);

            this.Ioc.RegisterSingleton(navigationService.Object);
            this.Ioc.RegisterSingleton(filerWebHandler.Object);
            this.Ioc.RegisterSingleton(storageService.Object);

            var viewModel = this.Ioc.IoCConstruct<FilerViewModel>();

            // Exercise
            await viewModel.Init(args);

            // Verify
            Assert.AreEqual(8, viewModel.Listings.Count);

            Assert.AreEqual(1, viewModel.Listings.Count(t => t.Name.Equals("existing")));
        }

        private static IEnumerable<string> RandomEntries(int count, bool withExisting = false)
        {
            for (var i = 0; i < count; i++)
            {
                yield return $"R{i}-{Guid.NewGuid()}";
            }

            if (withExisting)
            {
                yield return "existing";
            }
        }

        private async Task<IEnumerable<IOListing>> GetRandomEntriesAsync(string path, bool withExisting = false)
        {
            return await Task.Factory.StartNew(() =>
            {
                var result = new List<IOListing>();

                var name = "R0-" + Guid.NewGuid();

                result.Add(new IOListing
                {
                    FullPath = path + name + "/",
                    IsDirectory = true,
                    IsLink = false,
                    IsLocal = false,
                    LastModified = DateTime.Now,
                    Name = name,
                    RawSize = int.MaxValue,
                    Size = "0 MB",
                    Url = "http://example.com/{0}"
                });

                name = "R1-" + Guid.NewGuid();

                result.Add(new IOListing
                {
                    FullPath = path + name,
                    IsDirectory = false,
                    IsLink = false,
                    IsLocal = false,
                    LastModified = DateTime.Now,
                    Name = name,
                    RawSize = int.MaxValue,
                    Size = "0 MB",
                    Url = "http://example.com/{0}"
                });

                if (!withExisting)
                {
                    return result;
                }

                name = "existing";

                result.Add(new IOListing
                {
                    FullPath = path + name,
                    IsDirectory = false,
                    IsLink = false,
                    IsLocal = true,
                    LastModified = DateTime.Now,
                    Name = name,
                    RawSize = int.MaxValue,
                    Size = "0 MB",
                    Url = "http://example.com/{0}"
                });

                return result;
            });
        }
    }
}

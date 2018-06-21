// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using ApplicationServices;
    using DomainServices;
    using Model;
    using MvvmCross.Core.Navigation;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.Platform;
    using Resources;

    public class FilerViewModel : AbstractParameterizedViewModel<FilerViewModel.FilerArgs>, ITitled
    {
        private readonly IIOStorageService storageService;
        private readonly IFilerWebHandler filerWebHandler;
        private readonly ILogging logging;
        private FilerArgs filerArgs;

        private ObservableCollection<IOListing> listings;
        private bool isDownloading;

        public FilerViewModel(IMvxNavigationService navigationService, IIOStorageService storageService, IFilerWebHandler filerWebHandler, ILogging logging)
        {
            this.NavigationService = navigationService;
            this.storageService = storageService;
            this.filerWebHandler = filerWebHandler;
            this.logging = logging;
        }

        public ICommand ShowDetailCommand => new MvxCommand<IOListing>(this.LoadListing);

        public ObservableCollection<IOListing> Listings
        {
            get
            {
                return this.listings;
            }

            set
            {
                this.listings = value;
                this.RaisePropertyChanged();
            }
        }

        public string Title
        {
            get
            {
                return string.IsNullOrEmpty(this.filerArgs.CurrentDirectory) ? AppResources.TileLectureNotes : this.filerArgs.CurrentDirectory.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Last();
            }
        }

        public bool IsDownloading
        {
            get
            {
                return this.isDownloading;
            }

            set
            {
                this.isDownloading = value;
                this.RaisePropertyChanged();
            }
        }

        public async Task Init(FilerArgs args)
        {
            this.Listings = new ObservableCollection<IOListing>();

            if (this.IsLoading)
            {
                return;
            }

            this.IsLoading = true;

            this.filerArgs = args;

            var loadLocalListing = Task.Factory.StartNew(() => this.LoadLocal(args));
            var loadRemoteListing = this.LoadRemoteAsync(args);

            var results = await Task.Factory.ContinueWhenAll(new[] { loadLocalListing, loadRemoteListing }, tasks =>
            {
                var taskResults = new List<IEnumerable<IOListing>>();
                foreach (var task in tasks)
                {
                    if (task.IsFaulted)
                    {
                        this.logging.Exception(this, task.Exception);
                    }
                    else
                    {
                        taskResults.Add(task.Result);
                    }
                }

                return taskResults;
            });

            var ordered = results.SelectMany(l => l)
                .GroupBy(l => l.FullPath.ToLowerInvariant())
                .Select(g => g.OrderByDescending(l => l.IsLocal).First()) // prefer IsLocal == true
                .OrderByDescending(l => l.IsDirectory)
                .ThenBy(l => l.Name);

            this.Listings = new ObservableCollection<IOListing>(ordered);
            this.HasContent = this.Listings.Count > 0;

            this.IsLoading = false;
        }

        public override void Prepare(FilerArgs parameter)
        {
        }

        public void LoadListing(IOListing io)
        {
            if (io.IsDirectory)
            {
                this.Navigate<FilerViewModel, FilerArgs>(new FilerArgs { CurrentDirectory = io.FullPath });
            }
            else if (io.IsLocal)
            {
                this.storageService.StartOpenFile(io.FullPath);
            }
            else
            {
                if (this.IsDownloading)
                {
                    return;
                }

                this.IsDownloading = true;
                this.storageService.Download(io, state =>
                {
                    if (state == ResultState.Success)
                    {
                        this.Dispatcher.RequestMainThreadAction(() =>
                            this.Listings.Single(t => t.FullPath == io.FullPath).IsLocal = true);
                        this.RaisePropertyChanged(nameof(this.Listings));
                        this.storageService.StartOpenFile(io.FullPath);
                    }

                    this.IsDownloading = false;
                });
            }
        }

        private IEnumerable<IOListing> LoadLocal(FilerArgs args)
        {
            var list = new List<IOListing>();

            if (!this.storageService.DirectoryExists(args.CurrentDirectory))
            {
                return list;
            }

            list.AddRange(
                this.storageService
                    .GetDirectoryNames(args.CurrentDirectory)
                    .Select(dirName => new IOListing
                    {
                        FullPath = args.CurrentDirectory + dirName + "/",
                        Name = dirName,
                        IsDirectory = true,
                        IsLocal = true
                    }));

            list.AddRange(
                this.storageService
                    .GetFileNames(args.CurrentDirectory)
                    .Select(fileName => new IOListing
                    {
                        FullPath = args.CurrentDirectory + fileName,
                        Name = fileName,
                        IsDirectory = false,
                        IsLocal = true
                    }));

            return list;
        }

        private async Task<IEnumerable<IOListing>> LoadRemoteAsync(FilerArgs args)
        {
            var list = await this.filerWebHandler.GetDirectoryListingAsync(args, this.ObtainCancellationToken());

            if (list == null)
            {
                throw new IOListingNullException("IOListing empty");
            }

            return list;
        }

        public class FilerArgs
        {
            /// <summary>
            /// Gets or sets the path to the current directory (relative to the StorageService)
            /// </summary>
            public string CurrentDirectory { get; set; }
        }
    }
}

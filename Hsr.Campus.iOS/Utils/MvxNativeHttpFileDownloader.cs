// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MvvmCross.Platform.Core;
    using MvvmCross.Plugins.DownloadCache;

    public class MvxNativeHttpFileDownloader
        : MvxLockableObject, IMvxHttpFileDownloader
    {
        private const int DefaultMaxConcurrentDownloads = 20;

        private readonly Dictionary<MvxNativeFileDownloadRequest, bool> currentRequests =
            new Dictionary<MvxNativeFileDownloadRequest, bool>();

        private readonly int maxConcurrentDownloads;
        private readonly Queue<MvxNativeFileDownloadRequest> queuedRequests = new Queue<MvxNativeFileDownloadRequest>();

        public MvxNativeHttpFileDownloader(int maxConcurrentDownloads = DefaultMaxConcurrentDownloads)
        {
            this.maxConcurrentDownloads = maxConcurrentDownloads;
        }

        public void RequestDownload(string url, string downloadPath, Action success, Action<Exception> error)
        {
            var request = new MvxNativeFileDownloadRequest(url, downloadPath);
            request.DownloadComplete += (sender, args) =>
            {
                this.OnRequestFinished(request);
                success();
            };
            request.DownloadFailed += (sender, args) =>
            {
                this.OnRequestFinished(request);
                error(args.Value);
            };

            this.RunSyncOrAsyncWithLock(() =>
               {
                   this.queuedRequests.Enqueue(request);
                   if (this.currentRequests.Count < this.maxConcurrentDownloads)
                   {
                       this.StartNextQueuedItem();
                   }
               });
        }

        private void OnRequestFinished(MvxNativeFileDownloadRequest request)
        {
            this.RunSyncOrAsyncWithLock(() =>
                {
                    this.currentRequests.Remove(request);
                    if (this.queuedRequests.Any())
                    {
                        this.StartNextQueuedItem();
                    }
                });
        }

        private void StartNextQueuedItem()
        {
            if (this.currentRequests.Count >= this.maxConcurrentDownloads)
            {
                return;
            }

            this.RunSyncOrAsyncWithLock(() =>
                {
                    if (this.currentRequests.Count >= this.maxConcurrentDownloads || !this.queuedRequests.Any())
                    {
                        return;
                    }

                    var request = this.queuedRequests.Dequeue();
                    this.currentRequests.Add(request, true);
                    request.Start();
                });
        }
    }
}

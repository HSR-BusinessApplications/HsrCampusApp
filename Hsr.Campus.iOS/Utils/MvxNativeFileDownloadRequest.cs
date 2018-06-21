// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.IO;
    using System.Net;
    using Hsr.Campus.Core.ApplicationServices;
    using Hsr.Campus.Core.Infrastructure;
    using MvvmCross.Platform;
    using MvvmCross.Platform.Core;
    using MvvmCross.Plugins.DownloadCache;

    public class MvxNativeFileDownloadRequest
    {
        private readonly IHttpClientConfiguration httpClientConfiguration;
        private readonly IDevice device;
        private readonly IServiceApi serviceApi;

        public MvxNativeFileDownloadRequest(string url, string downloadPath)
        {
            this.Url = url;
            this.DownloadPath = downloadPath;

            this.httpClientConfiguration = Mvx.Resolve<IHttpClientConfiguration>();
            this.device = Mvx.Resolve<IDevice>();
            this.serviceApi = Mvx.Resolve<IServiceApi>();
        }

        public event EventHandler<MvxFileDownloadedEventArgs> DownloadComplete;

        public event EventHandler<MvxValueEventArgs<Exception>> DownloadFailed;

        public string DownloadPath { get; }

        public string Url { get; }

        public void Start()
        {
            try
            {
                var webClient = new NativeHttpClient(this.httpClientConfiguration);

                webClient
                    .AssignAgent(this.device)
                    .AssignAuthToken(this.serviceApi.News);

                webClient.GetStreamAsync(this.Url).ContinueWith(task => this.ProcessResponse(task.Result));
            }
            catch (Exception e)
            {
                this.FireDownloadFailed(e);
            }
        }

        private void ProcessResponse(Stream stream)
        {
            try
            {
                var fileService = MvxFileStoreHelper.SafeGetFileStore();
                var tempFilePath = this.DownloadPath + ".tmp";

                fileService.WriteFile(
                    tempFilePath,
                    (fileStream) =>
                    {
                        var buffer = new byte[4 * 1024];
                        int count;
                        while ((count = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            fileStream.Write(buffer, 0, count);
                        }
                    });

                fileService.TryMove(tempFilePath, this.DownloadPath, true);
            }
            catch (Exception exception)
            {
                this.FireDownloadFailed(exception);
                return;
            }

            this.FireDownloadComplete();
        }

        private void FireDownloadFailed(Exception exception)
        {
            this.DownloadFailed?.Invoke(this, new MvxValueEventArgs<Exception>(exception));
        }

        private void FireDownloadComplete()
        {
            this.DownloadComplete?.Invoke(this, new MvxFileDownloadedEventArgs(this.Url, this.DownloadPath));
        }
    }
}

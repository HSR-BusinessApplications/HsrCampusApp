// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS.PlatformServices
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using Foundation;
    using Hsr.Campus.Core.ApplicationServices;
    using Hsr.Campus.Core.Infrastructure;
    using Hsr.Campus.Core.Model;
    using Hsr.Campus.Core.Resources;
    using UIKit;

    public class IioStorageService : IioAbstract, IIOStorageService
    {
        private readonly IDevice device;
        private readonly IHttpClientConfiguration httpClientConfiguration;
        private readonly IUserInteractionService userInteraction;

        public IioStorageService(IDevice device, IHttpClientConfiguration httpClientConfiguration, IUserInteractionService userInteraction)
        {
            this.device = device;
            this.httpClientConfiguration = httpClientConfiguration;
            this.userInteraction = userInteraction;
        }

        protected override string BaseDir => Path.Combine(
            NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User)[0].Path,
            "Store/");

        public static UIWindow GetTopWindow()
        {
            return UIApplication.SharedApplication
                .Windows
                .Reverse()
                .FirstOrDefault(x =>
                    x.WindowLevel == UIWindowLevel.Normal &&
                    !x.Hidden);
        }

        public void StartOpenFile(string appPath)
        {
            using (var controller = UIDocumentInteractionController.FromUrl(NSUrl.FromFilename(this.Map(appPath))))
            {
                UIApplication.SharedApplication.InvokeOnMainThread(() =>
                {
                    controller.Delegate = new FileViewerInterationDelegate();
                    var success = controller.PresentPreview(true);

                    if (!success)
                    {
                        this.userInteraction.Dialog(AppResources.OpeningFile, string.Format(AppResources.OpeningError, appPath));
                    }
                });
            }
        }

        public IEnumerable<string> GetDirectoryNames(string appPath)
        {
            return Directory.GetDirectories(this.Map(appPath), "*").Select(dir => dir.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Last());
        }

        public IEnumerable<string> GetFileNames(string appPath)
        {
            return Directory.GetFiles(this.Map(appPath), "*").Select(file => file.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Last());
        }

        public async void Download(IOListing io, Action<ResultState> complete)
        {
            var webClient = new NativeHttpClient(this.httpClientConfiguration);

            webClient
                .AssignAgent(this.device);

            var stream = await webClient.GetStreamAsync(io.Url);

            await this.WriteFileAsync(this.Map(io.FullPath), stream);

            complete.Invoke(ResultState.Success);
        }

        public class FileViewerInterationDelegate : UIDocumentInteractionControllerDelegate
        {
            private readonly UIViewController controller;
            private readonly UIView view;

            public FileViewerInterationDelegate()
            {
                this.controller = GetTopViewController();
                this.view = GetTopView();
            }

            public static UIViewController GetTopViewController()
            {
                var root = GetTopWindow().RootViewController;
                var tabs = root as UITabBarController;
                if (tabs != null)
                {
                    return tabs.SelectedViewController;
                }

                var nav = root as UINavigationController;
                if (nav != null)
                {
                    return nav.VisibleViewController;
                }

                return root.PresentedViewController ?? root;
            }

            public static UIView GetTopView() => GetTopWindow().Subviews.Last();

            public override UIViewController ViewControllerForPreview(UIDocumentInteractionController controller) => this.controller;

            public override UIView ViewForPreview(UIDocumentInteractionController controller) => this.view;
        }
    }
}

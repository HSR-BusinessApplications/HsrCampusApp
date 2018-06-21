// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS
{
    using System;
    using System.Threading;
    using Core;
    using Core.ApplicationServices;
    using Core.ViewModels;
    using CoreGraphics;
    using Foundation;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.iOS.Platform;
    using MvvmCross.Platform;
    using UIKit;

    [Register("AppDelegate")]
    public partial class AppDelegate : MvxApplicationDelegate
    {
        private UIWindow window;

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            this.window = new UIWindow();

            var setup = new Setup(this, this.window);
            setup.Initialize();

            var startup = Mvx.Resolve<IMvxAppStart>();
            startup.Start();

            this.window.MakeKeyAndVisible();

            UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval(UIApplication.BackgroundFetchIntervalMinimum);
            UIApplication.SharedApplication.SetStatusBarHidden(false, UIStatusBarAnimation.None);
            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);

            UINavigationBar.Appearance.TintColor = UIColor.White;
            UINavigationBar.Appearance.BarTintColor = Constants.HsrBlue;
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes { TextColor = UIColor.White });

            return true;
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            var startup = Mvx.Resolve<IMvxAppStart>();

            const string prefix = "hsrcampus://";

            startup.Start(new StartUpHint
            {
                ViewModel = typeof(AccountViewModel),
                Parameter = new AccountViewModel.Args
                {
                    UriPart = url.ToString().Substring(url.ToString().IndexOf(prefix, StringComparison.Ordinal) + prefix.Length)
                }
            });

            return true;
        }

        public override void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
        {
            var account = Mvx.Resolve<IAccountService>();

            if (!account.HasAccount)
            {
                completionHandler(UIBackgroundFetchResult.NoData);
                return;
            }

            try
            {
                var accountBalanceSync = Mvx.Resolve<IAccountBalanceSync>();

                accountBalanceSync.UpdateAsync(CancellationToken.None).Wait();

                var adunisSync = Mvx.Resolve<IAdunisSync>();

                adunisSync.UpdateAsync(true, CancellationToken.None).Wait();

                completionHandler(UIBackgroundFetchResult.NewData);
            }
            catch
            {
                completionHandler(UIBackgroundFetchResult.Failed);
            }
        }
    }
}

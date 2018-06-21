// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS.PlatformServices
{
    using System;
    using Hsr.Campus.Core.ApplicationServices;
    using Hsr.Campus.Core.Resources;
    using UIKit;

    public class UserInteractionService
        : IUserInteractionService
    {
        public void Notification(string title, string content, Action after)
        {
            this.Dialog(title, content, after);
        }

        public void Toast(string content, ToastTime showFor)
        {
            this.Dialog(AppResources.ApplicationTitle, content);
        }

        public void Dialog(string title, string content, Action afterOk = null, Action afterCancel = null)
        {
            var hasCancel = afterCancel != null;

            var dialog = UIAlertController.Create(title, content, UIAlertControllerStyle.Alert);

            dialog.AddAction(UIAlertAction.Create(AppResources.ActionOk, UIAlertActionStyle.Default, (obj) => afterOk?.Invoke()));

            if (afterCancel != null)
            {
                dialog.AddAction(UIAlertAction.Create(AppResources.ActionCancel, UIAlertActionStyle.Cancel, (obj) => afterCancel.Invoke()));
            }

            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(dialog, true, null);
        }
    }
}

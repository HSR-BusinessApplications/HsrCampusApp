// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.PlatformServices
{
    using System;
    using Android.App;
    using Android.Content;
    using Android.Views;
    using Android.Widget;
    using Core.ApplicationServices;
    using MvvmCross.Platform;
    using MvvmCross.Platform.Droid.Platform;

    public class UserInteractionService : IUserInteractionService
    {
        private bool buttonPressd;

        public void Notification(string title, string content, Action after)
        {
            // Not available in Android -> Use Toast instead
        }

        public void Toast(string content, ToastTime showFor)
        {
            var context = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            var toast = Android.Widget.Toast.MakeText(
                context.Activity,
                content,
                showFor == ToastTime.Short ? ToastLength.Short : ToastLength.Long);
            toast.Show();
        }

        public void Dialog(string title, string content, Action afterOk = null, Action afterCancel = null)
        {
            var context = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            var builder = new AlertDialog.Builder(context.Activity);
            builder.SetTitle(title);
            builder.SetMessage(content);
            builder.SetOnDismissListener(new OnDismissListener(this.OnDismiss(afterCancel ?? afterOk)));

            if (afterOk != null)
            {
                builder.SetPositiveButton(Android.Resource.String.Ok, (sender, args) =>
                {
                    this.buttonPressd = true;
                    afterOk.Invoke();
                });
            }

            if (afterCancel != null)
            {
                builder.SetNegativeButton(Android.Resource.String.Cancel, (sender, args) =>
                {
                    this.buttonPressd = true;
                    afterCancel.Invoke();
                });
            }

            builder.Create().Show();
        }

        private Action OnDismiss(Action action)
        {
            return () =>
            {
                if (!this.buttonPressd)
                {
                    action?.Invoke();
                }

                this.buttonPressd = false;
            };
        }

        private sealed class OnDismissListener : Java.Lang.Object, IDialogInterfaceOnDismissListener
        {
            private readonly Action action;

            public OnDismissListener(Action action)
            {
                this.action = action;
            }

            public void OnDismiss(IDialogInterface dialog)
            {
                this.action.Invoke();
            }
        }
    }
}

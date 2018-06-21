// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.PlatformServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Support.V4.Content;
    using Android.Webkit;
    using Core.ApplicationServices;
    using Core.Model;
    using Core.Resources;
    using Java.IO;
    using MvvmCross.Platform;
    using MvvmCross.Platform.Droid.Platform;

    public class IOStorageService : IOAbstractService, IIOStorageService
    {
        private const string ProviderAuthority = "ch.hsr.apps.campus.file.provider";
        private readonly IUserInteractionService userInteraction;
        private readonly IMvxAndroidCurrentTopActivity context;

        public IOStorageService(IUserInteractionService userInteraction, IMvxAndroidCurrentTopActivity context)
        {
            this.userInteraction = userInteraction;
            this.context = context;
        }

        protected override File BaseDir => new File(this.context.Activity.GetExternalFilesDir(null), "filercache");

        public void StartOpenFile(string appPath)
        {
            try
            {
                // Create URI
                var file = new File(this.BaseDir, appPath);
                var uri = Android.Net.Uri.FromFile(file);
                var extension = MimeTypeMap.GetFileExtensionFromUrl(uri.ToString());
                var mimeType = MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);

                var intentUri = Build.VERSION.SdkInt >= BuildVersionCodes.M
                    ? FileProvider.GetUriForFile(this.context.Activity, ProviderAuthority, file)
                    : uri;

                var intent = new Intent();
                intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                intent.SetAction(Intent.ActionView);
                intent.SetDataAndType(intentUri, mimeType);

                var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
                if (intent.ResolveActivity(activity.PackageManager) == null)
                {
                    intent.SetDataAndType(uri, "*/*");
                }

                activity.StartActivityForResult(intent, 10);
            }
            catch
            {
                this.userInteraction.Toast("Failed to open.", ToastTime.Medium);
            }
        }

        public IEnumerable<string> GetDirectoryNames(string appPath)
        {
            var dir = new File(this.BaseDir, appPath);

            return (from t in dir.ListFiles() where t.IsDirectory && !t.IsHidden select t.Name).ToList();
        }

        public IEnumerable<string> GetFileNames(string appPath)
        {
            var dir = new File(this.BaseDir, appPath);

            return (from t in dir.ListFiles() where t.IsFile && !t.IsHidden select t.Name).ToList();
        }

        public void Download(IOListing io, Action<ResultState> complete)
        {
            this.AssurePath(io.FullPath);

            var dm = (DownloadManager)this.context.Activity.GetSystemService(Context.DownloadService);

            var uri = Android.Net.Uri.Parse(io.Url);

            var enocedUri = io.Url.Replace(uri.Path, Android.Net.Uri.Encode(uri.Path).Replace("%2F", "/"));

            var req = new DownloadManager.Request(Android.Net.Uri.Parse(enocedUri));

            var path = this.Map(io.FullPath);

            req.AllowScanningByMediaScanner();
            req.SetDestinationUri(Android.Net.Uri.FromFile(new File(path)));
            req.SetTitle(io.Name);

            var downlaodId = dm.Enqueue(req);

            Action<ResultState> action = state =>
            {
                complete.Invoke(state);
            };

            this.context.Activity.RegisterReceiver(
                new DownloadCompleteReceiver(action, downlaodId, this.userInteraction),
                new IntentFilter(DownloadManager.ActionDownloadComplete));
        }

        private class DownloadCompleteReceiver : BroadcastReceiver
        {
            private readonly long downloadId;
            private readonly IUserInteractionService userInteraction;
            private readonly Action<ResultState> completeAction;

            public DownloadCompleteReceiver(Action<ResultState> completeAction, long downloadId, IUserInteractionService userInteraction)
            {
                this.completeAction = completeAction;
                this.downloadId = downloadId;
                this.userInteraction = userInteraction;
            }

            public override void OnReceive(Context context, Intent intent)
            {
                var receivedDownloadId = intent.GetLongExtra(DownloadManager.ExtraDownloadId, 0L);

                if (receivedDownloadId != this.downloadId)
                {
                    return;
                }

                var downloadManager = (DownloadManager)context.GetSystemService(Context.DownloadService);
                var query = new DownloadManager.Query();
                query.SetFilterById(receivedDownloadId);
                var cursor = downloadManager.InvokeQuery(query);
                try
                {
                    if (cursor.MoveToFirst())
                    {
                        var statusIndex = cursor.GetColumnIndex(DownloadManager.ColumnStatus);
                        if (cursor.GetInt(statusIndex) == (int)DownloadStatus.Successful)
                        {
                            this.completeAction.Invoke(ResultState.Success);
                        }
                        else
                        {
                            var reason = cursor.GetInt(cursor.GetColumnIndex(DownloadManager.ColumnReason));
                            this.userInteraction.Toast(AppResources.DownloadingError.FormatWith(reason), ToastTime.Medium);
                            this.completeAction.Invoke(ResultState.Error);
                        }
                    }
                    else
                    {
                        // The download does not exist anymore
                        this.completeAction.Invoke(ResultState.Error);
                    }
                }
                finally
                {
                    context.UnregisterReceiver(this);
                }
            }
        }
    }
}

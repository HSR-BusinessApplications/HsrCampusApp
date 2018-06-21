// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.PlatformServices
{
    using System.IO;
    using System.Security.Cryptography;
    using Core.ApplicationServices;
    using MvvmCross.Platform;
    using MvvmCross.Platform.Droid.Platform;
    using File = Java.IO.File;

    public class IOCacheService : IOAbstractService, IIOCacheService
    {
        protected override File BaseDir
        {
            get
            {
                var context = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
                return context.Activity.CacheDir;
            }
        }

        public byte[] GetSha1HashFromFile(string appPath)
        {
            if (!this.FileExists(appPath))
            {
                return new byte[0];
            }

            var sha1 = new SHA1Managed();
            var file = new File(this.BaseDir, appPath);
            using (var filestream = new FileStream(file.AbsolutePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {
                return sha1.ComputeHash(filestream);
            }
        }

        public void WriteBitmap(string appPath, byte[] data)
        {
            this.AssurePath(appPath);

            var file = new File(this.BaseDir, appPath);
            file.SetWritable(true);

            using (var fos = new FileStream(file.AbsolutePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {
                fos.Write(data, 0, data.Length);
                fos.Close();
            }
        }
    }
}

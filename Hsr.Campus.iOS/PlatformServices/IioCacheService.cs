// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS.PlatformServices
{
    using System.IO;
    using System.Security.Cryptography;
    using Foundation;
    using Hsr.Campus.Core.ApplicationServices;

    public class IioCacheService : IioAbstract, IIOCacheService
    {
        protected override string BaseDir => Path.Combine(
            NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User)[0].Path,
            "AppData/");

        public byte[] GetSha1HashFromFile(string appPath)
        {
            if (!this.FileExists(appPath))
            {
                return new byte[0];
            }

            var sha1 = new SHA1Managed();
            using (var filestream = File.OpenRead(this.Map(appPath)))
            {
                return sha1.ComputeHash(filestream);
            }
        }

        public void WriteBitmap(string appPath, byte[] data)
        {
            this.AssurePath(appPath);

            File.WriteAllBytes(this.Map(appPath), data);

            NSFileManager.SetSkipBackupAttribute(this.Map(appPath), false);
        }
    }
}

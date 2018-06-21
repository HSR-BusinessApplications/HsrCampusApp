// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Droid.PlatformServices
{
    using System.IO;
    using System.Threading.Tasks;
    using Core.ApplicationServices;
    using File = Java.IO.File;

    public abstract class IOAbstractService : IIOService
    {
        protected abstract File BaseDir { get; }

        public bool FileExists(string appPath)
        {
            var file = new File(this.BaseDir, appPath);
            return file.IsFile && file.Exists();
        }

        public bool DirectoryExists(string appPath)
        {
            var file = new File(this.BaseDir, appPath);
            return file.IsDirectory && file.Exists();
        }

        public void AssurePath(string appPath)
        {
            var file = new File(this.BaseDir, appPath);

            if (appPath.IndexOf('.') > -1)
            {
                file = file.ParentFile;
            }

            file.Mkdirs();
        }

        public string Map(string appPath)
        {
            var file = new File(this.BaseDir, appPath);
            return file.AbsolutePath;
        }

        public void Delete(string appPath)
        {
            var file = new File(this.BaseDir, appPath);
            file.Delete();
        }

        public void ClearAll()
        {
            var fileList = this.BaseDir.ListFiles();
            if (fileList == null)
            {
                return;
            }

            foreach (var file in fileList)
            {
                DeleteRecursive(file);
            }
        }

        public CacheUsageSummary Usage() => DirSize(this.BaseDir);

        public Task WriteFileAsync(string appPath, Stream stream)
        {
            return Task.Run(() =>
            {
                this.AssurePath(appPath);

                var file = new File(this.BaseDir, appPath);

                file.SetWritable(true);

                using (var fos = new FileStream(file.AbsolutePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
                {
                    stream.CopyTo(fos);
                    fos.Close();
                }
            });
        }

        private static void DeleteRecursive(File fileOrDirectory)
        {
            if (fileOrDirectory.IsDirectory)
            {
                var fileList = fileOrDirectory.ListFiles();
                if (fileList == null)
                {
                    return;
                }

                foreach (var file in fileList)
                {
                    DeleteRecursive(file);
                }
            }

            fileOrDirectory.Delete();
        }

        private static CacheUsageSummary DirSize(File dir)
        {
            var result = default(CacheUsageSummary);

            if (dir == null || !dir.Exists())
            {
                return result;
            }

            foreach (var file in dir.ListFiles())
            {
                if (file == null)
                {
                    continue;
                }

                // Recursive call if it's a directory
                if (file.IsDirectory)
                {
                    result.Add(DirSize(file));
                }
                else
                {
                    // Sum the file size in bytes
                    result.ByteCount += file.Length();
                    result.FileCount += 1;
                }
            }

            return result; // return the file size
        }
    }
}

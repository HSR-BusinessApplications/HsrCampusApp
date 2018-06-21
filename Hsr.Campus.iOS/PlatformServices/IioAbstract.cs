// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.iOS.PlatformServices
{
    using System.IO;
    using System.Threading.Tasks;
    using Core.ApplicationServices;

    public abstract class IioAbstract : IIOService
    {
        protected abstract string BaseDir { get; }

        public bool FileExists(string appPath) => File.Exists(this.Map(appPath));

        public bool DirectoryExists(string appPath) => Directory.Exists(this.Map(appPath));

        /// <summary>
        /// Ensures that the directory exists. If not the it will try to create it.
        /// </summary>
        /// <param name="appPath">Relative path to the Application Sandbox</param>
        public void AssurePath(string appPath)
        {
            var directoryPath = this.Map(appPath.Substring(0, appPath.LastIndexOf('/')));

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        /// <summary>
        /// Creates an absolute path from a relative one
        /// </summary>
        /// <param name="appPath">Relative path to the Application Sandbox</param>
        /// <returns>Absolute path</returns>
        public string Map(string appPath) => Path.Combine(this.BaseDir, appPath);

        public void Delete(string appPath)
        {
            File.Delete(this.Map(appPath));
        }

        public void ClearAll()
        {
            Directory.Delete(this.BaseDir, true);
        }

        /// <inheritdoc />
        public CacheUsageSummary Usage()
        {
            return this.UsageRecursive(string.Empty);
        }

        public async Task WriteFileAsync(string appPath, Stream stream)
        {
            this.AssurePath(appPath);

            using (var fs = new FileStream(this.Map(appPath), FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                await stream.CopyToAsync(fs);
                fs.SetLength(stream.Length);
                stream.Close();
            }
        }

        /// <summary>
        /// Calculates the usage of the directory <paramref name="appPath"/> (including sub directories)
        /// </summary>
        /// <param name="appPath">Path of the directory</param>
        /// <returns>Summary of the usage</returns>
        private CacheUsageSummary UsageRecursive(string appPath)
        {
            var usage = default(CacheUsageSummary);

            foreach (var dirName in Directory.GetDirectories(this.Map(appPath), "*"))
            {
                usage.Add(this.UsageRecursive(appPath + dirName + "/"));
            }

            foreach (var fileName in Directory.GetFiles(this.Map(appPath), "*"))
            {
                usage.FileCount++;
                usage.ByteCount += new FileInfo(appPath + fileName).Length;
            }

            return usage;
        }
    }
}

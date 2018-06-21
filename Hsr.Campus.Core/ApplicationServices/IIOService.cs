// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ApplicationServices
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IIOService
    {
        bool FileExists(string appPath);

        bool DirectoryExists(string appPath);

        /// <summary>
        /// Ensures that the directory stated in <paramref name="appPath"/> exists
        /// If it does not exist it will be created
        /// </summary>
        /// <param name="appPath">Directory which should exist</param>
        void AssurePath(string appPath);

        /// <summary>
        /// Extends an IOService related path to an app related path
        /// </summary>
        /// <param name="appPath">Relative path in relation to the IOService</param>
        /// <returns>Relative path in relation to the app</returns>
        string Map(string appPath);

        void Delete(string appPath);

        void ClearAll();

        /// <summary>
        /// Shows the current occupancy of the cache
        /// </summary>
        /// <returns>Summary of the occupancy of the cache</returns>
        CacheUsageSummary Usage();

        /// <summary>
        /// Writes the content of <paramref name="stream"/> in the file appPath
        /// The file will be created or overwritten
        /// </summary>
        /// <param name="appPath">Relative path in relation to the IOService</param>
        /// <param name="stream">Content that should be written to the file</param>
        /// <returns>A <see cref="Task"/> which represents the asynchronous operation</returns>
        Task WriteFileAsync(string appPath, Stream stream);
    }
}

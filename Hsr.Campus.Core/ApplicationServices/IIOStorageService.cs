// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ApplicationServices
{
    using System;
    using System.Collections.Generic;
    using Model;

    public interface IIOStorageService : IIOService
    {
        void StartOpenFile(string appPath);

        /// <summary>
        /// Shows the names of all folders which reside in the stated path
        /// </summary>
        /// <param name="appPath">Relative path in relation to the StorageService</param>
        /// <returns>An enumeration of all folder names in the path <paramref name="appPath"/></returns>
        IEnumerable<string> GetDirectoryNames(string appPath);

        /// <summary>
        /// Shows the names of all files which reside in the stated path
        /// </summary>
        /// <param name="appPath">Relative path in relation to the StorageService</param>
        /// <returns>An enumeration of all file names in the path <paramref name="appPath"/></returns>
        IEnumerable<string> GetFileNames(string appPath);

        void Download(IOListing io, Action<ResultState> complete);
    }
}

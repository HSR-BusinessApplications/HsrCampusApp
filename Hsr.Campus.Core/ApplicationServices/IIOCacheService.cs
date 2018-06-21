// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ApplicationServices
{
    public interface IIOCacheService : IIOService
    {
        /// <summary>
        /// Returns the SHA1 hash of the stated file or null if the file does not exist
        /// </summary>
        /// <param name="appPath">Relative path in relation to the IOService</param>
        /// <returns>Array with the 20 Bytes of the SHA1 or null if the stated file does not exist</returns>
        byte[] GetSha1HashFromFile(string appPath);

        void WriteBitmap(string appPath, byte[] data);
    }
}

// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ApplicationServices
{
    /// <summary>
    /// A summary which describes how the cache is occupied
    /// </summary>
    public struct CacheUsageSummary
    {
        /// <summary>
        /// Gets or sets the number of files in cache
        /// </summary>
        public long FileCount { get; set; }

        /// <summary>
        /// Gets or sets the number of bytes (from the files) in cache
        /// </summary>
        public long ByteCount { get; set; }

        /// <summary>
        /// Adds the values of <paramref name="usage"/> to this summary
        /// </summary>
        /// <param name="usage">Summary which will be added to this summary</param>
        public void Add(CacheUsageSummary usage)
        {
            this.FileCount += usage.FileCount;
            this.ByteCount += usage.ByteCount;
        }
    }
}

// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.DomainServices
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Model;

    public interface IMapWebHandler
    {
        /// <summary>
        /// Asynchronous loader for the list of all buildings from the server
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> with which the operation can be cancelled</param>
        /// <returns>Information about all the buildings</returns>
        /// <exception cref="HttpRequestException">Is thrown when the HttpRequest failed</exception>
        Task<MapOverview> GetHashesAsync(CancellationToken cancellationToken);

        Task<Stream> GetImageStreamAsync(Guid id);
    }
}

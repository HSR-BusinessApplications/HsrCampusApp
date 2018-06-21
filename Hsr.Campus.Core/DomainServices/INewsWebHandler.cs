// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.DomainServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Model;

    public interface INewsWebHandler
    {
        Task<IEnumerable<WhNewsFeed>> GetFeedsAsync(CancellationToken cancellationToken);

        Task<byte[]> GetFeedIconAsync(SqlNewsFeed feed, int size);

        /// <summary>
        /// Shows the posts to a specific feed
        /// </summary>
        /// <param name="feed">Defines the feed (Facebook- Page or Group-ID)</param>
        /// <param name="before">Defines the date of the newest post to be returned</param>
        /// <param name="cancellationToken">Token with which the asynchronous operation can be cancelled</param>
        /// <returns>News objects or <c>null</c> if the operation is cancelled</returns>
        Task<IEnumerable<WhNews>> GetNewsEntriesAsync(SqlNewsFeed feed, DateTime before, CancellationToken cancellationToken);

        /// <summary>
        /// Represents the URL to an image related to the post
        /// </summary>
        /// <param name="newsId">News ID of the post</param>
        /// <returns>String of the URL</returns>
        string PictureUrl(string newsId);
    }
}

// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.DomainServices
{
    using System;
    using System.Collections.Generic;
    using Model;

    public interface INewsRepository
    {
        DateTime LastUpdated { get; }

        void UpdateFeeds(IEnumerable<WhNewsFeed> feeds);

        IEnumerable<SqlNewsFeed> RetrieveFeeds();

        void UpdateIcon(SqlNewsFeed feed, byte[] icon, int size);

        IEnumerable<SqlNews> UpdateNewsRange(SqlNewsFeed feed, IEnumerable<WhNews> newPosts);

        bool HasNewsBefore(SqlNewsFeed feed, DateTime before);

        IEnumerable<SqlNews> RetrieveNewsBefore(SqlNewsFeed feed, DateTime before, int amount);

        void DeleteNewsEntries(SqlNewsFeed oldFeed);

        void Truncate();
    }
}

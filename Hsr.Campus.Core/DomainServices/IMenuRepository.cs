// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.DomainServices
{
    using System;
    using System.Collections.Generic;
    using Model;

    public interface IMenuRepository
    {
        DateTime LastUpdated { get; }

        void UpdateFeeds(IEnumerable<WhMenuFeed> feeds);

        IEnumerable<SqlMenuFeed> RetrieveFeeds();

        void UpdateMenu(string id, string feedId, WhMenuDay day, string menuHtml);

        IEnumerable<SqlMenu> RetrieveMenus(string feedId);

        SqlMenu RetrieveMenu(string id);

        string MarkMenus();

        void UnmarkMenu(string id);

        void DeleteMarkedMenus(string marker);

        void Truncate();
    }
}

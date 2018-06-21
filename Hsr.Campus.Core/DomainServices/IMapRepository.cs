// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.DomainServices
{
    using System;
    using System.Collections.Generic;
    using Model;

    public interface IMapRepository
    {
        MapHashable RetrieveRoot();

        /// <summary>
        /// Retrieves the maps that have <paramref name="parentId"/> as parent
        /// </summary>
        /// <param name="parentId">ID of the map for which the child maps should be returned</param>
        /// <returns>List of all child maps</returns>
        IEnumerable<MapHashable> RetrieveEntries(Guid parentId);

        void PersistEntry(MapHashable entry);

        string Mark();

        void CleanUp(string marker);
    }
}

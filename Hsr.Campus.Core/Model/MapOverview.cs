// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using SQLite;

    [DataContract]
    public class MapOverview
        : MapHashable
    {
        [DataMember]
        [Ignore]
        public IEnumerable<MapBuilding> Buildings { get; set; }
    }
}

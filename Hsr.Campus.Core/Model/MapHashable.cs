// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System;
    using System.Runtime.Serialization;
    using SQLite;
    using ViewModels;

    [Table("Hashable")]
    [DataContract]
    public class MapHashable : ITitled
    {
        [Column("Marker")]
        public string Marker { get; set; }

        [Column("Id")]
        [PrimaryKey]
        [DataMember]
        public Guid Id { get; set; }

        [Column("ParentId")]
        public Guid? ParentId { get; set; }

        [Column("Hash")]
        [DataMember]
        public string Hash { get; set; }

        [Column("Name")]
        [DataMember]
        public string Name { get; set; }

        [Column("Coordinates")]
        [DataMember(IsRequired = false)]
        public string Coordinates { get; set; }

        [IgnoreDataMember]
        public string ImagePath => $"Map/{this.Id}.png";

        [Ignore]
        [IgnoreDataMember]
        public string Title
        {
            get { return this.Name; }
        }
    }
}

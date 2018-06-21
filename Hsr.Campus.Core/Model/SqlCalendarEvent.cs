// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System;
    using System.Runtime.Serialization;
    using SQLite;

    [DataContract]
    [Table("CalendarEvent2")]
    public class SqlCalendarEvent
    {
        [Column("EnentId")]
        [PrimaryKey]
        public string EventID { get; set; }

        [Column("Link")]
        public string Link { get; set; }

        [Column("Summary")]
        public string Summary { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Location")]
        public string Location { get; set; }

        [Column("Start")]
        public DateTime Start { get; set; }

        [Column("End")]
        public DateTime End { get; set; }

        [Column("LastUpdate")]
        public DateTime LastUpdated { get; set; }
    }
}

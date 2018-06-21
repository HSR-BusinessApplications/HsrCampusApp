// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System;
    using SQLite;

    [Table("Feeds")]
    public class SqlNewsFeed
    {
        /// <summary>
        /// Gets or sets the key of the feed
        /// The value is used for clear identification and the web request
        /// </summary>
        [Column("Key")]
        [PrimaryKey]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the name of the feed
        /// </summary>
        [Column("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the index of the feed which is used for the ordering of the feeds
        /// </summary>
        [Column("Index")]
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the path to the icon of the feed
        /// </summary>
        [Column("IconPath")]
        public string IconPath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the feed is displayed in the "HSR-Sport" tab
        /// </summary>
        [Column("HsrSport")]
        public bool HsrSport { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the last update of the icon (UTC)
        /// </summary>
        [Column("IconUpdate")]
        public DateTime LastIconUpdate { get; set; }

        /// <summary>
        /// Gets or sets the marker for the identification of deprecated entries
        /// </summary>
        [Column("Marker")]
        public string Marker { get; set; }
    }
}

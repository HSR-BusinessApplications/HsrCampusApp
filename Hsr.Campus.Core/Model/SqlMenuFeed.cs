// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using SQLite;

    [Table("Feeds")]
    public class SqlMenuFeed
    {
        /// <summary>
        /// Gets or sets the ID of the menu feed
        /// The value is used for clear identification and the web request
        /// </summary>
        [Column("Id")]
        [PrimaryKey]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the short name which will be displayed if theres little space
        /// </summary>
        [Column("ShortName")]
        public string ShortName { get; set; }

        /// <summary>
        /// Gets or sets the name of the menu feed
        /// </summary>
        [Column("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the marker for the identification of deprecated entries
        /// </summary>
        [Column("Marker")]
        public string Marker { get; set; }
    }
}

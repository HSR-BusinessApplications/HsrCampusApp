// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System;
    using SQLite;

    [Table("MenuDays")]
    public class SqlMenuDay
    {
        /// <summary>
        /// Gets or sets the ID of the menu feed
        /// </summary>
        [Column("Id")]
        [PrimaryKey]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the entry
        /// </summary>
        [Column("Date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the hash over the content of the menu
        /// </summary>
        [Column("ContentHash")]
        public string ContentHash { get; set; }
    }
}

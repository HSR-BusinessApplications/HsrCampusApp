// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System;
    using SQLite;
    using ViewModels;

    [Table("Menus")]
    public class SqlMenu : ITitled
    {
        /// <summary>
        /// Gets or sets the ID of the menu
        /// </summary>
        [Column("Id")]
        [PrimaryKey]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the menu feed
        /// </summary>
        [Column("FeedId")]
        public string FeedId { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the entry
        /// </summary>
        [Column("Date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the menu HTML code which will be displayed
        /// </summary>
        [Column("HtmlPage")]
        public string HtmlPage { get; set; }

        /// <summary>
        /// Gets or sets the hash over the HTML code
        /// </summary>
        [Column("ContentHash")]
        public string ContentHash { get; set; }

        /// <summary>
        /// Gets or sets the marker for the identification of deprecated entries
        /// </summary>
        [Column("Marker")]
        public string Marker { get; set; }

        [Ignore]
        public string Title
        {
            get { return $"{this.Date:ddd dd.MM}"; }
        }
    }
}

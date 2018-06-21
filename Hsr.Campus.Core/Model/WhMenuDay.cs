// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class WhMenuDay
    {
        private DateTime date;

        /// <summary>
        /// Gets or sets the date and time of the entry
        /// The value is delivered by the webhandler as "Unspecified" and must be marked as UTC because it would be converted when saving to the SQL database otherwise
        /// </summary>
        [DataMember]
        public DateTime Date
        {
            get { return this.date; }
            set { this.date = DateTime.SpecifyKind(value, DateTimeKind.Utc); }
        }

        /// <summary>
        /// Gets or sets the hash over the content of the menu
        /// </summary>
        [DataMember]
        public string ContentHash { get; set; }
    }
}

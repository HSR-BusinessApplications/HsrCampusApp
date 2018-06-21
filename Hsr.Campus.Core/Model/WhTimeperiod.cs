// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class WhTimeperiod
    {
        private DateTime begin;
        private DateTime end;

        /// <summary>
        /// Gets or sets the TermID which is used as the primary key in the SQL database
        /// The value is delivered by the webhandler
        /// Example: 895
        /// </summary>
        [DataMember(Name = "Id")]
        public int TermID { get; set; }

        /// <summary>
        /// Gets or sets the begin of a time period (HSR-Local as UTC)
        /// The value is delivered by the webhandler as "Unspecified" and must be marked as UTC because it would be converted when saving to the SQL database otherwise
        /// Example: 2016-02-22T00:00:00
        /// </summary>
        [DataMember]
        public DateTime Begin
        {
            get { return this.begin; }
            set { this.begin = DateTime.SpecifyKind(value, DateTimeKind.Utc); }
        }

        /// <summary>
        /// Gets or sets the end of a time period (HSR-Local as UTC)
        /// The value is delivered by the webhandler as "Unspecified" and must be marked as UTC because it would be converted when saving to the SQL database otherwise
        /// Example: 2016-02-22T00:00:00
        /// </summary>
        [DataMember]
        public DateTime End
        {
            get { return this.end; }
            set { this.end = DateTime.SpecifyKind(value, DateTimeKind.Utc); }
        }

        /// <summary>
        /// Gets or sets the name of the time period so the user can identify the time period
        /// The value gets defined when saving to the repository and consists of the name of the parent and the name deliverd by the webhandler
        /// Example: FS 2016 - Unterricht
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Adunis type of a time period as a numeric value
        /// The value is delivered by the webhandler
        /// Example: 2
        /// </summary>
        [DataMember]
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the children of a Semester-Type time period and will not be stored in the SQL database
        /// The value is deliverd by the webhandler
        /// Example: [{Id: 895, Begin: 2016-02-22T00:00:00, End: 2016-06-03T23:59:00, Name: Unterricht, Type: 2, Children: null}]
        /// </summary>
        [DataMember]
        public IEnumerable<WhTimeperiod> Children { get; set; }
    }
}

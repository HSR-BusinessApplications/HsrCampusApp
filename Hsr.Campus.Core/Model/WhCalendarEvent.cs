// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class WhCalendarEvent
    {
        private DateTime start;
        private DateTime end;

        [DataMember(Name = "id")]
        public string EventID { get; set; }

        [DataMember(Name = "htmlLink")]
        public string Link { get; set; }

        [DataMember]
        public string Summary { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public DateTimeOffset Start
        {
            get { return this.start; }
            set { this.start = DateTime.SpecifyKind(value.DateTime, DateTimeKind.Utc); }
        }

        [DataMember]
        public DateTimeOffset End
        {
            get { return this.end; }
            set { this.end = DateTime.SpecifyKind(value.DateTime, DateTimeKind.Utc); }
        }
    }
}

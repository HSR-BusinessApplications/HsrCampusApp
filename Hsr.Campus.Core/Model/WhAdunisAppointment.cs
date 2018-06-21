// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract(Name = "Appointment")]
    public class WhAdunisAppointment
    {
        private DateTime startTime;
        private DateTime endTime;

        /// <summary>
        /// Gets or sets the start date and time of an appointment (HSR-Local as UTC)
        /// The value is delivered by the webhandler as "Unspecified" and must be marked as UTC because it would be converted when saving to the SQL database otherwise
        /// Example: 2016-02-23T17:00:00
        /// </summary>
        [DataMember]
        public DateTime StartTime
        {
            get { return this.startTime; }
            set { this.startTime = DateTime.SpecifyKind(value, DateTimeKind.Utc); }
        }

        /// <summary>
        /// Gets or sets the end date and time of an appointment (HSR-Local as UTC)
        /// The value is delivered by the webhandler as "Unspecified" and must be marked as UTC because it would be converted when saving to the SQL database otherwise
        /// Example: 2016-02-23T17:00:00
        /// </summary>
        [DataMember]
        public DateTime EndTime
        {
            get { return this.endTime; }
            set { this.endTime = DateTime.SpecifyKind(value, DateTimeKind.Utc); }
        }

        /// <summary>
        /// Gets or sets the rooms in which appointments occur
        /// Example: 1.267, 1.269
        /// </summary>
        [DataMember]
        public IEnumerable<WhAdunisRoom> AppointmentRooms { get; set; }

        /// <summary>
        /// Gets or sets the lecturers of an appointment
        /// Example: MOI
        /// </summary>
        [DataMember]
        public IEnumerable<WhAdunisLecturer> Lecturers { get; set; }
    }
}

// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract(Name = "CourseAllocation")]
    public class WhAdunisCourseAllocation
    {
        /// <summary>
        /// Gets or sets the day of week in which the appointment occurs
        /// DayOfWeek is used to find all weekdays with at least one appointment
        /// Example: DayOfWeek.Tuesday
        /// </summary>
        [DataMember]
        public DayOfWeek? DayOfWeek { get; set; }

        /// <summary>
        /// Gets or sets the TimeslotID
        /// The TimeslotID is used to identify all courses that have no appointments
        /// Example: 1712
        /// </summary>
        [DataMember]
        public int? TimeslotId { get; set; }

        [DataMember]
        public IEnumerable<WhAdunisAppointment> Appointments { get; set; }
    }
}

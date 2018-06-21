// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SQLite;

    [Table("Timeperiod3")]
    public class SqlTimeperiod
    {
        /// <summary>
        /// Gets or sets the TermID which is used as the primary key in the SQL database
        /// The value is delivered by the webhandler
        /// Example: 895
        /// </summary>
        [Column("TermId")]
        [PrimaryKey]
        public int TermID { get; set; }

        /// <summary>
        /// Gets or sets the parent TermID which is the semester time period this time period belongs to
        /// The ParentTermID is used for CourseData request with the webhandler
        /// The value gets defined when saving to the repository
        /// Example: 773
        /// </summary>
        [Column("ParentID")]
        public int ParentTermID { get; set; }

        /// <summary>
        /// Gets or sets the begin of a time period (HSR-Local as UTC)
        /// The value is delivered by the webhandler
        /// Example: 2016-02-22T00:00:00
        /// </summary>
        [Column("Begin")]
        public DateTime Begin { get; set; }

        /// <summary>
        /// Gets or sets the end of a time period (HSR-Local as UTC)
        /// The value is delivered by the webhandler
        /// Example: 2016-06-03T23:59:00
        /// </summary>
        [Column("End")]
        public DateTime End { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether there are appointments in the local storage for the current time period
        /// The value gets generated when refreshing the course data
        /// </summary>
        [Column("HasAppointments")]
        public bool HasAppointments { get; set; }

        /// <summary>
        /// Gets or sets the name of the time period so the user can identify the time period
        /// The value gets defined when saving to the repository and consists of the parent name and the name delivered by the webhandler
        /// Example: FS 2016 - Unterricht
        /// </summary>
        [Column("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Adunis type of the time period
        /// The value is delivered by the webhandler
        /// Example: AdunisType.Timetable
        /// </summary>
        [Column("Type")]
        public AdunisType Type { get; set; }

        /// <summary>
        /// Gets or sets the identity of the time period which indicates to which user the time period belongs
        /// The value gets defined by the currently logged in user
        /// Example: jhochreu
        /// </summary>
        [Column("Identity")]
        public string Identity { get; set; }

        /// <summary>
        /// Gets or sets a list of all weekdays in which an appointment occurs
        /// The value gets generated when refreshing the course data
        /// To save the list into the database it will be serialized into a string
        /// Example: 1,2,4,5,0
        /// </summary>
        [Column("UniqueDaysOfWeek")]
        public string UniqueDaysOfWeekSerialized { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the last appointment update
        /// The value gets generated when refreshing the course data
        /// Example: 2017-03-25T10:33:43+00:00
        /// </summary>
        [Column("LastAppointmentUpdate")]
        public DateTime LastAppointmentUpdate { get; set; }

        /// <summary>
        /// Gets or sets the marker which is used to detect deprecated data
        /// Before an update the marker will be described with a GUID
        /// After an update all marked entries will be deleted since they will not be delivered by the webhandler anymore
        /// Example: 8deca729-9d16-45ad-ba70-2f9b2dbf34db
        /// </summary>
        [Column("Marker")]
        public string Marker { get; set; }

        public void SetUniqueDays(IEnumerable<DayOfWeek> daysOfWeek)
        {
            this.UniqueDaysOfWeekSerialized = string.Join(",", daysOfWeek.Cast<int>());
        }

        public IEnumerable<DayOfWeek> GetUniqueDays()
        {
            var result = this.UniqueDaysOfWeekSerialized.Split(',');
            return result[0]?.Length == 0 ? new List<DayOfWeek>() : result.Select(s => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), s));
        }
    }
}

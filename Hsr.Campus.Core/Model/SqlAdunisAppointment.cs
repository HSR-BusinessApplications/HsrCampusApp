// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Model
{
    using System;
    using System.Linq;
    using SQLite;

    [Table("Appointment2")]
    public class SqlAdunisAppointment
    {
        // "07:05 - 07:50", "08:10 - 08:55",
        //  "09:05 - 09:50", "10:10 - 10:55",
        //  "11:05 - 11:50", "12:10 - 12:55",
        //  "13:10 - 13:55", "14:05 - 14:50",
        //  "15:10 - 15:55", "16:05 - 16:50",
        //  "17:00 - 17:45", "17:55 - 18:40",
        //  "19:10 - 19:55", "20:05 - 20:50"
        private static readonly DateTime[] Times =
        {
            NewTime(7, 5), NewTime(8, 10),
            NewTime(9, 5), NewTime(10, 10),
            NewTime(11, 5), NewTime(12, 10),
            NewTime(13, 10), NewTime(14, 5),
            NewTime(15, 10), NewTime(16, 5),
            NewTime(17, 0), NewTime(17, 55),
            NewTime(19, 10), NewTime(20, 5)
        };

        /// <summary>
        /// Gets a list of all possible time windows for an appointment
        /// </summary>
        public static string[] Rows { get; } = Times.Select(time => $"{time:t} - {time.AddMinutes(45):t}").ToArray();

        /// <summary>
        /// Gets or sets the appointment id which is used as the primary key in the SQL database
        /// The value is set through the function SetId and consists of the TermID, CourseName and StartTime
        /// Example: 895Jap2-v12016-02-23T17:00:00
        /// </summary>
        [PrimaryKey]
        public string AppointmentID { get; set; }

        /// <summary>
        /// Gets or sets the TermID which is the primary key of the time period this appointment belongs to
        /// The value gets defined when saving to the repository
        /// Example: 895
        /// </summary>
        [Column("TermID")]
        public int TermID { get; set; }

        /// <summary>
        /// Gets or sets the marker which is used to detect deprecated data
        /// Before an update the marker will be described with a GUID
        /// After an update all marked entries will be deleted since they will not be delivered by the webhandler anymore
        /// Example: 8deca729-9d16-45ad-ba70-2f9b2dbf34db
        /// </summary>
        [Column("Marker")]
        public string Marker { get; set; }

        /// <summary>
        /// Gets or sets the Adunis type
        /// The value is delivered by the webhandler
        /// Example: 2
        /// </summary>
        [Column("Type")]
        public AdunisType Type { get; set; }

        /// <summary>
        /// Gets or sets the course name which is the official description of the appointment
        /// The value is delivered by the webhandler
        /// Example: Jap2-v1
        /// </summary>
        [Column("CourseName")]
        public string CourseName { get; set; }

        /// <summary>
        /// Gets or sets the start date and time of an appointment (HSR-Local as UTC)
        /// The value is delivered by the webhandler
        /// Example: 2016-02-23T17:00:00
        /// </summary>
        [Column("StartTime")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end date and time of an appointment (HSR-Local as UTC)
        /// The value is delivered by the webhandler
        /// Example: 2016-02-23T17:45:00
        /// </summary>
        [Column("EndTime")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Gets or sets the appointment rooms in which the appointment will be held
        /// The value is delivered by the webhandler
        /// Example: 1.267, 1.269
        /// </summary>
        [Column("AppointmentRooms")]
        public string AppointmentRooms { get; set; }

        /// <summary>
        /// Gets or sets the lecturers of an appoinment
        /// The value is delivered by the webhandler
        /// Example: MOI
        /// </summary>
        [Column("Lecturers")]
        public string Lecturers { get; set; }

        /// <summary>
        /// Gets or sets the description of an appointment
        /// The value is only used by the Android CalenderSyncAdapter
        /// </summary>
        [Column("Description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets the row in which the appointment should be displayed
        /// </summary>
        public int Row
        {
            get
            {
                var i = 0;
                foreach (var time in Times)
                {
                    if (this.StartTime.TimeOfDay <= time.TimeOfDay)
                    {
                        return i;
                    }

                    i++;
                }

                return 0;
            }
        }

        internal void SetId()
        {
            this.AppointmentID = $"{this.TermID}{this.CourseName}{this.StartTime:s}";
        }

        private static DateTime NewTime(int hour, int minute) => new DateTime(((hour * 3600) + (minute * 60)) * 10000000L, DateTimeKind.Utc);
    }
}

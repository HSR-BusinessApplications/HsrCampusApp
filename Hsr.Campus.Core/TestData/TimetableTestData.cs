// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

// The classes with test data must only be compiled in the Test-Build!
#pragma warning disable SA1124 // Do not use regions
#pragma warning disable SA1201 // Elements must appear in the correct order
#if TEST_DATA
namespace Hsr.Campus.Core.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Newtonsoft.Json;

    public static class TimetableTestData
    {
        private static int year;
        private static bool isSpringSemester;

        private static string firstSemesterName;
        private static DateTime firstSemesterStartDate;
        private static string firstSemesterStart;
        private static DateTime firstSemesterEndDate;
        private static string firstSemesterEnd;
        private static DateTime firstSemesterExamStartDate;
        private static string firstSemesterExamStart;
        private static DateTime firstSemesterExamEndDate;
        private static string firstSemesterExamEnd;

        private static string secondSemesterName;
        private static DateTime secondSemesterStartDate;
        private static string secondSemesterStart;
        private static DateTime secondSemesterEndDate;
        private static string secondSemesterEnd;
        private static DateTime secondSemesterExamStartDate;
        private static string secondSemesterExamStart;
        private static DateTime secondSemesterExamEndDate;
        private static string secondSemesterExamEnd;

        static TimetableTestData()
        {
            InitializeData();
        }

#region TpData
        public static string TpData => Stamp(SetupTpData());

        private static string SetupTpData()
        {
            InitializeData();

            var tPData = new object[]
            {
                new
                {
                    Id = 773,
                    Begin = firstSemesterStart,
                    End = firstSemesterEnd,
                    Name = firstSemesterName,
                    Type = 1,
                    Children = new object[]
                    {
                        new
                        {
                            Id = 895,
                            Begin = firstSemesterStart,
                            End = firstSemesterEnd,
                            Name = "Unterricht",
                            Type = 2
                        },
                        new
                        {
                            Id = 932,
                            Begin = firstSemesterExamStart,
                            End = firstSemesterExamEnd,
                            Name = "<<STAMP>>",
                            Type = 4
                        }
                    }
                },
                new
                {
                    Id = 774,
                    Begin = secondSemesterStart,
                    End = secondSemesterEnd,
                    Name = secondSemesterName,
                    Type = 1,
                    Children = new object[]
                    {
                        new
                        {
                            Id = 896,
                            Begin = secondSemesterStart,
                            End = secondSemesterEnd,
                            Name = "Unterricht",
                            Type = 2
                        }
                    }
                },
                new
                {
                    Id = 100,
                    Begin = secondSemesterStart,
                    End = secondSemesterEnd,
                    Name = "Error",
                    Type = 1,
                    Children = new object[]
                    {
                        new
                        {
                            Id = 101,
                            Begin = secondSemesterStart,
                            End = secondSemesterEnd,
                            Name = "Empty TP",
                            Type = 2
                        },
                        new
                        {
                            Id = 102,
                            Begin = secondSemesterExamStart,
                            End = secondSemesterExamEnd,
                            Type = 4
                        }
                    }
                }
            };

            var json = JsonConvert.SerializeObject(tPData);
            return json;
        }
#endregion

#region ExamData
        public static string ExamData { get; } = SetupExamData();

        private static string SetupExamData()
        {
            InitializeData();

            var exams = new
            {
                Courses = new object[]
                {
                   new
                   {
                       Name = "P_MsTe",
                       CourseAllocations = new object[]
                       {
                           new
                           {
                               Appointments = new object[]
                               {
                                   new
                                   {
                                       StartTime = firstSemesterExamStartDate.AddHours(12).AddMinutes(50).ToString("yyyy-MM-ddTHH:mm:ss"),
                                       EndTime = firstSemesterExamStartDate.AddHours(14).AddMinutes(50).ToString("yyyy-MM-ddTHH:mm:ss"),
                                       AppointmentRooms = new object[]
                                       {
                                           new
                                           {
                                               Nummer = "1.279"
                                           }
                                       },
                                       Lecturers = new object[]
                                       {
                                           new
                                           {
                                               Fullname = "Albert Einstein",
                                               Shortname = "EIA"
                                           }
                                       }
                                   }
                               }
                           }
                       }
                   },
                   new
                   {
                        Name = "P_WED1",
                        CourseAllocations = new object[]
                           {
                               new
                               {
                                   Appointments = new object[]
                                   {
                                       new
                                       {
                                           StartTime = firstSemesterExamStartDate.AddDays(2).AddHours(12).AddMinutes(30).ToString("yyyy-MM-ddTHH:mm:ss"),
                                           EndTime = firstSemesterExamStartDate.AddDays(2).AddHours(14).AddMinutes(30).ToString("yyyy-MM-ddTHH:mm:ss"),
                                           AppointmentRooms = new object[]
                                           {
                                               new
                                               {
                                                   Nummer = "4.101"
                                               }
                                           },
                                           Lecturers = new object[]
                                           {
                                               new
                                               {
                                                   Fullname = "Elon Musk",
                                                   Shortname = "MUE"
                                               }
                                           }
                                       }
                                   }
                               }
                           }
                    },
                    new
                    {
                        Name = "P_WED2",
                        CourseAllocations = new object[]
                           {
                               new
                               {
                                   Appointments = new object[]
                                   {
                                       new
                                       {
                                           StartTime = firstSemesterExamStartDate.AddDays(5).AddHours(10).AddMinutes(10).ToString("yyyy-MM-ddTHH:mm:ss"),
                                           EndTime = firstSemesterExamStartDate.AddDays(5).AddHours(12).AddMinutes(30).ToString("yyyy-MM-ddTHH:mm:ss"),
                                           AppointmentRooms = new object[]
                                           {
                                               new
                                               {
                                                   Nummer = "5.003"
                                               }
                                           },
                                           Lecturers = new object[]
                                           {
                                               new
                                               {
                                                   Fullname = "Elon Musk",
                                                   Shortname = "MUE"
                                               }
                                           }
                                       }
                                   }
                               }
                           }
                    },
                    new
                    {
                        Name = "P_AppArch",
                        CourseAllocations = new object[]
                           {
                               new
                               {
                                   Appointments = new object[]
                                   {
                                       new
                                       {
                                           StartTime = firstSemesterExamStartDate.AddDays(8).AddHours(8).AddMinutes(10).ToString("yyyy-MM-ddTHH:mm:ss"),
                                           EndTime = firstSemesterExamStartDate.AddDays(8).AddHours(10).AddMinutes(10).ToString("yyyy-MM-ddTHH:mm:ss"),
                                           AppointmentRooms = new object[]
                                           {
                                               new
                                               {
                                                   Nummer = "1.277"
                                               }
                                           },
                                           Lecturers = new object[]
                                           {
                                               new
                                               {
                                                   Fullname = "Bill Gates",
                                                   Shortname = "GAB"
                                               }
                                           }
                                       }
                                   }
                               }
                           }
                    },
                    new
                    {
                        Name = "P_InfSi1",
                        CourseAllocations = new object[]
                           {
                               new
                               {
                                   Appointments = new object[]
                                   {
                                       new
                                       {
                                           StartTime = firstSemesterExamStartDate.AddDays(17).AddHours(15).AddMinutes(30).ToString("yyyy-MM-ddTHH:mm:ss"),
                                           EndTime = firstSemesterExamStartDate.AddDays(17).AddHours(16).AddMinutes(30).ToString("yyyy-MM-ddTHH:mm:ss"),
                                           AppointmentRooms = new object[]
                                           {
                                               new
                                               {
                                                   Nummer = "5.001"
                                               }
                                           },
                                           Lecturers = new object[]
                                           {
                                               new
                                               {
                                                   Fullname = "Mark Zuckerberg",
                                                   Shortname = "ZUM"
                                               }
                                           }
                                       }
                                   }
                               }
                           }
                    },
                    new
                    {
                        Name = "P_PhAI",
                        CourseAllocations = new object[]
                           {
                               new
                               {
                                   Appointments = new object[]
                                   {
                                       new
                                       {
                                           StartTime = firstSemesterExamStartDate.AddDays(21).AddHours(15).AddMinutes(10).ToString("yyyy-MM-ddTHH:mm:ss"),
                                           EndTime = firstSemesterExamStartDate.AddDays(21).AddHours(17).AddMinutes(10).ToString("yyyy-MM-ddTHH:mm:ss"),
                                           AppointmentRooms = new object[]
                                           {
                                               new
                                               {
                                                   Nummer = "5.003"
                                               }
                                           },
                                           Lecturers = new object[]
                                           {
                                               new
                                               {
                                                   Fullname = "Stephen Hawking",
                                                   Shortname = "HAS"
                                               }
                                           }
                                       }
                                   }
                               }
                           }
                    },
                    new
                    {
                        Name = "P_BuRe2",
                        CourseAllocations = new object[]
                           {
                               new
                               {
                                   Appointments = new object[]
                                   {
                                       new
                                       {
                                           StartTime = firstSemesterExamStartDate.AddDays(23).AddHours(10).AddMinutes(10).ToString("yyyy-MM-ddTHH:mm:ss"),
                                           EndTime = firstSemesterExamStartDate.AddDays(23).AddHours(12).AddMinutes(30).ToString("yyyy-MM-ddTHH:mm:ss"),
                                           AppointmentRooms = new object[]
                                           {
                                               new
                                               {
                                                   Nummer = "4.101"
                                               }
                                           },
                                           Lecturers = new object[]
                                           {
                                               new
                                               {
                                                   Fullname = "Warren Buffet",
                                                   Shortname = "BUW"
                                               }
                                           }
                                       }
                                   }
                               }
                           }
                    },
                    new
                    {
                        Name = "P_Jap2",
                        CourseAllocations = new object[]
                           {
                               new
                               {
                                   Appointments = new object[]
                                   {
                                       new
                                       {
                                           StartTime = firstSemesterExamStartDate.AddDays(24).AddHours(10).AddMinutes(10).ToString("yyyy-MM-ddTHH:mm:ss"),
                                           EndTime = firstSemesterExamStartDate.AddDays(24).AddHours(12).AddMinutes(30).ToString("yyyy-MM-ddTHH:mm:ss"),
                                           AppointmentRooms = new object[]
                                           {
                                               new
                                               {
                                                   Nummer = "5.001"
                                               }
                                           },
                                           Lecturers = new object[]
                                           {
                                               new
                                               {
                                                   Fullname = "Hayao Miyazaki",
                                                   Shortname = "MIH"
                                               }
                                           }
                                       }
                                   }
                               }
                           }
                    },
                },
                Person = "Max Muster",
                Semester = firstSemesterName
            };

            var json = JsonConvert.SerializeObject(exams);
            return json;
        }
#endregion

#region TimetableData
        public static string TimetableData773 =>
            @"{""Courses"":[
                        {""Name"":""ChallP2-pj1"",""CourseAllocations"":[
                            {""DayOfWeek"":null,""TimeslotId"":1700,""Appointments"":[]}]}," +
            CreateEntry("1-14", "1", "1705", 14, firstSemesterStartDate.AddHours(10), "1.209", new[] { new Teacher("Mark Zuckerberg", "ZUM"), new Teacher("Evan Spiegel", "SPE"), new Teacher("Randi Zuckerberg", "ZUR") }) +
            CreateEntry("1-7", "1", "1706", 7, firstSemesterStartDate.AddHours(11).AddMinutes(5), "1.209", new[] { new Teacher("Mark Zuckerberg", "ZUM"), new Teacher("Evan Spiegel", "SPE"), new Teacher("Randi Zuckerberg", "ZUR") }) +
            CreateEntry("8-14", "4", "1708", 7, firstSemesterStartDate.AddMonths(1).AddDays(17).AddHours(13).AddMinutes(10), "C3.006", new[] { new Teacher("Mark Zuckerberg", "ZUM"), new Teacher("Evan Spiegel", "SPE"), new Teacher("Randi Zuckerberg", "ZUR") }) +
            CreateEntry("1-18", "1", "1709", 18, firstSemesterStartDate.AddHours(14).AddMinutes(5), "C3.006", new[] { new Teacher("Mark Zuckerberg", "ZUM"), new Teacher("Evan Spiegel", "SPE"), new Teacher("Randi Zuckerberg", "ZUR") }) +
            CreateEntry("8:10", "2", "1703", 15, firstSemesterStartDate.AddDays(1).AddHours(8).AddMinutes(10), "1.277", new[] { new Teacher("Sundar Pichai", "PIS"), new Teacher("Larry Page", "PAL") }) +
            CreateEntry("Cloud-v1", "2", "1704", 15, firstSemesterStartDate.AddDays(1).AddHours(9).AddMinutes(5), "1.277", new[] { new Teacher("Sundar Pichai", "PIS"), new Teacher("Larry Page", "PAL") }) +
            CreateEntry("Cloud-u11", "2", "1705", 15, firstSemesterStartDate.AddDays(1).AddHours(10).AddMinutes(10), "1.277", new[] { new Teacher("Sundar Pichai", "PIS"), new Teacher("Larry Page", "PAL") }) +
            CreateEntry("Cloud-u11", "2", "1706", 15, firstSemesterStartDate.AddDays(1).AddHours(11).AddMinutes(5), "1.277", new[] { new Teacher("Sundar Pichai", "PIS"), new Teacher("Larry Page", "PAL") }) +
            CreateEntry("ManagSim-u2", "2", "1708", 15, firstSemesterStartDate.AddDays(1).AddHours(13).AddMinutes(10), "1.259", new[] { new Teacher("Steve Wozniak", "WOS") }) +
            CreateEntry("ManagSim-u2", "2", "1709", 15, firstSemesterStartDate.AddDays(1).AddHours(14).AddMinutes(5), "1.259", new[] { new Teacher("Steve Wozniak", "WOS") }) +
            CreateEntry("PhAI-v1", "2", "1710", 15, firstSemesterStartDate.AddDays(1).AddHours(15).AddMinutes(10), "3.010", new[] { new Teacher("Albert Einstein", "EIA") }) +
            CreateEntry("PhAI-u11", "2", "1711", 15, firstSemesterStartDate.AddDays(1).AddHours(16).AddMinutes(5), "3.002", new[] { new Teacher("Albert Einstein", "EIA") }) +
            CreateEntry("Jap2-v1", "2", "1712", 15, firstSemesterStartDate.AddDays(1).AddHours(17), "1.267", new[] { new Teacher("Hayao Miyazaki", "MIH") }) +
            CreateEntry("Jap2-u11", "2", "1713", 15, firstSemesterStartDate.AddDays(1).AddHours(17).AddMinutes(75), "1.267", new[] { new Teacher("Hayao Miyazaki", "MIH") }) +
            CreateEntry("WED1-v1", "3", "1705", 15, firstSemesterStartDate.AddDays(2).AddHours(10).AddMinutes(10), "3.008", new[] { new Teacher("Mark Zuckerberg", "ZUM") }) +
            CreateEntry("WED1-v1", "3", "1706", 15, firstSemesterStartDate.AddDays(2).AddHours(11).AddMinutes(5), "3.008", new[] { new Teacher("Mark Zuckerberg", "ZUM") }) +
            CreateEntry("ReVertr-v2", "3", "1712", 15, firstSemesterStartDate.AddDays(2).AddHours(17), "3.114", new[] { new Teacher("Angela Merkel", "MEA"), new Teacher("Michelle Obama", "OBM") }) +
            CreateEntry("ReVertr-v2", "3", "1713", 15, firstSemesterStartDate.AddDays(2).AddHours(17).AddMinutes(55), "3.114", new[] { new Teacher("Angela Merkel", "MEA"), new Teacher("Michelle Obama", "OBM") }) +
            CreateEntry("PhAI-v1", "6", "1703", 15, firstSemesterStartDate.AddDays(5).AddHours(8).AddMinutes(10), "3.011", new[] { new Teacher("Albert Einstein", "EIA") }) +
            CreateEntry("PhAI-v1", "6", "1704", 15, firstSemesterStartDate.AddDays(5).AddHours(9).AddMinutes(5), "3.011", new[] { new Teacher("Albert Einstein", "EIA") }) +
            CreateEntry("SomeMore", "6", "1704", 15, firstSemesterStartDate.AddDays(5).AddHours(9).AddMinutes(5), "3.011", new[] { new Teacher("Albert Einstein", "EIA") }) +
            CreateEntry("AndMore...", "6", "1704", 15, firstSemesterStartDate.AddDays(5).AddHours(9).AddMinutes(5), "3.011", new[] { new Teacher("Albert Einstein", "EIA") }) +
            CreateEntry("WED1-u14", "6", "1704", 15, firstSemesterStartDate.AddDays(5).AddHours(9).AddMinutes(5), "1.263", new[] { new Teacher("Jan Koum", "KOJ") }) +
            CreateEntry("WED1-u14", "6", "1705", 15, firstSemesterStartDate.AddDays(5).AddHours(10).AddMinutes(10), "1.263", new[] { new Teacher("Jan Koum", "KOJ") }) +
            CreateEntry("WED1-u14", "0", "1705", 15, firstSemesterStartDate.AddDays(6).AddHours(10).AddMinutes(10), "1.263", new[] { new Teacher("Jan Koum", "KOJ") }) +
            @"],""Person"":""Max Muster"",""Semester"":""" + firstSemesterName + "\"}";

        public static string TimetableData774 => Stamp(@"{""Courses"":[" +
            CreateEntry("8:10", "2", "1703", 14, secondSemesterStartDate.AddDays(1).AddHours(8).AddMinutes(10), "1.277", new[] { new Teacher("Sundar Pichai", "PIS"), new Teacher("Larry Page", "PAL") }) +
            CreateEntry("<<STAMP>>", "1", "1704", 14, secondSemesterStartDate.AddHours(9).AddMinutes(5), "1.277", new[] { new Teacher("Sundar Pichai", "PIS"), new Teacher("Larry Page", "PAL") }) +
            CreateEntry("Cloud-u11", "2", "1705", 14, secondSemesterStartDate.AddDays(1).AddHours(10).AddMinutes(10), "1.277", new[] { new Teacher("Sundar Pichai", "PIS"), new Teacher("Larry Page", "PAL") }) +
            CreateEntry("Cloud-u11", "2", "1706", 14, secondSemesterStartDate.AddDays(1).AddHours(11).AddMinutes(5), "1.277", new[] { new Teacher("Sundar Pichai", "PIS"), new Teacher("Larry Page", "PAL") }) +
            CreateEntry("ManagSim-u2", "2", "1708", 14, secondSemesterStartDate.AddDays(1).AddHours(13).AddMinutes(10), "1.259", new[] { new Teacher("Steve Wozniak", "WOS") }) +
            CreateEntry("ManagSim-u2", "2", "1709", 14, secondSemesterStartDate.AddDays(1).AddHours(14).AddMinutes(5), "1.259", new[] { new Teacher("Steve Wozniak", "WOS") }) +
            CreateEntry("PhAI-v1", "2", "1710", 14, secondSemesterStartDate.AddDays(1).AddHours(15).AddMinutes(10), "3.010", new[] { new Teacher("Albert Einstein", "EIA") }) +
            CreateEntry("PhAI-u11", "2", "1711", 14, secondSemesterStartDate.AddDays(1).AddHours(16).AddMinutes(5), "3.002", new[] { new Teacher("Albert Einstein", "EIA") }) +
            CreateEntry("Jap2-Mo", "1", "1712", 14, secondSemesterStartDate.AddHours(17), "1.267", new[] { new Teacher("Hayao Miyazaki", "MIH") }) +
            CreateEntry("Jap2-Mo", "1", "1713", 14, secondSemesterStartDate.AddHours(17).AddMinutes(55), "1.267", new[] { new Teacher("Hayao Miyazaki", "MIH") }) +
            CreateEntry("Jap2-Di", "2", "1712", 14, secondSemesterStartDate.AddDays(1).AddHours(17), "1.267", new[] { new Teacher("Hayao Miyazaki", "MIH") }) +
            CreateEntry("Jap2-Di", "2", "1713", 14, secondSemesterStartDate.AddDays(1).AddHours(17).AddMinutes(55), "1.267", new[] { new Teacher("Hayao Miyazaki", "MIH") }) +
            CreateEntry("Jap2-Mi", "3", "1712", 14, secondSemesterStartDate.AddDays(2).AddHours(17), "1.267", new[] { new Teacher("Hayao Miyazaki", "MIH") }) +
            CreateEntry("Jap2-Mi", "3", "1713", 14, secondSemesterStartDate.AddDays(2).AddHours(17).AddMinutes(55), "1.267", new[] { new Teacher("Hayao Miyazaki", "MIH") }) +
            CreateEntry("Jap2-Do", "4", "1712", 14, secondSemesterStartDate.AddDays(3).AddHours(17), "1.267", new[] { new Teacher("Hayao Miyazaki", "MIH") }) +
            CreateEntry("Jap2-Do", "4", "1713", 14, secondSemesterStartDate.AddDays(3).AddHours(17).AddMinutes(55), "1.267", new[] { new Teacher("Hayao Miyazaki", "MIH") }) +
            CreateEntry("Jap2-Fr", "5", "1712", 14, secondSemesterStartDate.AddDays(4).AddHours(17), "1.267", new[] { new Teacher("Hayao Miyazaki", "MIH") }) +
            CreateEntry("Jap2-Fr", "5", "1713", 14, secondSemesterStartDate.AddDays(4).AddHours(17).AddMinutes(55), "1.267", new[] { new Teacher("Hayao Miyazaki", "MIH") }) +
            $"],\"Person\":\"Max Muster\",\"Semester\":\"" + secondSemesterName + "\"}");

        private static string CreateEntry(string name, string dayOfWeek, string timeslotId, int nOfWeeks, DateTime startTime, string roomNr, IEnumerable<Teacher> teachers)
        {
            return @"
                    {""Name"":""" + name + @""",""CourseAllocations"":[
                        {""DayOfWeek"":" + dayOfWeek +
                            @",""TimeslotId"":" + timeslotId +
                            @",""Appointments"":[" + CreateAppointments(startTime, nOfWeeks, roomNr, teachers) +
                            @"]}]},";
        }

        private static string CreateAppointments(DateTime startTime, int nOfWeeks, string roomNr, IEnumerable<Teacher> teachers)
        {
            var appointments = string.Empty;
            for (var i = 0; i < nOfWeeks; i++)
            {
                appointments += @"
                                {""StartTime"":""" + startTime.ToString("yyyy-MM-ddTHH:mm:ss") +
                                    @""",""EndTime"":""" + startTime.AddMinutes(45).ToString("yyyy-MM-ddTHH:mm:ss") +
                                    @""",""AppointmentRooms"":[{""Nummer"":""" + roomNr +
                                    @"""}],""Lecturers"":[
                                    " + CreateLectures(teachers) +
                                    @"]},";
                startTime = startTime.AddDays(7);
            }

            appointments = appointments.TrimEnd(',');

            return appointments;
        }

        private static string CreateLectures(IEnumerable<Teacher> teachers)
        {
            var lecture = teachers.Aggregate(string.Empty, (current, teacher) => current +
                @"{""Fullname"":""" + teacher.Name +
                @""",""Shortname"":""" + teacher.Short +
                @"""},");

            lecture = lecture.TrimEnd(',');

            return lecture;
        }

#endregion

        private static string Stamp(string input)
        {
            return input.Replace("<<STAMP>>", DateTime.UtcNow.ToString("hh:mm:s.fff"));
        }

        private static DateTime GetMondayDateOfWeek(int week, int year)
        {
            int i = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(new DateTime(year, 1, 1), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            if (i == 1)
            {
                return CultureInfo.CurrentCulture.Calendar.AddDays(new DateTime(year, 1, 1), ((week - 1) * 7) - GetDayCountFromMonday(CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(new DateTime(year, 1, 1))) + 1);
            }
            else
            {
                int x = Convert.ToInt32(CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(new DateTime(year, 1, 1)));
                return CultureInfo.CurrentCulture.Calendar.AddDays(new DateTime(year, 1, 1), ((week - 1) * 7) + (7 - GetDayCountFromMonday(CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(new DateTime(year, 1, 1)))) + 1);
            }
        }

        private static int GetDayCountFromMonday(DayOfWeek dow)
        {
            switch (dow)
            {
                case DayOfWeek.Monday:
                    return 1;
                case DayOfWeek.Tuesday:
                    return 2;
                case DayOfWeek.Wednesday:
                    return 3;
                case DayOfWeek.Thursday:
                    return 4;
                case DayOfWeek.Friday:
                    return 5;
                case DayOfWeek.Saturday:
                    return 6;
                default:
                    return 7; // Sunday
            }
        }

        private static void InitializeData()
        {
            if (year != DateTime.Now.Year)
            {
                year = DateTime.Now.Year;
                isSpringSemester = DateTime.Now > GetMondayDateOfWeek(8, year) && DateTime.Now < GetMondayDateOfWeek(22, year);

                firstSemesterName = isSpringSemester ? "FS " + year : "HS " + year;
                firstSemesterStartDate = isSpringSemester ? GetMondayDateOfWeek(8, year) : GetMondayDateOfWeek(38, year);
                firstSemesterStart = firstSemesterStartDate.ToString("yyyy-MM-ddTHH:mm:ss");
                firstSemesterEndDate = isSpringSemester ? GetMondayDateOfWeek(22, year).AddDays(4) : GetMondayDateOfWeek(47, year).AddDays(4);
                firstSemesterEnd = firstSemesterEndDate.ToString("yyyy-MM-ddTHH:mm:ss");
                firstSemesterExamStartDate = isSpringSemester ? GetMondayDateOfWeek(33, year) : GetMondayDateOfWeek(3, year);
                firstSemesterExamStart = firstSemesterExamStartDate.ToString("yyyy-MM-ddTHH:mm:ss");
                firstSemesterExamEndDate = isSpringSemester ? GetMondayDateOfWeek(37, year).AddDays(4) : GetMondayDateOfWeek(7, year).AddDays(4);
                firstSemesterExamEnd = firstSemesterExamEndDate.ToString("yyyy-MM-ddTHH:mm:ss");

                secondSemesterName = isSpringSemester ? "HS " + year : "FS " + (year + 1);
                secondSemesterStartDate = isSpringSemester ? GetMondayDateOfWeek(38, year) : GetMondayDateOfWeek(8, year + 1);
                secondSemesterStart = secondSemesterStartDate.ToString("yyyy-MM-ddTHH:mm:ss");
                secondSemesterEndDate = isSpringSemester ? GetMondayDateOfWeek(47, year).AddDays(4) : GetMondayDateOfWeek(22, year + 1).AddDays(4);
                secondSemesterEnd = secondSemesterEndDate.ToString("yyyy-MM-ddTHH:mm:ss");
                secondSemesterExamStartDate = isSpringSemester ? GetMondayDateOfWeek(3, year) : GetMondayDateOfWeek(33, year + 1);
                secondSemesterExamStart = secondSemesterExamStartDate.ToString("yyyy-MM-ddTHH:mm:ss");
                secondSemesterExamEndDate = isSpringSemester ? GetMondayDateOfWeek(7, year).AddDays(4) : GetMondayDateOfWeek(37, year + 1).AddDays(4);
                secondSemesterExamEnd = secondSemesterExamEndDate.ToString("yyyy-MM-ddTHH:mm:ss");
            }
        }

        private class Teacher
        {
            public Teacher(string name, string shortname)
            {
                this.Name = name;
                this.Short = shortname;
            }

            public string Name { get; }

            public string Short { get; }
        }
    }
}
#endif
#pragma warning restore SA1124 // Do not use regions
#pragma warning restore SA1201 // Elements must appear in the correct order

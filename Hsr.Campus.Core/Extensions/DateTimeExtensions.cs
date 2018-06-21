// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace System
{
    using Globalization;

    public static class DateTimeExtensions
    {
        public static DateTime SyncWeekday(this DateTime referenceDate, DayOfWeek weekDay)
        {
            var result = referenceDate;
            while (result.DayOfWeek != weekDay)
            {
                result = result.AddDays(1);
            }

            return result;
        }

        public static int ToUnixTimestamp(this DateTime dtStart) => (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

        public static int WeekNr(this DateTime dtStart)
        {
            var cal = DateTimeFormatInfo.GetInstance(CultureInfo.CurrentUICulture).Calendar;
            var calWeek = cal.GetWeekOfYear(dtStart, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            if (calWeek > 51 && dtStart.Day > 28)
            {
                var dateDayOfWeek = dtStart.DayOfWeek;
                var jan1DayOfWeek = new DateTime(dtStart.Year + 1, 1, 1).DayOfWeek;
                if ((dateDayOfWeek == DayOfWeek.Monday
                        || dateDayOfWeek == DayOfWeek.Tuesday
                        || dateDayOfWeek == DayOfWeek.Wednesday)
                    && (jan1DayOfWeek == DayOfWeek.Tuesday
                        || jan1DayOfWeek == DayOfWeek.Wednesday
                        || jan1DayOfWeek == DayOfWeek.Thursday))
                {
                    calWeek = 1;
                }
            }
            else if (calWeek == 1 && dtStart.Day <= 3)
            {
                var dateDayOfWeek = dtStart.DayOfWeek;
                var jan1DayOfWeek = new DateTime(dtStart.Year, 1, 1).DayOfWeek;
                if ((dateDayOfWeek == DayOfWeek.Friday
                        || dateDayOfWeek == DayOfWeek.Saturday
                        || dateDayOfWeek == DayOfWeek.Sunday)
                    && (jan1DayOfWeek == DayOfWeek.Friday
                        || jan1DayOfWeek == DayOfWeek.Saturday
                        || jan1DayOfWeek == DayOfWeek.Sunday))
                {
                    calWeek = cal.GetWeekOfYear(new DateTime(dtStart.Year - 1, 12, 31), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                }
            }

            return calWeek;
        }
    }
}

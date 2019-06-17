using System;
using System.Globalization;

namespace WorkoutTracker.Data.Common
{
    public static partial class DateTimeExtensions
    {
        private static GregorianCalendar _gc = new GregorianCalendar();

        public static int GetWeekOfMonthAlt(this DateTime date)
        {
            DateTime beginningOfMonth = new DateTime(date.Year, date.Month, 1);

            while (date.Date.AddDays(1).DayOfWeek != CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                date = date.AddDays(1);

            return (int)Math.Truncate((double)date.Subtract(beginningOfMonth).TotalDays / 7f) + 1;
        }

        public static int GetWeekOfMonth(this DateTime date)
        {
            DateTime beginningOfMonth = new DateTime(date.Year, date.Month, 1);

            while (beginningOfMonth.DayOfWeek != CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
            {
                beginningOfMonth = beginningOfMonth.AddDays(1);
            }

            var daySpan = Convert.ToInt32((date.Date - beginningOfMonth.Date).TotalDays);
            return (daySpan / 7) + 1;
        }

        public static int GetWeekOfMonthPartial(this DateTime date)
        {
            var beginningOfMonth = new DateTime(date.Year, date.Month, 1);
            var firstWeekDayOffset = new DateTime(date.Year, date.Month, 1);

            while (firstWeekDayOffset.DayOfWeek != CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
            {
                firstWeekDayOffset = firstWeekDayOffset.AddDays(1);
            }

            if (firstWeekDayOffset.Date > date.Date)
            {
                firstWeekDayOffset = firstWeekDayOffset.AddDays(-7);
            }
            else if (firstWeekDayOffset.Date > beginningOfMonth.Date)
            {
                int bomDay = ((int)beginningOfMonth.DayOfWeek == 0) ? 7 : (int)beginningOfMonth.DayOfWeek;
                int thisDay = ((int)date.DayOfWeek == 0) ? 7 : (int)date.DayOfWeek;

                if (bomDay <= thisDay)
                {
                    firstWeekDayOffset = firstWeekDayOffset.AddDays(-7);
                }
            }

            var daySpan = Convert.ToInt32((date.Date - firstWeekDayOffset.Date).TotalDays);

            return (daySpan / 7) + 1;
        }

        public static DateTime FirstDayOfWeek(this DateTime dt)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;
            return dt.AddDays(-diff).Date;
        }

        public static DateTime LastDayOfWeek(this DateTime dt)
        {
            return dt.FirstDayOfWeek().AddDays(6);
        }

        public static DateTime FirstDayOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        public static DateTime FirstDayOfMonthHavingFirstDayofWeek(this DateTime date)
        {
            DateTime beginningOfMonth = new DateTime(date.Year, date.Month, 1);

            while (beginningOfMonth.DayOfWeek != CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
            {
                beginningOfMonth = beginningOfMonth.AddDays(1);
            }

            return beginningOfMonth;
        }

        public static DateTime LastDayOfMonth(this DateTime dt)
        {
            return dt.FirstDayOfMonth().AddMonths(1).AddDays(-1);
        }

        public static DateTime FirstDayOfNextMonth(this DateTime dt)
        {
            return dt.FirstDayOfMonth().AddMonths(1);
        }

        public static bool IsDateInBetween(this DateTime dt, DateTime start, DateTime end)
        {
            return (dt.Date >= start.Date && dt.Date <= end.Date);
        }

        public static string ToMonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
        }

        public static string ToShortMonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(dateTime.Month);
        }
    }
}

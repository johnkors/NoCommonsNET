using System;
using System.Collections.Generic;

namespace NoCommons.Date
{
    public class NorwegianDateUtil
    {
        private static Dictionary<int, List<DateTime>> holidays;

        /**
         * Adds the given number of working days to the given date. A working day is
         * specified as a regular Norwegian working day, excluding weekends and all
         * national holidays.
         * <p/>
         * Example 1:<br/>
         * - Add 5 working days to Wednesday 21.03.2007 -> Yields Wednesday
         * 28.03.2007. (skipping saturday and sunday)<br/>
         * <p/>
         * Example 2:<br/>
         * - Add 5 working days to Wednesday 04.04.2007 (day before
         * easter-long-weekend) -> yields Monday 16.04.2007 (skipping 2 weekends and
         * 3 weekday holidays).
         *
         * @param date
         *            The original date.
         * @param days
         *            The number of working days to add.
         * @return The new date.
         */

        public static DateTime addWorkingDaysToDate(DateTime date, int days)
        {
            var cal = date;
            var workingDays = 0;
            for (var i = 0; i < days; i++)
            {
                var newDate = date.AddDays(1);
                while (!isWorkingDay(newDate))
                {
                    workingDays++;
                }
            }

            return cal.AddDays(workingDays);
        }

        /**
         * Will check if the given date is a working day. That is check if the given
         * date is a weekend day or a national holiday.
         *
         * @param date
         *            The date to check.
         * @return true if the given date is a working day, false otherwise.
         */

        public static bool isWorkingDay(DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday
                   && !isHoliday(date);
        }

        /**
         * Check if given Date object is a holiday.
         *
         * @param date
         *            The Date to check.
         * @return true if holiday, false otherwise.
         */

        public static bool isHoliday(DateTime inDate)
        {
            var year = inDate.Year;
            var yearSet = getHolidaySet(year);
            foreach (DateTime aYearSet in yearSet)
            {
                var date = aYearSet;
                if (checkDate(inDate, date))
                {
                    return true;
                }
            }
            return false;
        }

        /**
         * Return a sorted array of holidays for a given year.
         *
         * @param year
         *            The year to get holidays for.
         * @return The array of holidays, sorted by date.
         */

        public static List<DateTime> getHolidays(int year)
        {
            var days = getHolidaySet(year);
            days.Sort();
            return days;
        }

        /**
         * Get a set of holidays for a given year.
         *
         * @param year
         *            The year to get holidays for.
         * @return The set of dates.
         */

        private static List<DateTime> getHolidaySet(int year)
        {
            if (holidays == null)
            {
                holidays = new Dictionary<int, List<DateTime>>();
            }
            if (!holidays.ContainsKey(year))
            {
                var yearSet = new List<DateTime>();

                // Add set holidays.
                yearSet.Add(getDate(1, 1, year));
                yearSet.Add(getDate(1, 5, year));
                yearSet.Add(getDate(17, 5, year));
                yearSet.Add(getDate(25, 12, year));
                yearSet.Add(getDate(26, 12, year));

                // Add movable holidays - based on easter day.
                var easterDay = getEasterDay(year);

                // Sunday before easter.
                yearSet.Add(rollGetDate(easterDay, -7));

                // Thurday before easter.
                yearSet.Add(rollGetDate(easterDay, -3));

                // Friday before easter.
                yearSet.Add(rollGetDate(easterDay, -2));

                // Easter day.
                yearSet.Add(easterDay);

                // Second easter day.
                yearSet.Add(rollGetDate(easterDay, 1));

                // "Kristi himmelfart" day.
                yearSet.Add(rollGetDate(easterDay, 39));

                // "Pinse" day.
                yearSet.Add(rollGetDate(easterDay, 49));

                // Second "Pinse" day.
                yearSet.Add(rollGetDate(easterDay, 50));

                holidays.Add(year, yearSet);
            }
            return holidays[year];
        }

     

 

        /**
         * Calculates easter day (sunday) by using Spencer Jones formula found here:
         * <a href="http://no.wikipedia.org/wiki/P%C3%A5skeformelen">Wikipedia -
         * Påskeformelen</a>
         *
         * @param year
         *            The year to calculate from.
         * @return The Calendar object representing easter day for the given year.
         */

        private static DateTime getEasterDay(int year)
        {
            int a = year%19;
            int b = year/100;
            int c = year%100;
            int d = b/4;
            int e = b%4;
            int f = (b + 8)/25;
            int g = (b - f + 1)/3;
            int h = ((19*a) + b - d - g + 15)%30;
            int i = c/4;
            int k = c%4;
            int l = (32 + (2*e) + (2*i) - h - k)%7;
            int m = (a + (11*h) + (22*l))/451;
            int n = (h + l - (7*m) + 114)/31; // This is the month number.
            int p = (h + l - (7*m) + 114)%31; // This is the date minus one.

            return new DateTime(year, n - 1, p + 1);
        }

        /**
         * Check if the given dates match on day and month.
         *
         * @param cal
         *            The Calendar representing the first date.
         * @param other
         *            The Calendar representing the second date.
         * @return true if they match, false otherwise.
         */

        private static bool checkDate(DateTime cal, DateTime other)
        {
            return checkDate(cal, other.Day, other.Month);
        }

        /**
         * Check if the given date represents the given date and month.
         *
         * @param cal
         *            The Calendar object representing date to check.
         * @param date
         *            The date.
         * @param month
         *            The month.
         * @return true if they match, false otherwise.
         */

        private static bool checkDate(DateTime cal, int date, int month)
        {
            return cal.Day == date && cal.Month == month;
        }


        /**
         * Add the given number of days to the calendar and convert to Date.
         *
         * @param calendar
         *            The calendar to add to.
         * @param days
         *            The number of days to add.
         * @return The date object given by the modified calendar.
         */

        private static DateTime rollGetDate(DateTime calendar, int days)
        {
            var added = calendar.AddDays(days);
            return added;
        }

        /**
         * Get the date for the given values.
         *
         * @param day
         *            The day.
         * @param month
         *            The month.
         * @param year
         *            The year.
         * @return The date represented by the given values.
         */

        private static DateTime getDate(int day, int month, int year)
        {
            //Calendar cal = Calendar.getInstance(TimeZone. GetTimeZone("Europe/Berlin"), new Locale("no", "NO"));

            return new DateTime(year, month, day);
        }
    }
}
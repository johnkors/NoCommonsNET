using System;
using System.Collections.Generic;

namespace NoCommons.Date
{
    public class NorwegianDateUtil
    {
        private static readonly object Lock = new object();
        private static Dictionary<int, HashSet<DateTime>> holidays;
        
        /// <summary>
        /// Adds the given number of working days to the given date. A working day is
        /// specified as a regular Norwegian working day, excluding weekends and all
        /// national holidays.
        /// 
        /// Example 1:
        /// - Add 5 working days to Wednesday 21.03.2007 -> Yields Wednesday
        /// 28.03.2007. (skipping saturday and sunday)
        /// 
        /// Example 2:
        /// - Add 5 working days to Wednesday 04.04.2007 (day before
        /// easter-long-weekend) -> yields Monday 16.04.2007 (skipping 2 weekends and
        /// 3 weekday holidays).
        /// </summary>
        /// <param name="date">The original date</param>
        /// <param name="days">The number of working days to add</param>
        /// <returns>The new date</returns>
        public static DateTime addWorkingDaysToDate(DateTime date, int days)
        {

            var localDate = date;
            for (var i = 0; i < days; i++)
            {
                localDate = localDate.AddDays(1);
                while (!isWorkingDay(localDate))
                {
                    localDate = localDate.AddDays(1);
                }
            }

            return localDate;
        }

        /// <summary>
        /// Will check if the given date is a working day. That is check if the given
        /// date is a weekend day or a national holiday.
        /// </summary>
        /// <param name="date">The date to check</param>
        /// <returns>true if the given date is a working day, false otherwise</returns>
        public static bool isWorkingDay(DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday
                   && !isHoliday(date);
        }

        /// <summary>
        /// Check if given Date object is a holiday.
        /// </summary>
        /// <param name="date">date to check if is a holiday</param>
        /// <returns>true if holiday, false otherwise</returns>
        public static bool isHoliday(DateTime date)
        {
            var year = date.Year;
            var holidaysForYear = getHolidaySet(year);
            foreach (var holiday in holidaysForYear)
            {  
                if (checkDate(date, holiday))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Return a sorted array of holidays for a given year.
        /// </summary>
        /// <param name="year">The year to get holidays for</param>
        /// <returns>Holidays, sorted by date</returns>
        public static IEnumerable<DateTime> getHolidays(int year)
        {
            var days = getHolidaySet(year);
            var listOfHolidays = new List<DateTime>(days);
            listOfHolidays.Sort((date1, date2) => date1.CompareTo(date2));
            return listOfHolidays;
        }
    
        /// <summary>
        /// Get a set of holidays for a given year
        /// </summary>
        /// <param name="year">The year to get holidays for</param>
        /// <returns>Holidays for year</returns>
        private static IEnumerable<DateTime> getHolidaySet(int year)
        {
            lock (Lock)
            {
                if (holidays == null)
                {
                    holidays = new Dictionary<int, HashSet<DateTime>>();
                }
                if (!holidays.ContainsKey(year))
                {
                    var yearSet = new HashSet<DateTime>();

                    // Add set holidays.
                    yearSet.Add(new DateTime(year, 1, 1));
                    yearSet.Add(new DateTime(year, 5, 1));
                    yearSet.Add(new DateTime(year, 5, 17));
                    yearSet.Add(new DateTime(year, 12, 25));
                    yearSet.Add(new DateTime(year, 12, 26));

                    // Add movable holidays - based on easter day.
                    var easterDay = getEasterDay(year);

                    // Sunday before easter.
                    yearSet.Add(easterDay.AddDays(-7));

                    // Thurday before easter.
                    yearSet.Add(easterDay.AddDays(-3));

                    // Friday before easter.
                    yearSet.Add(easterDay.AddDays(-2));

                    // Easter day.
                    yearSet.Add(easterDay);

                    // Second easter day.
                    yearSet.Add(easterDay.AddDays(1));

                    // "Kristi himmelfart" day.
                    yearSet.Add(easterDay.AddDays(39));

                    // "Pinse" day.
                    yearSet.Add(easterDay.AddDays(49));

                    // Second "Pinse" day.
                    yearSet.Add(easterDay.AddDays(50));

                    holidays.Add(year, yearSet);
                }
            }
            return holidays[year];
        }

       
     
      
       /// <summary>
       ///  Calculates easter day (sunday) by using Spencer Jones formula found here:
       ///  http://no.wikipedia.org/wiki/P%C3%A5skeformelen
       /// </summary>
       /// <param name="year">year</param>
       /// <returns>easterday for year</returns>
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

            return new DateTime(year, n, p + 1);
        }

        private static bool checkDate(DateTime date, DateTime other)
        {
            return date.Day == other.Day && date.Month == other.Month;
        }
    }
}
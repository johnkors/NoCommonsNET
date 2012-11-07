using System;
using System.Globalization;
using NUnit.Framework;
using NoCommons.Date;

namespace NoCommons.Tests.Date
{
    public class NorwegianDateUtilTest
    {
        [Test]
        public void testAdd2DaysWithinSameWeek()
        {
            var date = new DateTime(21, 3, 2007);
            NorwegianDateUtil.addWorkingDaysToDate(date, 2);

            Assert.AreEqual(23, date.Day);
        }

        [Test]
        public void testAdd2DaysToLastDayOfMonth()
        {
            var date = new DateTime(2007, 28, 2);
            NorwegianDateUtil.addWorkingDaysToDate(date, 2);

            Assert.AreEqual(2, date.Day);
            Assert.AreEqual(3, date.Month);
        }

        [Test]
        public void testAdd5DaysWithNoHolidays()
        {
            var date = new DateTime(2007, 03, 21);
            NorwegianDateUtil.addWorkingDaysToDate(date, 5);

            Assert.AreEqual(28, date.Day);
        }

        [Test]
        public void testAdd5DaysBeforeEasterHoliday()
        {
            var date = new DateTime(2007, 4, 4);
            NorwegianDateUtil.addWorkingDaysToDate(date, 5);

            Assert.AreEqual(16, date.Day);
        }

        [Test]
        public void testAdd5DaysBeforeNationalDay()
        {
            var date = new DateTime(2007, 5, 16);
            NorwegianDateUtil.addWorkingDaysToDate(date, 5);

            Assert.AreEqual(24, date.Day);
        }

        [Test]
        public void testAdd5DaysBeforeChristmas()
        {
            var date = new DateTime(2007, 12, 21);
            NorwegianDateUtil.addWorkingDaysToDate(date, 5);

            Assert.AreEqual(2, date.Day);
            Assert.AreEqual(1, date.Month);
            Assert.AreEqual(2008, date.Year);
        }

        [Test]
        public void testWorkingDays()
        {
            Assert.IsFalse(NorwegianDateUtil.isWorkingDay(new DateTime(2007,3,25)), "Sunday not working day");
            Assert.IsTrue(NorwegianDateUtil.isWorkingDay(new DateTime(2007, 3, 26)), "Monday is working day");
            Assert.IsFalse(NorwegianDateUtil.isWorkingDay(new DateTime(2007,1,1)), "New years day not working day");
            Assert.IsFalse(NorwegianDateUtil.isWorkingDay(new DateTime(2007,4,8)), "Easter day not working day");
        }

        [Test]
        public void testVariousNorwegianHolidays()
        {
            // Set dates
            checkHoliday("01.01.2007");
            checkHoliday("01.05.2007");
            checkHoliday("17.05.2007");
            checkHoliday("25.12.2007");
            checkHoliday("26.12.2007");

            // Movable dates 2007
            checkHoliday("01.04.2007");
            checkHoliday("05.04.2007");
            checkHoliday("06.04.2007");
            checkHoliday("08.04.2007");
            checkHoliday("09.04.2007");
            checkHoliday("17.05.2007");
            checkHoliday("27.05.2007");
            checkHoliday("28.05.2007");

            // Movable dates 2008
            checkHoliday("16.03.2008");
            checkHoliday("20.03.2008");
            checkHoliday("21.03.2008");
            checkHoliday("23.03.2008");
            checkHoliday("24.03.2008");
            checkHoliday("01.05.2008");
            checkHoliday("11.05.2008");
            checkHoliday("12.05.2008");
        }

        [Test]
        public void testGetAllNorwegianHolidaysForYear()
        {
            const string format = "dd.MM.yyyy";
            var holidays = NorwegianDateUtil.getHolidays(2008);
            Assert.AreEqual(12, holidays.Count);
            Assert.AreEqual("01.01.2008", holidays[0].ToString(format));
            Assert.AreEqual("16.03.2008", holidays[1].ToString(format));
            Assert.AreEqual("20.03.2008", holidays[2].ToString(format));
            Assert.AreEqual("21.03.2008", holidays[3].ToString(format));
            Assert.AreEqual("23.03.2008", holidays[4].ToString(format));
            Assert.AreEqual("24.03.2008", holidays[5].ToString(format));
            Assert.AreEqual("01.05.2008", holidays[6].ToString(format));
            Assert.AreEqual("11.05.2008", holidays[7].ToString(format));
            Assert.AreEqual("12.05.2008", holidays[8].ToString(format));
            Assert.AreEqual("17.05.2008", holidays[9].ToString(format));
            Assert.AreEqual("25.12.2008", holidays[10].ToString(format));
            Assert.AreEqual("26.12.2008", holidays[11].ToString(format));
        }

        private void checkHoliday(String date)
        {
            var dateTime = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            Assert.AreEqual( dateTime  , NorwegianDateUtil.isHoliday(dateTime));
        }
    }
}
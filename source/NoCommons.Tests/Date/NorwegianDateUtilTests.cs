using System.Globalization;
using NoCommons.Date;

namespace NoCommons.Tests.Date;

public class NorwegianDateUtilTest
{
    [Fact]
    public void testAdd2DaysWithinSameWeek()
    {
        DateTime date = new(2007, 3, 21);
        DateTime newDate = NorwegianDateUtil.AddWorkingDaysToDate(date, 2);

        Assert.Equal(23, newDate.Day);
    }

    [Fact]
    public void testAdd2DaysToLastDayOfMonth()
    {
        DateTime date = new(2007, 2, 28);
        DateTime newDate = NorwegianDateUtil.AddWorkingDaysToDate(date, 2);

        Assert.Equal(2, newDate.Day);
        Assert.Equal(3, newDate.Month);
    }

    [Fact]
    public void testAdd5DaysWithNoHolidays()
    {
        DateTime date = new(2007, 03, 21);
        DateTime newDate = NorwegianDateUtil.AddWorkingDaysToDate(date, 5);

        Assert.Equal(28, newDate.Day);
    }

    [Fact]
    public void testAdd5DaysBeforeEasterHoliday()
    {
        DateTime date = new(2007, 4, 4);
        DateTime newDate = NorwegianDateUtil.AddWorkingDaysToDate(date, 5);

        Assert.Equal(16, newDate.Day);
    }

    [Fact]
    public void testAdd5DaysBeforeNationalDay()
    {
        DateTime date = new(2007, 5, 16);
        DateTime newDate = NorwegianDateUtil.AddWorkingDaysToDate(date, 5);

        Assert.Equal(24, newDate.Day);
    }

    [Fact]
    public void testAdd5DaysBeforeChristmas()
    {
        DateTime date = new(2007, 12, 21);
        DateTime newDate = NorwegianDateUtil.AddWorkingDaysToDate(date, 5);

        Assert.Equal(2, newDate.Day);
        Assert.Equal(1, newDate.Month);
        Assert.Equal(2008, newDate.Year);
    }

    [Fact]
    public void testWorkingDays()
    {
        Assert.False(NorwegianDateUtil.IsWorkingDay(new DateTime(2007, 3, 25)), "Sunday not working day");
        Assert.True(NorwegianDateUtil.IsWorkingDay(new DateTime(2007, 3, 26)), "Monday is working day");
        Assert.False(NorwegianDateUtil.IsWorkingDay(new DateTime(2007, 1, 1)), "New years day not working day");
        Assert.False(NorwegianDateUtil.IsWorkingDay(new DateTime(2007, 4, 8)), "Easter day not working day");
    }

    [Fact]
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

    [Fact]
    public void testGetAllNorwegianHolidaysForYear()
    {
        const string format = "dd.MM.yyyy";
        IEnumerable<DateTime>? holidays = NorwegianDateUtil.GetHolidays(2008);
        Assert.Equal(12, holidays.Count());
        Assert.Equal("01.01.2008", holidays.ElementAt(0).ToString(format));
        Assert.Equal("16.03.2008", holidays.ElementAt(1).ToString(format));
        Assert.Equal("20.03.2008", holidays.ElementAt(2).ToString(format));
        Assert.Equal("21.03.2008", holidays.ElementAt(3).ToString(format));
        Assert.Equal("23.03.2008", holidays.ElementAt(4).ToString(format));
        Assert.Equal("24.03.2008", holidays.ElementAt(5).ToString(format));
        Assert.Equal("01.05.2008", holidays.ElementAt(6).ToString(format));
        Assert.Equal("11.05.2008", holidays.ElementAt(7).ToString(format));
        Assert.Equal("12.05.2008", holidays.ElementAt(8).ToString(format));
        Assert.Equal("17.05.2008", holidays.ElementAt(9).ToString(format));
        Assert.Equal("25.12.2008", holidays.ElementAt(10).ToString(format));
        Assert.Equal("26.12.2008", holidays.ElementAt(11).ToString(format));
    }

    private void checkHoliday(string date)
    {
        DateTime dateTime = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
        Assert.True(NorwegianDateUtil.IsHoliday(dateTime));
    }
}

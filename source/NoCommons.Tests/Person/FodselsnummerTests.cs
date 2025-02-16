using NoCommons.Person;

namespace NoCommons.Tests.Person;

public class FodselsnummerTest
{
    private const string VALID_FODSELSNUMMER = "01010123476";

    private const string VALID_D_FODSELSNUMMER = "41010123476";

    private Fodselsnummer sut;

    public FodselsnummerTest()
    {
        sut = new Fodselsnummer(VALID_FODSELSNUMMER);
    }

    [Fact]
    public void testGetDateAndMonth()
    {
        Assert.Equal("0101", sut.GetDateAndMonth());
    }

    [Fact]
    public void testGetDayInMonth()
    {
        Assert.Equal("01", sut.GetDayInMonth());
        sut = new Fodselsnummer(VALID_D_FODSELSNUMMER);
        Assert.Equal("01", sut.GetDayInMonth());
    }

    [Fact]
    public void testGetMonth()
    {
        Assert.Equal("01", sut.GetMonth());
    }

    [Fact]
    public void testGetDateAndMonthDNumber()
    {
        sut = new Fodselsnummer(VALID_D_FODSELSNUMMER);
        Assert.Equal("0101", sut.GetDateAndMonth());
    }

    [Fact]
    public void testGetCentury()
    {
        sut = new Fodselsnummer("01016666609");
        Assert.Equal("18", sut.GetCentury());

        sut = new Fodselsnummer("01016633301");
        Assert.Equal("19", sut.GetCentury());

        sut = new Fodselsnummer("01019196697");
        Assert.Equal("19", sut.GetCentury());

        sut = new Fodselsnummer("01013366671");
        Assert.Equal("20", sut.GetCentury());

        // DNumber...
        sut = new Fodselsnummer("41016666609");
        Assert.Equal("18", sut.GetCentury());

        sut = new Fodselsnummer("01015466609");
        Assert.Equal("18", sut.GetCentury());

        sut = new Fodselsnummer("41016633301");
        Assert.Equal("19", sut.GetCentury());

        sut = new Fodselsnummer("41019196697");
        Assert.Equal("19", sut.GetCentury());

        sut = new Fodselsnummer("41013366671");
        Assert.Equal("20", sut.GetCentury());
    }

    [Fact]
    public void testGet2DigitBirthYear()
    {
        Assert.Equal("01", sut.Get2DigitBirthYear());
    }

    [Fact]
    public void testGetBirthYear()
    {
        Assert.Equal("1901", sut.GetBirthYear());
        sut = new Fodselsnummer(VALID_D_FODSELSNUMMER);
        Assert.Equal("1901", sut.GetBirthYear());
    }

    [Fact]
    public void testGetDateOfBirth()
    {
        Assert.Equal("010101", sut.GetDateOfBirth());
    }

    [Fact]
    public void testGetDateOfBirthDNumber()
    {
        sut = new Fodselsnummer(VALID_D_FODSELSNUMMER);
        Assert.Equal("010101", sut.GetDateOfBirth());
    }

    [Fact]
    public void testGetPersonnummer()
    {
        Assert.Equal("23476", sut.GetPersonnummer());
    }

    [Fact]
    public void testGetIndividnummer()
    {
        Assert.Equal("234", sut.GetIndividnummer());
    }

    [Fact]
    public void testGetGenderDigit()
    {
        Assert.Equal(4, sut.GetGenderDigit());
    }

    [Fact]
    public void testGetChecksumDigits()
    {
        Assert.Equal(7, sut.GetChecksumDigit1());
        Assert.Equal(6, sut.GetChecksumDigit2());
    }

    [Fact]
    public void testIsMale()
    {
        Assert.False(sut.IsMale());
    }

    [Fact]
    public void testIsMaleDNumber()
    {
        sut = new Fodselsnummer(VALID_D_FODSELSNUMMER);
        Assert.False(sut.IsMale());
    }

    [Fact]
    public void testIsFemale()
    {
        Assert.True(sut.IsFemale());
    }

    [Fact]
    public void testIsFemaleDNumber()
    {
        sut = new Fodselsnummer(VALID_D_FODSELSNUMMER);
        Assert.True(sut.IsFemale());
    }

    [Fact]
    public void testIsDNumber()
    {
        Assert.False(Fodselsnummer.IsDNumber("01010101006"));
        Assert.False(Fodselsnummer.IsDNumber("80000000000"));
        Assert.True(Fodselsnummer.IsDNumber("47086303651"));
    }

    [Fact]
    public void testParseDNumber()
    {
        Assert.Equal("07086303651", Fodselsnummer.ParseDNumber("47086303651"));
    }
}

using Xunit;
using NoCommons.Person;

namespace NoCommons.Tests.Person
{
        public class FodselsnummerTest {

        private const string VALID_FODSELSNUMMER = "01010123476";

        private const string VALID_D_FODSELSNUMMER = "41010123476";

        private Fodselsnummer sut;

        public FodselsnummerTest() {
	        sut = new Fodselsnummer(VALID_FODSELSNUMMER);
        }

        [Fact]
        public void testGetDateAndMonth() {
	        Assert.Equal("0101", sut.getDateAndMonth());
        }

        [Fact]
        public void testGetDayInMonth() {
	        Assert.Equal("01", sut.getDayInMonth());
	        sut = new Fodselsnummer(VALID_D_FODSELSNUMMER);
	        Assert.Equal("01", sut.getDayInMonth());
        }

        [Fact]
        public void testGetMonth() {
	        Assert.Equal("01", sut.getMonth());
        }

        [Fact]
        public void testGetDateAndMonthDNumber() {
	        sut = new Fodselsnummer(VALID_D_FODSELSNUMMER);
	        Assert.Equal("0101", sut.getDateAndMonth());
        }

        [Fact]
        public void testGetCentury() {
	        sut = new Fodselsnummer("01016666609");
	        Assert.Equal("18", sut.getCentury());

	        sut = new Fodselsnummer("01016633301");
	        Assert.Equal("19", sut.getCentury());

	        sut = new Fodselsnummer("01019196697");
	        Assert.Equal("19", sut.getCentury());

	        sut = new Fodselsnummer("01013366671");
	        Assert.Equal("20", sut.getCentury());

	        // DNumber...
	        sut = new Fodselsnummer("41016666609");
	        Assert.Equal("18", sut.getCentury());

            sut = new Fodselsnummer("01015466609");
            Assert.Equal("18", sut.getCentury());

            sut = new Fodselsnummer("41016633301");
	        Assert.Equal("19", sut.getCentury());

	        sut = new Fodselsnummer("41019196697");
	        Assert.Equal("19", sut.getCentury());

	        sut = new Fodselsnummer("41013366671");
	        Assert.Equal("20", sut.getCentury());
        }

        [Fact]
        public void testGet2DigitBirthYear() {
	        Assert.Equal("01", sut.get2DigitBirthYear());
        }

        [Fact]
        public void testGetBirthYear() {
	        Assert.Equal("1901", sut.getBirthYear());
	        sut = new Fodselsnummer(VALID_D_FODSELSNUMMER);
	        Assert.Equal("1901", sut.getBirthYear());
        }

        [Fact]
        public void testGetDateOfBirth() {
	        Assert.Equal("010101", sut.getDateOfBirth());
        }

        [Fact]
        public void testGetDateOfBirthDNumber() {
	        sut = new Fodselsnummer(VALID_D_FODSELSNUMMER);
	        Assert.Equal("010101", sut.getDateOfBirth());
        }

        [Fact]
        public void testGetPersonnummer() {
	        Assert.Equal("23476", sut.getPersonnummer());
        }

        [Fact]
        public void testGetIndividnummer() {
	        Assert.Equal("234", sut.getIndividnummer());
        }

        [Fact]
        public void testGetGenderDigit() {
	        Assert.Equal(4, sut.getGenderDigit());
        }

        [Fact]
        public void testGetChecksumDigits() {
	        Assert.Equal(7, sut.getChecksumDigit1());
	        Assert.Equal(6, sut.getChecksumDigit2());
        }

        [Fact]
        public void testIsMale() {
	        Assert.False(sut.isMale());
        }

        [Fact]
        public void testIsMaleDNumber() {
	        sut = new Fodselsnummer(VALID_D_FODSELSNUMMER);
	        Assert.False(sut.isMale());
        }

        [Fact]
        public void testIsFemale() {
	        Assert.True(sut.isFemale());
        }

        [Fact]
        public void testIsFemaleDNumber() {
	        sut = new Fodselsnummer(VALID_D_FODSELSNUMMER);
	        Assert.True(sut.isFemale());
        }

        [Fact]
        public void testIsDNumber() {
	        Assert.False(Fodselsnummer.isDNumber("01010101006"));
	        Assert.False(Fodselsnummer.isDNumber("80000000000"));
	        Assert.True(Fodselsnummer.isDNumber("47086303651"));
        }

        [Fact]
        public void testParseDNumber() {
	        Assert.Equal("07086303651", Fodselsnummer.parseDNumber("47086303651"));
        }
    }
}
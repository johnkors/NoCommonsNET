using NUnit.Framework;
using NoCommons.Person;

namespace NoCommons.Tests.Person
{
        public class FodselsnummerTest {

        private const string VALID_FODSELSNUMMER = "01010123476";

        private const string VALID_D_FODSELSNUMMER = "41010123476";

        private Fodselsnummer sut;

        [SetUp]
        public void setUpValidFodselsnummer() {
	        sut = new Fodselsnummer(VALID_FODSELSNUMMER);
        }

        [Test]
        public void testGetDateAndMonth() {
	        Assert.AreEqual("0101", sut.getDateAndMonth());
        }

        [Test]
        public void testGetDayInMonth() {
	        Assert.AreEqual("01", sut.getDayInMonth());
	        sut = new Fodselsnummer(VALID_D_FODSELSNUMMER);
	        Assert.AreEqual("01", sut.getDayInMonth());
        }

        [Test]
        public void testGetMonth() {
	        Assert.AreEqual("01", sut.getMonth());
        }

        [Test]
        public void testGetDateAndMonthDNumber() {
	        sut = new Fodselsnummer(VALID_D_FODSELSNUMMER);
	        Assert.AreEqual("0101", sut.getDateAndMonth());
        }

        [Test]
        public void testGetCentury() {
	        sut = new Fodselsnummer("01016666609");
	        Assert.AreEqual("18", sut.getCentury());

	        sut = new Fodselsnummer("01016633301");
	        Assert.AreEqual("19", sut.getCentury());

	        sut = new Fodselsnummer("01019196697");
	        Assert.AreEqual("19", sut.getCentury());

	        sut = new Fodselsnummer("01013366671");
	        Assert.AreEqual("20", sut.getCentury());

	        // DNumber...
	        sut = new Fodselsnummer("41016666609");
	        Assert.AreEqual("18", sut.getCentury());

            sut = new Fodselsnummer("01015466609");
            Assert.AreEqual("18", sut.getCentury());

            sut = new Fodselsnummer("41016633301");
	        Assert.AreEqual("19", sut.getCentury());

	        sut = new Fodselsnummer("41019196697");
	        Assert.AreEqual("19", sut.getCentury());

	        sut = new Fodselsnummer("41013366671");
	        Assert.AreEqual("20", sut.getCentury());
        }

        [Test]
        public void testGet2DigitBirthYear() {
	        Assert.AreEqual("01", sut.get2DigitBirthYear());
        }

        [Test]
        public void testGetBirthYear() {
	        Assert.AreEqual("1901", sut.getBirthYear());
	        sut = new Fodselsnummer(VALID_D_FODSELSNUMMER);
	        Assert.AreEqual("1901", sut.getBirthYear());
        }

        [Test]
        public void testGetDateOfBirth() {
	        Assert.AreEqual("010101", sut.getDateOfBirth());
        }

        [Test]
        public void testGetDateOfBirthDNumber() {
	        sut = new Fodselsnummer(VALID_D_FODSELSNUMMER);
	        Assert.AreEqual("010101", sut.getDateOfBirth());
        }

        [Test]
        public void testGetPersonnummer() {
	        Assert.AreEqual("23476", sut.getPersonnummer());
        }

        [Test]
        public void testGetIndividnummer() {
	        Assert.AreEqual("234", sut.getIndividnummer());
        }

        [Test]
        public void testGetGenderDigit() {
	        Assert.AreEqual(4, sut.getGenderDigit());
        }

        [Test]
        public void testGetChecksumDigits() {
	        Assert.AreEqual(7, sut.getChecksumDigit1());
	        Assert.AreEqual(6, sut.getChecksumDigit2());
        }

        [Test]
        public void testIsMale() {
	        Assert.AreEqual(false, sut.isMale());
        }

        [Test]
        public void testIsMaleDNumber() {
	        sut = new Fodselsnummer(VALID_D_FODSELSNUMMER);
	        Assert.AreEqual(false, sut.isMale());
        }

        [Test]
        public void testIsFemale() {
	        Assert.AreEqual(true, sut.isFemale());
        }

        [Test]
        public void testIsFemaleDNumber() {
	        sut = new Fodselsnummer(VALID_D_FODSELSNUMMER);
	        Assert.AreEqual(true, sut.isFemale());
        }

        [Test]
        public void testIsDNumber() {
	        Assert.IsFalse(Fodselsnummer.isDNumber("01010101006"));
	        Assert.IsFalse(Fodselsnummer.isDNumber("80000000000"));
	        Assert.IsTrue(Fodselsnummer.isDNumber("47086303651"));
        }

        [Test]
        public void testParseDNumber() {
	        Assert.AreEqual("07086303651", Fodselsnummer.parseDNumber("47086303651"));
        }
    }
}
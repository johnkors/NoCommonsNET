using System;
using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;
using NoCommons.Person;

namespace NoCommons.Tests.Person
{
    public class FodselsnummerCalculatorTest
    {
        private DateTime date;
        private const string _dateFormat = "ddMMyyyy";

        [SetUp]
        public void SetUpDate()
        {
            date = new DateTime(2006, 9, 6);
        }

        [Test]
        public void testGetFodselsnummerForDateAndGender()
        {
            List<Fodselsnummer> options = FodselsnummerCalculator.getFodselsnummerForDateAndGender(date, KJONN.KVINNE);
            Assert.IsTrue(options.Count > 10, "Forventet minst 10 fødselsnumre, men fikk " + options.Count);
        }

        [Test]
        public void testGetFodselsnummerForDate()
        {
            List<Fodselsnummer> options = FodselsnummerCalculator.getManyFodselsnummerForDate(date);
            Assert.IsTrue(options.Count > 20, "Forventet minst 20 fødselsnumre, men fikk " + options.Count);
        }

        [Test]
        public void getValidFodselsnummerForDate()
        {
            List<Fodselsnummer> validOptions = FodselsnummerCalculator.getManyFodselsnummerForDate(date);
            Assert.IsTrue(validOptions.Count == 38, "Forventet 38 fødselsnumre, men fikk " + validOptions.Count);
        }

        [Test]
        public void testThatAllGeneratedNumbersAreValid()
        {
            foreach (Fodselsnummer fnr in FodselsnummerCalculator.getManyFodselsnummerForDate(date))
            {
                Assert.IsTrue(FodselsnummerValidator.IsValid(fnr.ToString()), "Ugyldig fødselsnummer: " + fnr);
            }
        }

        [Test]
        public void testInvalidDateTooEarly()
        {
            date = DateTime.ParseExact("09091853", _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
            List<Fodselsnummer> options = FodselsnummerCalculator.getManyFodselsnummerForDate(date);
            Assert.AreEqual(0, options.Count);
        }

        [Test]
        public void testInvalidDateTooLate()
        {
            date = DateTime.ParseExact("09092040", _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
            List<Fodselsnummer> options = FodselsnummerCalculator.getManyFodselsnummerForDate(date);
            Assert.AreEqual(0, options.Count);
        }

        [Test]
        public void testOneFodselsnummer()
        {
            date = DateTime.ParseExact("01121980", _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
            Fodselsnummer fodselsnummer = FodselsnummerCalculator.getFodselsnummerForDate(date);
            Assert.IsTrue(FodselsnummerValidator.IsValid(fodselsnummer.ToString()));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using Xunit;
using NoCommons.Person;

namespace NoCommons.Tests.Person
{
    public class FodselsnummerCalculatorTest
    {
        private DateTime date;
        private const string _dateFormat = "ddMMyyyy";

        public FodselsnummerCalculatorTest()
        {
            date = new DateTime(2006, 9, 6);
        }

        [Fact]
        public void testGetFodselsnummerForDateAndGender()
        {
            List<Fodselsnummer> options = FodselsnummerCalculator.getFodselsnummerForDateAndGender(date, KJONN.KVINNE);
            Assert.True(options.Count > 10, "Forventet minst 10 fødselsnumre, men fikk " + options.Count);
        }

        [Fact]
        public void testGetFodselsnummerForDate()
        {
            List<Fodselsnummer> options = FodselsnummerCalculator.getManyFodselsnummerForDate(date);
            Assert.True(options.Count > 20, "Forventet minst 20 fødselsnumre, men fikk " + options.Count);
        }

        [Fact]
        public void getValidFodselsnummerForDate()
        {
            List<Fodselsnummer> validOptions = FodselsnummerCalculator.getManyFodselsnummerForDate(date);
            Assert.True(validOptions.Count == 38, "Forventet 38 fødselsnumre, men fikk " + validOptions.Count);
        }

        [Fact]
        public void testThatAllGeneratedNumbersAreValid()
        {
            foreach (Fodselsnummer fnr in FodselsnummerCalculator.getManyFodselsnummerForDate(date))
            {
                Assert.True(FodselsnummerValidator.IsValid(fnr.ToString()), "Ugyldig fødselsnummer: " + fnr);
            }
        }

        [Fact]
        public void testInvalidDateTooEarly()
        {
            date = DateTime.ParseExact("09091853", _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
            List<Fodselsnummer> options = FodselsnummerCalculator.getManyFodselsnummerForDate(date);
            Assert.Equal(0, options.Count);
        }

        [Fact]
        public void testInvalidDateTooLate()
        {
            date = DateTime.ParseExact("09092040", _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
            List<Fodselsnummer> options = FodselsnummerCalculator.getManyFodselsnummerForDate(date);
            Assert.Equal(0, options.Count);
        }

        [Fact]
        public void testOneFodselsnummer()
        {
            date = DateTime.ParseExact("01121980", _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
            Fodselsnummer fodselsnummer = FodselsnummerCalculator.getFodselsnummerForDate(date);
            Assert.True(FodselsnummerValidator.IsValid(fodselsnummer.ToString()));
        }
    }
}
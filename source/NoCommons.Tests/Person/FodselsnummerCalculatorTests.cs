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
            List<Fodselsnummer> options = FodselsnummerCalculator.GetFodselsnummerForDateAndGender(date, KJONN.KVINNE);
            Assert.Equal(207, options.Count);
        }

        [Fact]
        public void getValidFodselsnummerForDate()
        {
            List<Fodselsnummer> validOptions = FodselsnummerCalculator.GetManyFodselsnummerForDate(date);
            Assert.True(validOptions.Count == 412, "Forventet 412 fødselsnumre, men fikk " + validOptions.Count);
        }

        [Fact]
        public void getValidFodselsnummerForDNumberDate()
        {
            List<Fodselsnummer> validOptions = FodselsnummerCalculator.GetManyFodselsnummerForDate(date, true);
            Assert.True(validOptions.Count == 413, "Forventet 412 fødselsnumre som er d-nummer, men fikk " + validOptions.Count);
        }

        [Fact]
        public void testThatAllGeneratedNumbersAreValid()
        {
            foreach (Fodselsnummer fnr in FodselsnummerCalculator.GetManyFodselsnummerForDate(date))
            {
                Assert.True(FodselsnummerValidator.IsValid(fnr.ToString()), "Ugyldig fødselsnummer: " + fnr);
            }
        }

        [Fact]
        public void testThatAllGeneratedDFodselsnumbersAreValid()
        {
            foreach (Fodselsnummer fnr in FodselsnummerCalculator.GetManyFodselsnummerForDate(date, true))
            {
                Assert.True(FodselsnummerValidator.IsValid(fnr.ToString()), "Ugyldig fødselsnummer: " + fnr);
            }
        }

        [Fact]
        public void testInvalidDateTooEarly()
        {
            date = DateTime.ParseExact("09091853", _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
            List<Fodselsnummer> options = FodselsnummerCalculator.GetManyFodselsnummerForDate(date);
            Assert.Empty(options);
        }

        [Fact]
        public void testInvalidDateTooLate()
        {
            date = DateTime.ParseExact("09092040", _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
            List<Fodselsnummer> options = FodselsnummerCalculator.GetManyFodselsnummerForDate(date);
            Assert.Empty(options);
        }

        [Fact]
        public void testOneFodselsnummer()
        {
            date = DateTime.ParseExact("01121980", _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
            Fodselsnummer fodselsnummer = FodselsnummerCalculator.GetFodselsnummerForDate(date);
            Assert.True(FodselsnummerValidator.IsValid(fodselsnummer.ToString()));
        }
    }
}
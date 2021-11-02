using System;
using Xunit;
using NoCommons.Person;

namespace NoCommons.Tests.Person
{
    public class FodselsnummerValidatorTests
    {
        [Fact]
        public void testInvalidFodselsnummerWrongLength()
        {
            try
            {
                FodselsnummerValidator.ValidateSyntax("0123456789");
                Assert.True(false);
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, FodselsnummerValidator.ERROR_SYNTAX);
            }
        }

        [Fact]
        public void testInvalidFodselsnummerNotDigits()
        {
            try
            {
                FodselsnummerValidator.ValidateSyntax("abcdefghijk");
                Assert.True(false);
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, FodselsnummerValidator.ERROR_SYNTAX);
            }
        }

        [Fact]
        public void testInvalidIndividnummer()
        {
            try
            {
                FodselsnummerValidator.validateIndividnummer("01015780000");
                Assert.True(false);
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, FodselsnummerValidator.ERROR_INVALID_INDIVIDNUMMER);
            }
        }

        [Fact]
        public void testInvalidDateMonthMax()
        {
            try
            {
                FodselsnummerValidator.validateDate("01130400000");
                Assert.True(false);
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, FodselsnummerValidator.ERROR_INVALID_DATE);
            }
        }

        [Fact]
        public void testInvalidDateMonthMin()
        {
            try
            {
                FodselsnummerValidator.validateDate("01000400000");
                Assert.True(false);
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, FodselsnummerValidator.ERROR_INVALID_DATE);
            }
        }

        [Fact]
        public void testInvalidDateDayMin()
        {
            try
            {
                FodselsnummerValidator.validateDate("00120467800");
                Assert.True(false);
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, FodselsnummerValidator.ERROR_INVALID_DATE);
            }
        }

        [Fact]
        public void testInvalidDateDayMax()
        {
            try
            {
                FodselsnummerValidator.validateDate("32120400000");
                Assert.True(false);
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, FodselsnummerValidator.ERROR_INVALID_DATE);
            }
        }

        [Fact]
        public void testInvalidDateLeapDay()
        {
            try
            {
                FodselsnummerValidator.validateDate("29020700000");
                Assert.True(false);
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, FodselsnummerValidator.ERROR_INVALID_DATE);
            }
        }

        [Fact]
        public void testInvalidFodselsnummerChecksum()
        {
            try
            {
                FodselsnummerValidator.validateChecksums("01010101010");
                Assert.True(false);
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, FodselsnummerValidator.ERROR_INVALID_CHECKSUM);
            }
        }

        [Fact]
        public void testValidLeapDay()
        {
            Assert.True(FodselsnummerValidator.IsValid("29029633310"));
        }

        [Fact]
        public void testIsValid()
        {
            Assert.True(FodselsnummerValidator.IsValid("01010101006"));
        }

        [Fact]
        public void testDNumberIsValid()
        {
            Assert.True(FodselsnummerValidator.IsValid("47086303651"));
        }

        [Fact]
        public void testGetDNumber()
        {
            FodselsnummerValidator.getFodselsnummer("47086303651");
        }
    }
}

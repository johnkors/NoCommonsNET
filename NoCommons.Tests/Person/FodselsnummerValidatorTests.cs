using System;
using NUnit.Framework;
using NoCommons.Person;

namespace NoCommons.Tests.Person
{
    [TestFixture]
    public class FodselsnummerValidatorTests
    {
        [Test]
        public void testInvalidFodselsnummerWrongLength()
        {
            try
            {
                FodselsnummerValidator.ValidateSyntax("0123456789");
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, FodselsnummerValidator.ERROR_SYNTAX);
            }
        }

        [Test]
        public void testInvalidFodselsnummerNotDigits()
        {
            try
            {
                FodselsnummerValidator.ValidateSyntax("abcdefghijk");
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, FodselsnummerValidator.ERROR_SYNTAX);
            }
        }

        [Test]
        public void testInvalidIndividnummer()
        {
            try
            {
                FodselsnummerValidator.validateIndividnummer("01015780000");
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, FodselsnummerValidator.ERROR_INVALID_INDIVIDNUMMER);
            }
        }

        [Test]
        public void testInvalidDateMonthMax()
        {
            try
            {
                FodselsnummerValidator.validateDate("01130400000");
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, FodselsnummerValidator.ERROR_INVALID_DATE);
            }
        }

        [Test]
        public void testInvalidDateMonthMin()
        {
            try
            {
                FodselsnummerValidator.validateDate("01000400000");
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, FodselsnummerValidator.ERROR_INVALID_DATE);
            }
        }

        [Test]
        public void testInvalidDateDayMin()
        {
            try
            {
                FodselsnummerValidator.validateDate("00120467800");
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, FodselsnummerValidator.ERROR_INVALID_DATE);
            }
        }

        [Test]
        public void testInvalidDateDayMax()
        {
            try
            {
                FodselsnummerValidator.validateDate("32120400000");
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, FodselsnummerValidator.ERROR_INVALID_DATE);
            }
        }

        [Test]
        public void testInvalidDateLeapDay()
        {
            try
            {
                FodselsnummerValidator.validateDate("29020700000");
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, FodselsnummerValidator.ERROR_INVALID_DATE);
            }
        }

        [Test]
        public void testInvalidFodselsnummerChecksum()
        {
            try
            {
                FodselsnummerValidator.validateChecksums("01010101010");
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                AssertionUtils.AssertMessageContains(e, FodselsnummerValidator.ERROR_INVALID_CHECKSUM);
            }
        }

        [Test]
        public void testValidLeapDay()
        {
            Assert.IsTrue(FodselsnummerValidator.IsValid("29029633310"));
        }

        [Test]
        public void testIsValid()
        {
            Assert.IsTrue(FodselsnummerValidator.IsValid("01010101006"));
        }

        [Test]
        public void testDNumberIsValid()
        {
            Assert.IsTrue(FodselsnummerValidator.IsValid("47086303651"));
        }

        [Test]
        public void testGetDNumber()
        {
            FodselsnummerValidator.getFodselsnummer("47086303651");
        }
    }
}

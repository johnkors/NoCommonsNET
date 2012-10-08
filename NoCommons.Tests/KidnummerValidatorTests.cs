using System;
using NUnit.Framework;
using NoCommons.Banking;
using NoCommons.Common;

namespace NoCommons.Tests.Banking
{
    [TestFixture]
    public class KidnummerValidatorTests
    {
        private const string KIDNUMMER_VALID_MOD10 = "2345676";
        private const string KIDNUMMER_VALID_MOD11 = "12345678903";
        private const string KIDNUMMER_INVALID_CHECKSUM = "2345674";
        private const string KIDNUMMER_INVALID_LENGTH_SHORT = "1";
        private const string KIDNUMMER_INVALID_LENGTH_LONG = "12345678901234567890123456";
        
        protected void assertMessageContains(ArgumentException e, String message)
        {
            Assert.IsTrue(e.Message.Contains(message));
        }

        [Test]
        public void testInvalidKidnummer() {
            try {
                KidnummerValidator.validateSyntax("");
                Assert.Fail();
            } catch (ArgumentException e) {
                assertMessageContains(e, StringNumberValidator.ERROR_SYNTAX);
            }
        }

        [Test]
        public void testInvalidKidnummerNotDigits() {
            try {
                KidnummerValidator.validateSyntax("abcdefghijk");
                Assert.Fail();
            } catch (ArgumentException e) {
                assertMessageContains(e, StringNumberValidator.ERROR_SYNTAX);
            }
        }

        [Test]
        public void testInvalidKidnummerTooShort() {
            try {
                KidnummerValidator.validateSyntax(KIDNUMMER_INVALID_LENGTH_SHORT);
                Assert.Fail();
            } catch (ArgumentException e) {
                assertMessageContains(e, KidnummerValidator.ERROR_LENGTH);
            }
        }

        [Test]
        public void testInvalidKidnummerTooLong() {
            try {
                KidnummerValidator.validateSyntax(KIDNUMMER_INVALID_LENGTH_LONG);
                Assert.Fail();
            } catch (ArgumentException e) {
                assertMessageContains(e, KidnummerValidator.ERROR_LENGTH);
            }
        }

        [Test]
        public void testInvalidKidnummerWrongChecksum() {
            try {
                KidnummerValidator.validateChecksum(KIDNUMMER_INVALID_CHECKSUM);
                Assert.Fail();
            } catch (ArgumentException e) {
                assertMessageContains(e, StringNumberValidator.ERROR_INVALID_CHECKSUM);
            }
        }

        [Test]
        public void testIsValidMod10() {
            Assert.IsTrue(KidnummerValidator.IsValid(KIDNUMMER_VALID_MOD10));
        }

        [Test]
        public void testIsValidMod11() {
            Assert.IsTrue(KidnummerValidator.IsValid(KIDNUMMER_VALID_MOD11));
        }

        [Test]
        public void testIsInvalid() {
            Assert.IsFalse(KidnummerValidator.IsValid(KIDNUMMER_INVALID_CHECKSUM));
        }
    }
}
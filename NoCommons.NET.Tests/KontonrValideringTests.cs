using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoCommons.Banking;
using NoCommons.Common;

namespace NoCommons.Tests
{
    [TestClass]
    public class KontonrValideringTests
    {
        private const string KONTONUMMER_VALID = "99990000006";
        private const string KONTONUMMER_INVALID_CHECKSUM = "99990000005";

        [TestMethod]
        public void TestInvalidKontonummerWrongLength()
        {
            try
            {
                KontonummerValidator.ValidateSyntax("123456789012");
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                AssertMessageContains(e, StringNumberValidator.ERROR_SYNTAX);
            }
        }

        [TestMethod]
        public void TestInvalidKontonummerNotDigits()
        {
            try
            {
                KontonummerValidator.ValidateSyntax("abcdefghijk");
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                AssertMessageContains(e, StringNumberValidator.ERROR_SYNTAX);
            }
        }

        [TestMethod]
        public void TestInvalidKontonummerWrongChecksum()
        {
            try
            {
                KontonummerValidator.ValidateChecksum(KONTONUMMER_INVALID_CHECKSUM);
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                AssertMessageContains(e, StringNumberValidator.ERROR_INVALID_CHECKSUM);
            }
        }

        [TestMethod]
        public void TestInvalidAccountTypeWrongLength()
        {
            var b = new StringBuilder(KontonummerValidator.ACCOUNTTYPE_NUM_DIGITS + 1);
            for (int i = 0; i < KontonummerValidator.ACCOUNTTYPE_NUM_DIGITS + 1; i++)
            {
                b.Append("0");
            }
            try
            {
                KontonummerValidator.ValidateAccountTypeSyntax(b.ToString());
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                AssertMessageContains(e, StringNumberValidator.ERROR_SYNTAX);
            }
        }

        [TestMethod]
        public void TestInvalidAccountTypeNotDigits()
        {
            var b = new StringBuilder(KontonummerValidator.ACCOUNTTYPE_NUM_DIGITS);
            for (int i = 0; i < KontonummerValidator.ACCOUNTTYPE_NUM_DIGITS; i++)
            {
                b.Append("A");
            }
            try
            {
                KontonummerValidator.ValidateAccountTypeSyntax(b.ToString());
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                AssertMessageContains(e, StringNumberValidator.ERROR_SYNTAX);
            }
        }

        [TestMethod]
        public void TestInvalidRegisternummerNotDigits()
        {
            var b = new StringBuilder(KontonummerValidator.REGISTERNUMMER_NUM_DIGITS);
            for (int i = 0; i < KontonummerValidator.REGISTERNUMMER_NUM_DIGITS; i++)
            {
                b.Append("A");
            }
            try
            {
                KontonummerValidator.ValidateRegisternummerSyntax(b.ToString());
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                AssertMessageContains(e, StringNumberValidator.ERROR_SYNTAX);
            }
        }

        [TestMethod]
        public void TestInvalidRegisternummerWrongLength()
        {
            var b = new StringBuilder(KontonummerValidator.REGISTERNUMMER_NUM_DIGITS + 1);
            for (int i = 0; i < KontonummerValidator.REGISTERNUMMER_NUM_DIGITS + 1; i++)
            {
                b.Append("0");
            }
            try
            {
                KontonummerValidator.ValidateRegisternummerSyntax(b.ToString());
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                AssertMessageContains(e, StringNumberValidator.ERROR_SYNTAX);
            }
        }

        [TestMethod]
        public void TestGetValidKontonummerFromInvalidKontonummerWrongChecksum()
        {
            Kontonummer knr = KontonummerValidator.GetAndForceValidKontonummer(KONTONUMMER_INVALID_CHECKSUM);
            Assert.IsTrue(KontonummerValidator.IsValid(knr.ToString()));
        }

        [TestMethod]
        public void TestIsValid()
        {
            Assert.IsTrue(KontonummerValidator.IsValid(KONTONUMMER_VALID));
            Assert.IsFalse(KontonummerValidator.IsValid(KONTONUMMER_INVALID_CHECKSUM));
        }

        [TestMethod]
        public void TestValidNumberEndingOn9()
        {
            Assert.IsTrue(KontonummerValidator.IsValid("97104133219"));
            Assert.IsTrue(KontonummerValidator.IsValid("97105302049"));
            Assert.IsTrue(KontonummerValidator.IsValid("97104008309"));
            Assert.IsTrue(KontonummerValidator.IsValid("97102749069"));
        }

        private static void AssertMessageContains(ArgumentException argumentException, string errorSyntax)
        {
            bool containsText = argumentException.Message.Contains(errorSyntax);
            Assert.IsTrue(containsText);
        }

    }
}

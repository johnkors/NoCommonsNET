using System;
using System.Text;
using NUnit.Framework;
using NoCommons.Common;

namespace NoCommons.Banking.Tests
{
    [TestFixture]
    public class KontonrValideringTests
    {
        private const string KONTONUMMER_VALID = "99990000006";
        private const string KONTONUMMER_INVALID_CHECKSUM = "99990000005";

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
        public void TestGetValidKontonummerFromInvalidKontonummerWrongChecksum()
        {
            Kontonummer knr = KontonummerValidator.GetAndForceValidKontonummer(KONTONUMMER_INVALID_CHECKSUM);
            Assert.IsTrue(KontonummerValidator.IsValid(knr.ToString()));
        }

        [Test]
        public void TestIsValid()
        {
            Assert.IsTrue(KontonummerValidator.IsValid(KONTONUMMER_VALID));
            Assert.IsFalse(KontonummerValidator.IsValid(KONTONUMMER_INVALID_CHECKSUM));
        }

        [TestCase("97104133219")]
        [TestCase("97105302049")]
        [TestCase("97104008309")]
        [TestCase("97102749069")]
        public void TestValidNumberEndingOn9(string kontonrEndingOn9)
        {
            Assert.IsTrue(KontonummerValidator.IsValid(kontonrEndingOn9));
        }

        private static void AssertMessageContains(ArgumentException argumentException, string errorSyntax)
        {
            bool containsText = argumentException.Message.Contains(errorSyntax);
            Assert.IsTrue(containsText);
        }


        
    }
}

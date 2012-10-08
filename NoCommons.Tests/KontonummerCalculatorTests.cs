using System.Collections.Generic;
using NUnit.Framework;
using NoCommons.Banking;

namespace NoCommons.NET.Tests
{
    public class KontonummerCalculatorTests
    {
        private const int LIST_LENGTH = 100;
        private const string TEST_ACCOUNT_TYPE = "45";
        private const string TEST_REGISTERNUMMER = "9710";

        [Test]
        public void testGetKontonummerList()
        {
            List<Kontonummer> options = KontonummerCalculator.GetKontonummerList(LIST_LENGTH);
            Assert.AreEqual(LIST_LENGTH, options.Count);
            foreach (Kontonummer k in options)
            {
                Assert.IsTrue(KontonummerValidator.IsValid(k.ToString()));
            }
        }

        [Test]
        public void testGetKontonummerListForAccountType()
        {
            List<Kontonummer> options = KontonummerCalculator.GetKontonummerListForAccountType(TEST_ACCOUNT_TYPE, LIST_LENGTH);
            Assert.AreEqual(LIST_LENGTH, options.Count);
            foreach (Kontonummer option in options)
            {
                Assert.IsTrue(KontonummerValidator.IsValid(option.ToString()), "Invalid kontonr. ");
                Assert.IsTrue(option.GetAccountType().Equals(TEST_ACCOUNT_TYPE), "Invalid account type. ");
            }
        }

        [Test]
        public void testGetKontonummerListForRegisternummer()
        {
            List<Kontonummer> options = KontonummerCalculator.GetKontonummerListForRegisternummer(TEST_REGISTERNUMMER, LIST_LENGTH);
            Assert.AreEqual(LIST_LENGTH, options.Count);
            foreach (Kontonummer option in options)
            {
                
                Assert.IsTrue(KontonummerValidator.IsValid(option.ToString()));
                Assert.IsTrue(option.GetRegisternummer().Equals(TEST_REGISTERNUMMER));
            }
        }
    }
}
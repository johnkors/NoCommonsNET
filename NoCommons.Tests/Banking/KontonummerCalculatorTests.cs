using NUnit.Framework;
using NoCommons.Banking;

namespace NoCommons.Tests.Banking
{
    [TestFixture]
    public class KontonummerCalculatorTests
    {
        private const int LIST_LENGTH = 100;
        private const string TEST_ACCOUNT_TYPE = "45";
        private const string TEST_REGISTERNUMMER = "9710";

        [Test]
        public void testGetKontonummerList()
        {
            var options = KontonummerCalculator.GetKontonummerList(LIST_LENGTH);
            Assert.AreEqual(LIST_LENGTH, options.Count);
            foreach (Kontonummer k in options)
            {
                Assert.IsTrue(KontonummerValidator.IsValid(k.ToString()));
            }
        }

        [Test]
        public void testGetKontonummerListForAccountType()
        {
            var options = KontonummerCalculator.GetKontonummerListForAccountType(TEST_ACCOUNT_TYPE, LIST_LENGTH);
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
            var options = KontonummerCalculator.GetKontonummerListForRegisternummer(TEST_REGISTERNUMMER, LIST_LENGTH);
            Assert.AreEqual(LIST_LENGTH, options.Count);
            foreach (Kontonummer option in options)
            {
                
                Assert.IsTrue(KontonummerValidator.IsValid(option.ToString()));
                Assert.IsTrue(option.GetRegisternummer().Equals(TEST_REGISTERNUMMER));
            }
        }
    }


}
using Xunit;
using NoCommons.Banking;

namespace NoCommons.Tests.Banking
{
    public class KontonummerCalculatorTests
    {
        private const int LIST_LENGTH = 100;
        private const string TEST_ACCOUNT_TYPE = "45";
        private const string TEST_REGISTERNUMMER = "9710";

        [Fact]
        public void testGetKontonummerList()
        {
            var options = KontonummerCalculator.GetKontonummerList(LIST_LENGTH);
            Assert.Equal(LIST_LENGTH, options.Count);
            foreach (Kontonummer k in options)
            {
                Assert.True(KontonummerValidator.IsValid(k.ToString()));
            }
        }

        [Fact]
        public void testGetKontonummerListForAccountType()
        {
            var options = KontonummerCalculator.GetKontonummerListForAccountType(TEST_ACCOUNT_TYPE, LIST_LENGTH);
            Assert.Equal(LIST_LENGTH, options.Count);
            foreach (Kontonummer option in options)
            {
                Assert.True(KontonummerValidator.IsValid(option.ToString()), "Invalid kontonr. ");
                Assert.True(option.GetAccountType().Equals(TEST_ACCOUNT_TYPE), "Invalid account type. ");
            }
        }

        [Fact]
        public void testGetKontonummerListForRegisternummer()
        {
            var options = KontonummerCalculator.GetKontonummerListForRegisternummer(TEST_REGISTERNUMMER, LIST_LENGTH);
            Assert.Equal(LIST_LENGTH, options.Count);
            foreach (Kontonummer option in options)
            {
                
                Assert.True(KontonummerValidator.IsValid(option.ToString()));
                Assert.True(option.GetRegisternummer().Equals(TEST_REGISTERNUMMER));
            }
        }
    }


}
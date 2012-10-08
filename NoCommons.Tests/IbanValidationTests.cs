using NUnit.Framework;
using NoCommons.Banking;

namespace NoCommons.Tests.Banking
{
    [TestFixture]
    public class IbanValidationTests
    {
        /// Structure for IBAN in a lot of counties are found here:
        /// URL: http://www.ecbs.org/iban.htm
        
        [TestCase("NO9386011117947", "Norway")]
        [TestCase("ES9121000418450200051332", "Spain")]
        public void TestValidNorwegianIban(string ibanValue, string country)
        {
            Assert.IsTrue(IbanValidator.IsValid(ibanValue), string.Format("Invalid for {0}", country));
        }

        [TestCase("NO93 8601 1117 947", "On valid print format, but not valid digital format")]
        [TestCase("ES91 2100 0418 4502 0005 1332", "On valid print format, but not valid digital format")]
        [TestCase("no9386011117947", "Lower case country code")]
        [TestCase("9386011117947", "Missing country code")]
        [TestCase("NO938601111794", "Missing end digit")]
        [TestCase("", "Empty string")]
        [TestCase(null, "null")]
        public void InvalidIbansReturnsFalse(string ibanValue, string reason)
        {
            Assert.IsFalse(IbanValidator.IsValid(ibanValue), string.Format("Valid when {0}. ", reason));
        }

    }
}
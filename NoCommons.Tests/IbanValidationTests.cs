using NUnit.Framework;

namespace NoCommons.Banking.Tests
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
    }
}
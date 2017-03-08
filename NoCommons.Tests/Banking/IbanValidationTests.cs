using Xunit;
using NoCommons.Banking;

namespace NoCommons.Tests.Banking
{
    public class IbanValidationTests
    {
        /// Structure for IBAN in a lot of counties are found here:
        /// URL: http://www.ecbs.org/iban.htm
        [Theory]            
        [InlineData("NO9386011117947", "Norway")]
        [InlineData("ES9121000418450200051332", "Spain")]
        public void TestValidNorwegianIban(string ibanValue, string country)
        {
            Assert.True(IbanValidator.IsValid(ibanValue), string.Format("Invalid for {0}", country));
        }

        [Theory]
        [InlineData("NO93 8601 1117 947", "On valid print format, but not valid digital format")]
        [InlineData("ES91 2100 0418 4502 0005 1332", "On valid print format, but not valid digital format")]
        [InlineData("no9386011117947", "Lower case country code")]
        [InlineData("9386011117947", "Missing country code")]
        [InlineData("NO938601111794", "Missing end digit")]
        [InlineData("", "Empty string")]
        [InlineData(null, "null")]
        public void InvalidIbansReturnsFalse(string ibanValue, string reason)
        {
            Assert.False(IbanValidator.IsValid(ibanValue), string.Format("Valid when {0}. ", reason));
        }

    }
}
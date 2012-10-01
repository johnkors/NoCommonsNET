using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoCommons.Banking;

namespace NoCommons.Tests
{
    [TestClass]
    public class IbanValidationTests
    {
        /// Structure for IBAN in a lot of counties are found here:
        /// URL: http://www.ecbs.org/iban.htm
        
        [TestMethod]
        public void TestValidNorwegianIban()
        {
            var validIbanValue = "NO9386011117947";
            Assert.IsTrue(IbanValidator.IsValid(validIbanValue));
        }

        [TestMethod]
        public void TestValidSpanishIban()
        {
            var validSpanishIban = "ES9121000418450200051332";
            Assert.IsTrue(IbanValidator.IsValid(validSpanishIban));
        }

            
    }
}
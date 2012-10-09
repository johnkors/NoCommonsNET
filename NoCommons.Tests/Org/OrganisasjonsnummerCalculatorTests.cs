using NUnit.Framework;
using NoCommons.Org;

namespace NoCommons.Tests.Org
{
    [TestFixture]
    public class OrganisasjonsnummerCalculatorTests
    {
        private const int LIST_LENGTH = 100;

        [Test]
        public void testGetOrganisasjonsnummerList()
        {
            var options = OrganisasjonsnummerCalculator.GetOrganisasjonsnummerList(LIST_LENGTH);
            Assert.AreEqual(LIST_LENGTH, options.Count);
            foreach (var nr in options)
            {
                Assert.IsTrue(OrganisasjonsnummerValidator.IsValid(nr.ToString()));
            }
        }
    }
}
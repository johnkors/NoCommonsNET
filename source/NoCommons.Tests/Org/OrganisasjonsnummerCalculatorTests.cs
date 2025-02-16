using NoCommons.Org;

namespace NoCommons.Tests.Org;

public class OrganisasjonsnummerCalculatorTests
{
    private const int LIST_LENGTH = 100;

    [Fact]
    public void testGetOrganisasjonsnummerList()
    {
        List<Organisasjonsnummer>? options = OrganisasjonsnummerCalculator.GetOrganisasjonsnummerList(LIST_LENGTH);
        Assert.Equal(LIST_LENGTH, options.Count);
        foreach (Organisasjonsnummer? nr in options)
        {
            Assert.True(OrganisasjonsnummerValidator.IsValid(nr.ToString()));
        }
    }
}

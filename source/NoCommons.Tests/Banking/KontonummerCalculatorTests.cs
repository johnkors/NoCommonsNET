using NoCommons.Banking;

namespace NoCommons.Tests.Banking;

public class KontonummerCalculatorTests
{
    private const int LIST_LENGTH = 100;
    private const string TEST_ACCOUNT_TYPE = "45";
    private const string TEST_REGISTERNUMMER = "9710";

    [Fact]
    public void testGetKontonummerList()
    {
        List<Kontonummer>? options = KontonummerCalculator.GetKontonummerList(LIST_LENGTH);
        Assert.Equal(LIST_LENGTH, options.Count);
        foreach (Kontonummer k in options)
        {
            Assert.True(KontonummerValidator.IsValid(k.ToString()));
        }
    }

    [Fact]
    public void testGetKontonummerListForAccountType()
    {
        List<Kontonummer>? options =
            KontonummerCalculator.GetKontonummerListForAccountType(TEST_ACCOUNT_TYPE, LIST_LENGTH);
        Assert.Equal(LIST_LENGTH, options.Count);
        foreach (Kontonummer option in options)
        {
            Assert.True(KontonummerValidator.IsValid(option.ToString()));
            Assert.Equal(TEST_ACCOUNT_TYPE, option.GetAccountType());
        }
    }

    [Fact]
    public void testGetKontonummerListForRegisternummer()
    {
        List<Kontonummer>? options =
            KontonummerCalculator.GetKontonummerListForRegisternummer(TEST_REGISTERNUMMER, LIST_LENGTH);
        Assert.Equal(LIST_LENGTH, options.Count);
        foreach (Kontonummer? option in options)
        {
            Assert.True(KontonummerValidator.IsValid(option.ToString()));
            Assert.Equal(TEST_REGISTERNUMMER, option.GetRegisternummer());
        }
    }
}

using System.Text;
using NoCommons.Banking;
using NoCommons.Common;

namespace NoCommons.Tests.Banking;

public class KontonrValideringTests
{
    private const string KONTONUMMER_VALID = "99990000006";
    private const string KONTONUMMER_INVALID_CHECKSUM = "99990000005";

    [Fact]
    public void TestInvalidKontonummerWrongLength()
    {
        try
        {
            KontonummerValidator.ValidateSyntax("123456789012");
            Assert.True(false);
        }
        catch (ArgumentException e)
        {
            AssertionUtils.AssertMessageContains(e, StringNumberValidator.ERROR_SYNTAX);
        }
    }

    [Fact]
    public void TestInvalidKontonummerNotDigits()
    {
        try
        {
            KontonummerValidator.ValidateSyntax("abcdefghijk");
            Assert.True(false);
        }
        catch (ArgumentException e)
        {
            AssertionUtils.AssertMessageContains(e, StringNumberValidator.ERROR_SYNTAX);
        }
    }

    [Fact]
    public void TestInvalidKontonummerWrongChecksum()
    {
        try
        {
            KontonummerValidator.ValidateChecksum(KONTONUMMER_INVALID_CHECKSUM);
            Assert.True(false);
        }
        catch (ArgumentException e)
        {
            AssertionUtils.AssertMessageContains(e, StringNumberValidator.ERROR_INVALID_CHECKSUM);
        }
    }

    [Fact]
    public void TestInvalidAccountTypeWrongLength()
    {
        StringBuilder b = new(KontonummerValidator.ACCOUNTTYPE_NUM_DIGITS + 1);
        for (int i = 0; i < KontonummerValidator.ACCOUNTTYPE_NUM_DIGITS + 1; i++)
        {
            b.Append("0");
        }

        try
        {
            KontonummerValidator.ValidateAccountTypeSyntax(b.ToString());
            Assert.True(false);
        }
        catch (ArgumentException e)
        {
            AssertionUtils.AssertMessageContains(e, StringNumberValidator.ERROR_SYNTAX);
        }
    }

    [Fact]
    public void TestInvalidAccountTypeNotDigits()
    {
        StringBuilder b = new(KontonummerValidator.ACCOUNTTYPE_NUM_DIGITS);
        for (int i = 0; i < KontonummerValidator.ACCOUNTTYPE_NUM_DIGITS; i++)
        {
            b.Append("A");
        }

        try
        {
            KontonummerValidator.ValidateAccountTypeSyntax(b.ToString());
            Assert.True(false);
        }
        catch (ArgumentException e)
        {
            AssertionUtils.AssertMessageContains(e, StringNumberValidator.ERROR_SYNTAX);
        }
    }

    [Fact]
    public void TestInvalidRegisternummerNotDigits()
    {
        StringBuilder b = new(KontonummerValidator.REGISTERNUMMER_NUM_DIGITS);
        for (int i = 0; i < KontonummerValidator.REGISTERNUMMER_NUM_DIGITS; i++)
        {
            b.Append("A");
        }

        try
        {
            KontonummerValidator.ValidateRegisternummerSyntax(b.ToString());
            Assert.True(false);
        }
        catch (ArgumentException e)
        {
            AssertionUtils.AssertMessageContains(e, StringNumberValidator.ERROR_SYNTAX);
        }
    }

    [Fact]
    public void TestInvalidRegisternummerWrongLength()
    {
        StringBuilder b = new(KontonummerValidator.REGISTERNUMMER_NUM_DIGITS + 1);
        for (int i = 0; i < KontonummerValidator.REGISTERNUMMER_NUM_DIGITS + 1; i++)
        {
            b.Append("0");
        }

        try
        {
            KontonummerValidator.ValidateRegisternummerSyntax(b.ToString());
            Assert.True(false);
        }
        catch (ArgumentException e)
        {
            AssertionUtils.AssertMessageContains(e, StringNumberValidator.ERROR_SYNTAX);
        }
    }

    [Fact]
    public void TestGetValidKontonummerFromInvalidKontonummerWrongChecksum()
    {
        Kontonummer knr = KontonummerValidator.GetAndForceValidKontonummer(KONTONUMMER_INVALID_CHECKSUM);
        Assert.True(KontonummerValidator.IsValid(knr.ToString()));
    }

    [Fact]
    public void TestIsValid()
    {
        Assert.True(KontonummerValidator.IsValid(KONTONUMMER_VALID));
        Assert.False(KontonummerValidator.IsValid(KONTONUMMER_INVALID_CHECKSUM));
    }

    [Theory]
    [InlineData("97104133219")]
    [InlineData("97105302049")]
    [InlineData("97104008309")]
    [InlineData("97102749069")]
    public void TestValidNumberEndingOn9(string kontonrEndingOn9)
    {
        Assert.True(KontonummerValidator.IsValid(kontonrEndingOn9));
    }
}

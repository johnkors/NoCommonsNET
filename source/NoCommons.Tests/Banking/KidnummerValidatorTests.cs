using NoCommons.Banking;
using NoCommons.Common;

namespace NoCommons.Tests.Banking;

public class KidnummerValidatorTests
{
    private const string KIDNUMMER_VALID_MOD10 = "2345676";
    private const string KIDNUMMER_VALID_MOD11 = "12345678903";
    private const string KIDNUMMER_INVALID_CHECKSUM = "2345674";
    private const string KIDNUMMER_INVALID_LENGTH_SHORT = "1";
    private const string KIDNUMMER_INVALID_LENGTH_LONG = "12345678901234567890123456";

    [Fact]
    public void testInvalidKidnummer()
    {
        try
        {
            KidnummerValidator.ValidateSyntax("");
            Assert.True(false);
        }
        catch (ArgumentException e)
        {
            Assert.Contains(StringNumberValidator.ERROR_SYNTAX, e.Message);
        }
    }

    [Fact]
    public void testInvalidKidnummerNotDigits()
    {
        try
        {
            KidnummerValidator.ValidateSyntax("abcdefghijk");
            Assert.True(false);
        }
        catch (ArgumentException e)
        {
            Assert.Contains(StringNumberValidator.ERROR_SYNTAX, e.Message);
        }
    }

    [Fact]
    public void testInvalidKidnummerTooShort()
    {
        try
        {
            KidnummerValidator.ValidateSyntax(KIDNUMMER_INVALID_LENGTH_SHORT);
            Assert.True(false);
        }
        catch (ArgumentException e)
        {
            Assert.Contains(KidnummerValidator.ERROR_LENGTH, e.Message);
        }
    }

    [Fact]
    public void testInvalidKidnummerTooLong()
    {
        try
        {
            KidnummerValidator.ValidateSyntax(KIDNUMMER_INVALID_LENGTH_LONG);
            Assert.True(false);
        }
        catch (ArgumentException e)
        {
            Assert.Contains(KidnummerValidator.ERROR_LENGTH, e.Message);
        }
    }

    [Fact]
    public void testInvalidKidnummerWrongChecksum()
    {
        try
        {
            KidnummerValidator.ValidateChecksum(KIDNUMMER_INVALID_CHECKSUM);
            Assert.True(false);
        }
        catch (ArgumentException e)
        {
            Assert.Contains(StringNumberValidator.ERROR_INVALID_CHECKSUM, e.Message);
        }
    }

    [Fact]
    public void testIsValidMod10()
    {
        Assert.True(KidnummerValidator.IsValid(KIDNUMMER_VALID_MOD10));
    }

    [Fact]
    public void testIsValidMod11()
    {
        Assert.True(KidnummerValidator.IsValid(KIDNUMMER_VALID_MOD11));
    }

    [Fact]
    public void testIsInvalid()
    {
        Assert.False(KidnummerValidator.IsValid(KIDNUMMER_INVALID_CHECKSUM));
    }
}

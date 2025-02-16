using System.Globalization;
using NoCommons.Common;

namespace NoCommons.Person;

/**
* Provides methods that validates if a Fodselsnummer is valid with respect to
* syntax, Individnummer, Date and checksum digits.
*/
public class FodselsnummerValidator : StringNumberValidator
{
    private const int LENGTH = 11;

    private const string DATE_FORMAT = "ddMMyyyy";

    public const string ERROR_INVALID_DATE = "Invalid date in f�dselsnummer : ";

    public const string ERROR_INVALID_INDIVIDNUMMER = "Invalid individnummer in f�dselsnummer : ";

    private static readonly int[] K1_WEIGHTS = new[] { 2, 5, 4, 9, 8, 1, 6, 7, 3 };
    private static readonly int[] K2_WEIGHTS = new[] { 2, 3, 4, 5, 6, 7, 2, 3, 4, 5 };

    /**
     * Returns an object that represents a Fodselsnummer.
     * 
     * @param fodselsnummer
     * A string containing a Fodselsnummer
     * @return A Fodselsnummer instance
     * @throws ArgumentException
     * thrown if string contains an invalid Fodselsnummer
     */
    public static Fodselsnummer getFodselsnummer(string fodselsnummer)
    {
        ValidateSyntax(fodselsnummer);
        validateIndividnummer(fodselsnummer);
        validateDate(fodselsnummer);
        validateChecksums(fodselsnummer);
        return new Fodselsnummer(fodselsnummer);
    }

    /**
     * Return true if the provided string is a valid Fodselsnummer.
     * 
     * @param fodselsnummer
     * A string containing a Fodselsnummer
     * @return true or false
     */
    public static bool IsValid(string fodselsnummer)
    {
        try
        {
            getFodselsnummer(fodselsnummer);
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    public static void ValidateSyntax(string fodselsnummer)
    {
        ValidateLengthAndAllDigits(fodselsnummer, LENGTH);
    }

    public static void validateIndividnummer(string fodselsnummer)
    {
        Fodselsnummer fnr = new(fodselsnummer);
        if (fnr.GetCentury() == null)
        {
            throw new ArgumentException(ERROR_INVALID_INDIVIDNUMMER + fodselsnummer);
        }
    }

    public static void validateDate(string fodselsnummer)
    {
        Fodselsnummer fnr = new(fodselsnummer);
        try
        {
            string dateString = fnr.GetDateAndMonth() + fnr.GetCentury() + fnr.Get2DigitBirthYear();
            DateTime.ParseExact(dateString, DATE_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None);
        }
        catch (Exception)
        {
            throw new ArgumentException(ERROR_INVALID_DATE + fodselsnummer);
        }
    }

    public static void validateChecksums(string fodselsnummer)
    {
        Fodselsnummer fnr = new(fodselsnummer);
        int k1 = CalculateFirstChecksumDigit(fnr);
        int k2 = CalculateSecondChecksumDigit(fnr);
        if (k1 != fnr.GetChecksumDigit1() || k2 != fnr.GetChecksumDigit2())
        {
            throw new ArgumentException(ERROR_INVALID_CHECKSUM + fodselsnummer);
        }
    }

    internal static int CalculateFirstChecksumDigit(Fodselsnummer fodselsnummer)
    {
        return CalculateMod11CheckSum(K1_WEIGHTS, fodselsnummer);
    }

    internal static int CalculateSecondChecksumDigit(Fodselsnummer fodselsnummer)
    {
        return CalculateMod11CheckSum(K2_WEIGHTS, fodselsnummer);
    }
}

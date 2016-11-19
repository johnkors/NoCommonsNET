using System;
using System.Globalization;
using NoCommons.Common;

namespace NoCommons.Person
{
     /**
     * Provides methods that validates if a Fodselsnummer is valid with respect to
     * syntax, Individnummer, Date and checksum digits.
     */
    public class FodselsnummerValidator : StringNumberValidator {

        private const int LENGTH = 11;

        private const string DATE_FORMAT = "ddMMyyyy";

        private static readonly int[] K1_WEIGHTS = new [] { 2, 5, 4, 9, 8, 1, 6, 7, 3 };

        public const string ERROR_INVALID_DATE = "Invalid date in fødselsnummer : ";

        public const string ERROR_INVALID_INDIVIDNUMMER = "Invalid individnummer in fødselsnummer : ";

        /**
	     * Returns an object that represents a Fodselsnummer.
	     * 
	     * @param fodselsnummer
	     *            A String containing a Fodselsnummer
	     * @return A Fodselsnummer instance
	     * @throws ArgumentException
	     *             thrown if String contains an invalid Fodselsnummer
	     */
        public static Fodselsnummer getFodselsnummer(String fodselsnummer) {
            ValidateSyntax(fodselsnummer);
            validateIndividnummer(fodselsnummer);
            validateDate(fodselsnummer);
            validateChecksums(fodselsnummer);
            return new Fodselsnummer(fodselsnummer);
        }

        /**
	     * Return true if the provided String is a valid Fodselsnummer.
	     * 
	     * @param fodselsnummer
	     *            A String containing a Fodselsnummer
	     * @return true or false
	     */
        public static bool IsValid(String fodselsnummer) {
            try {
                getFodselsnummer(fodselsnummer);
                return true;
            } catch (ArgumentException) {
                return false;
            }
        }

        public static void ValidateSyntax(string fodselsnummer) {
            ValidateLengthAndAllDigits(fodselsnummer, LENGTH);
        }

        public static void validateIndividnummer(String fodselsnummer) {
            var fnr = new Fodselsnummer(fodselsnummer);
            if (fnr.getCentury() == null) {
                throw new ArgumentException(ERROR_INVALID_INDIVIDNUMMER + fodselsnummer);
            }
        }

        public static void validateDate(String fodselsnummer) {
            var fnr = new Fodselsnummer(fodselsnummer);
            try {
                var dateString = fnr.getDateAndMonth() + fnr.getCentury() + fnr.get2DigitBirthYear();
                DateTime.ParseExact(dateString, DATE_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None);
            } catch (Exception) {
                throw new ArgumentException(ERROR_INVALID_DATE + fodselsnummer);
            }
        }

        public static void validateChecksums(String fodselsnummer) {
            var fnr = new Fodselsnummer(fodselsnummer);
            int k1 = CalculateFirstChecksumDigit(fnr);
            int k2 = CalculateSecondChecksumDigit(fnr);
            if (k1 != fnr.getChecksumDigit1() || k2 != fnr.getChecksumDigit2()) {
                throw new ArgumentException(ERROR_INVALID_CHECKSUM + fodselsnummer);
            }
        }

        internal static int CalculateFirstChecksumDigit(Fodselsnummer fodselsnummer) {
            return CalculateMod11CheckSum(K1_WEIGHTS, fodselsnummer);
        }

        internal static int CalculateSecondChecksumDigit(Fodselsnummer fodselsnummer) {
            return CalculateMod11CheckSum(GetMod11Weights(fodselsnummer), fodselsnummer);
        }
    }
}
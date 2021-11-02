using System;
using NoCommons.Common;

namespace NoCommons.Org
{
    /**
     * Provides methods that validates if an Organisasjonsnummer is valid with
     * respect to syntax (length and digits only) and that the checksum digit is
     * correct.
     */
    public class OrganisasjonsnummerValidator : StringNumberValidator
    {
        private const int LENGTH = 9;

        /**
	     * Return true if the provided string is a valid Organisasjonsnummer.
	     * 
	     * @param organisasjonsnummer
	     *            A string containing a Organisasjonsnummer
	     * @return true or false
	     */
        public static bool IsValid(string organisasjonsnummer)
        {
            try
            {
                GetOrganisasjonsnummer(organisasjonsnummer);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        /**
	     * Returns an object that represents an Organisasjonsnummer.
	     * 
	     * @param organisasjonsnummer
	     *            A string containing an Organisasjonsnummer
	     * @return An Organisasjonsnummer instance
	     * @throws ArgumentException
	     *             thrown if string contains an invalid Organisasjonsnummer
	     */
        public static Organisasjonsnummer GetOrganisasjonsnummer(string organisasjonsnummer)
        {
            ValidateSyntax(organisasjonsnummer);
            ValidateChecksum(organisasjonsnummer);
            return new Organisasjonsnummer(organisasjonsnummer);
        }

        /**
	     * Returns an object that represents a Organisasjonsnummer. The checksum of
	     * the supplied organisasjonsnummer is changed to a valid checksum if the
	     * original organisasjonsnummer has an invalid checksum.
	     * 
	     * @param organisasjonsnummer
	     *            A string containing a Organisasjonsnummer
	     * @return A Organisasjonsnummer instance
	     * @throws ArgumentException
	     *             thrown if string contains an invalid Organisasjonsnummer, ie.
	     *             a number which for one cannot calculate a valid checksum.
	     */
        public static Organisasjonsnummer GetAndForceValidOrganisasjonsnummer(string organisasjonsnummer)
        {
            ValidateSyntax(organisasjonsnummer);
            try
            {
                ValidateChecksum(organisasjonsnummer);
            }
            catch (ArgumentException)
            {
                var onr = new Organisasjonsnummer(organisasjonsnummer);
                int checksum = CalculateMod11CheckSum(GetMod11Weights(onr), onr);
                organisasjonsnummer = organisasjonsnummer.Substring(0, LENGTH - 1) + checksum;
            }
            return new Organisasjonsnummer(organisasjonsnummer);
        }

        internal static void ValidateSyntax(string fodselsnummer)
        {
            ValidateLengthAndAllDigits(fodselsnummer, LENGTH);
        }

        internal static void ValidateChecksum(string organisasjonsnummer)
        {
            var onr = new Organisasjonsnummer(organisasjonsnummer);
            int k1 = CalculateMod11CheckSum(GetMod11Weights(onr), onr);
            if (k1 != onr.GetChecksumDigit())
            {
                throw new ArgumentException(ERROR_INVALID_CHECKSUM + organisasjonsnummer);
            }
        }
    }
}
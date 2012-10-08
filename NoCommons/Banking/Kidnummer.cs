using System;
using NoCommons.Common;

namespace NoCommons.Banking
{
    public class KidnummerValidator : StringNumberValidator {

	    public const string ERROR_LENGTH = "A Kidnummer is between 2 and 25 digits";

	    /**
	     * Return true if the provided String is a valid KID-nummmer.
	     * 
	     * @param kidnummer
	     *            A String containing a Kidnummer
	     * @return true or false
	     */
	    public static bool IsValid(String kidnummer) {
		    try {
			    getKidnummer(kidnummer);
			    return true;
		    } catch (ArgumentException) {
			    return false;
		    }
	    }

	    /**
	     * Returns an object that represents a Kidnummer.
	     * 
	     * @param kidnummer
	     *            A String containing a Kidnummer
	     * @return A Kidnummer instance
	     * @throws IllegalArgumentException
	     *             thrown if String contains an invalid Kidnummer
	     */
	    public static Kidnummer getKidnummer(string kidnummer) {
		    validateSyntax(kidnummer);
		    validateChecksum(kidnummer);
		    return new Kidnummer(kidnummer);
	    }

        public static void validateSyntax(string kidnummer) {
		    ValidateAllDigits(kidnummer);
		    validateLengthInRange(kidnummer, 2, 25);
	    }

	    private static void validateLengthInRange(string kidnummer, int i, int j) {
		    if (kidnummer == null || kidnummer.Length < i || kidnummer.Length > j) {
			    throw new ArgumentException(ERROR_LENGTH);
		    }
	    }

	    public static void validateChecksum(String kidnummer) {
		    StringNumber k = new Kidnummer(kidnummer);
		    int kMod10 = CalculateMod10CheckSum(GetMod10Weights(k), k);
		    int kMod11 = CalculateMod11CheckSum(GetMod11Weights(k), k);
		    if (kMod10 != k.GetChecksumDigit() && kMod11 != k.GetChecksumDigit()) {
			    throw new ArgumentException(ERROR_INVALID_CHECKSUM + kidnummer);
		    }
	    }

}









    /**
     * This class represent a Norwegian KID-nummer - a number used to identify 
     * a customer on invoices. A Kidnummer consists of digits only, and the last
     * digit is a checksum digit (either mod10 or mod11).
     */
    public class Kidnummer : StringNumber {

        public Kidnummer(string kontonummer) : base(kontonummer)
        {
        
        }
    }
}
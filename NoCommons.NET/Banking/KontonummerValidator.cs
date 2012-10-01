using System;
using NoCommons.Common;

namespace NoCommons.Banking
{
    public class KontonummerValidator : StringNumberValidator {

	    private const int LENGTH = 11;
        public static int ACCOUNTTYPE_NUM_DIGITS = 2;
        public static int REGISTERNUMMER_NUM_DIGITS = 4;
        
	    public static bool IsValid(string kontonummer) {
		    try {
			    GetKontonummer(kontonummer);
			    return true;
		    } catch (ArgumentException) {
			    return false;
		    }
	    }

	    public static Kontonummer GetKontonummer(string kontonummer){
		    ValidateSyntax(kontonummer);
		    ValidateChecksum(kontonummer);
		    return new Kontonummer(kontonummer);
	    }

	    public static Kontonummer GetAndForceValidKontonummer(string kontonummer) {
		    ValidateSyntax(kontonummer);
		    try {
			    ValidateChecksum(kontonummer);
		    } catch (ArgumentException) {
			    var k = new Kontonummer(kontonummer);
			    int checksum = CalculateMod11CheckSum(GetMod11Weights(k), k);
			    kontonummer = kontonummer.Substring(0, LENGTH - 1) + checksum;
		    }
		    return new Kontonummer(kontonummer);
	    }

        public static void ValidateSyntax(string kontonummer) {
		    ValidateLengthAndAllDigits(kontonummer, LENGTH);
	    }

        public static void ValidateAccountTypeSyntax(string accountType) {
		    ValidateLengthAndAllDigits(accountType, ACCOUNTTYPE_NUM_DIGITS);
	    }

        public static void ValidateRegisternummerSyntax(string registernummer) {
		    ValidateLengthAndAllDigits(registernummer, REGISTERNUMMER_NUM_DIGITS);
	    }

        public static void ValidateChecksum(string kontonummer) {
		    var k = new Kontonummer(kontonummer);
		    int k1 = CalculateMod11CheckSum(GetMod11Weights(k), k);
		    if (k1 != k.GetChecksumDigit()) {
			    throw new ArgumentException(ERROR_INVALID_CHECKSUM + kontonummer);
		    }
	    }

    }



}

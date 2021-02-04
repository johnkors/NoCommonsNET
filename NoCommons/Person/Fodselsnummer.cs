using System;
using NoCommons.Common;

namespace NoCommons.Person
{
    /**
     * This class represent a Norwegian social security number - a Fodselsnummer. A
     * Fodselsnummer consists of 11 digits, where the first 6 digits contains the
     * date of birth and the last 5 consists of an Individnummer (3 digits) and two
     * checksum digits.
     */
    public class Fodselsnummer : StringNumber {

        public Fodselsnummer(string fodselsnummer) : base(fodselsnummer) {
		
        }

        /**
	        * Returns the first 4 digits of the Fodselsnummer that contains the date
	        * (01-31) and month(01-12) of birth.
	        *
	        * @return A string containing the date and month of birth.
	        */
        public string GetDateAndMonth() {
	        return ParseDNumber(GetValue()).Substring(0, 4);
        }

        /**
	        * Returns the first 2 digits of the Fodselsnummer that contains the date
	        * (01-31), stripped for eventual d-numbers.
	        *
	        * @return A string containing the date of birth
	        */
        public string GetDayInMonth() {
	        return ParseDNumber(GetValue()).Substring(0, 2);
        }

        /**
	        * Returns the digits 3 and 4 of the Fodselsnummer that contains the month
	        * (01-12), stripped for eventual d-numbers.
	        *
	        * @return A string containing the date of birth
	        */
        public string GetMonth() {
	        return ParseDNumber(GetValue()).Substring(2, 2);
        }

        /**
	        * Returns the birthyear of the Fodselsnummer
	        *
	        * @return A string containing the year of birth.
	        */
        public string GetBirthYear() {
	        return GetCentury() + Get2DigitBirthYear();
        }

        public string GetCentury() {
	        string result = null;
	        int individnummerInt = int.Parse(GetIndividnummer());
	        int birthYear = int.Parse(Get2DigitBirthYear());
	        if (individnummerInt <= 499) {
		        result = "19";
	        } else if (individnummerInt >= 500 && birthYear < 40) {
		        result = "20";
	        } else if (individnummerInt >= 500 && individnummerInt <= 749 && birthYear >= 54) {
		        result = "18";
	        } else if (individnummerInt >= 900 && birthYear > 39) {
		        result = "19";
	        }
	        return result;
        }

        /**
	        * Returns the two digits of the Fodselsnummer that contains the year birth
	        * (00-99).
	        *
	        * @return A string containing the year of birth.
	        */
        public string Get2DigitBirthYear() {
	        return GetValue().Substring(4, 2);
        }

        /**
	        * Returns the first 6 digits of the Fodselsnummer that contains the date
	        * (01-31), month(01-12) and year(00-99) of birth.
	        *
	        * @return A string containing the date and month of birth.
	        */
        public string GetDateOfBirth() {
	        return ParseDNumber(GetValue()).Substring(0, 6);
        }

        /**
	        * Returns the last 5 digits of the Fodselsnummer, also referred to as the
	        * Personnummer. The Personnummer consists of the Individnummer (3 digits)
	        * and two checksum digits.
	        *
	        * @return A string containing the Personnummer.
	        */
        public string GetPersonnummer() {
	        return GetValue().Substring(6);
        }

        /**
	        * Returns the first three digits of the Personnummer, also known as the
	        * Individnummer.
	        *
	        * @return A string containing the Individnummer.
	        */
        public string GetIndividnummer() {
	        return GetValue().Substring(6, 3);
        }

        /**
	        * Returns the digit that decides the gender - the 9th in the Fodselsnummer.
	        *
	        * @return The digit.
	        */
        public int GetGenderDigit() {
	        return GetAt(8);
        }

        /**
	        * Returns the first checksum digit - the 10th in the Fodselsnummer.
	        *
	        * @return The digit.
	        */
        public int GetChecksumDigit1() {
	        return GetAt(9);
        }

        /**
	        * Returns the second checksum digit - the 11th in the Fodselsnummer.
	        *
	        * @return The digit.
	        */
        public int GetChecksumDigit2() {
	        return GetAt(10);
        }

        /**
	        * Returns true if the Fodselsnummer represents a man.
	        *
	        * @return true or false.
	        */
        public bool IsMale() {
	        return GetGenderDigit() % 2 != 0;
        }

        /**
	        * Returns true if the Fodselsnummer represents a woman.
	        *
	        * @return true or false.
	        */
        public bool IsFemale() {
	        return !IsMale();
        }

        public static bool IsDNumber(string fodselsnummer) {
	        try {
		        int firstDigit = GetFirstDigit(fodselsnummer);
		        if (firstDigit > 3 && firstDigit < 8) {
			        return true;
		        }
	        } catch (ArgumentException) {
		        // ignore
	        }
	        return false;
        }

        public static string ParseDNumber(string fodselsnummer) {
	        if (!IsDNumber(fodselsnummer)) {
		        return fodselsnummer;
	        } else {
		        return (GetFirstDigit(fodselsnummer) - 4) + fodselsnummer.Substring(1);
	        }
        }

        private static int GetFirstDigit(string fodselsnummer) {
	        return int.Parse(fodselsnummer.Substring(0, 1));
        }

        public KJONN GetKjonn() {
	        if (IsFemale()) {
		        return KJONN.KVINNE;
	        } else {
		        return KJONN.MANN;
	        }
        }
        
        public override string ToString(){
	        return GetValue();
        }
    }


   
}

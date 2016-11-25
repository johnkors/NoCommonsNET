using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoCommons.Person
{
    /**
     * This class calculates valid Fodselsnummer instances for a given date.
     */
    public class FodselsnummerCalculator
    {

        /**
         * Returns a List with valid Fodselsnummer instances for a given Date and gender.
         */
        public static List<Fodselsnummer> getFodselsnummerForDateAndGender(DateTime date, KJONN kjonn)
        {
            List<Fodselsnummer> result = getManyFodselsnummerForDate(date);
            result = splitByGender(kjonn, result);
            return result;
        }

        /**
         * Return one random valid fodselsnummer on a given date
         */
        public static Fodselsnummer getFodselsnummerForDate(DateTime date)
        {
            List<Fodselsnummer> fodselsnummerList = getManyFodselsnummerForDate(date);
            //Collections.shuffle(fodselsnummerList);
            return fodselsnummerList[0];
        }

        /**
         * Returns a List with with VALID Fodselsnummer instances for a given Date.
         *
         * @param date The Date instance
         * @return A List with Fodelsnummer instances
         */
        public static List<Fodselsnummer> getManyFodselsnummerForDate(DateTime date)
        {
            if (date == null)
            {
                throw new ArgumentException();
            }
            var century = getCentury(date);
            var dateString = date.ToString("ddMMyy");
            var result = new List<Fodselsnummer>();
            for (int i = 999; i >= 0; i--)
            {
                var sb = new StringBuilder(dateString);
                if (i < 10)
                {
                    sb.Append("00");
                }
                else if (i < 100)
                {
                    sb.Append("0");
                }
                sb.Append(i);
                var fodselsnummer = new Fodselsnummer(sb.ToString());
                try
                {
                    sb.Append(FodselsnummerValidator.CalculateFirstChecksumDigit(fodselsnummer));
                    fodselsnummer = new Fodselsnummer(sb.ToString());

                    sb.Append(FodselsnummerValidator.CalculateSecondChecksumDigit(fodselsnummer));
                    fodselsnummer = new Fodselsnummer(sb.ToString());

                    var centuryByIndividnummer = fodselsnummer.getCentury();
                    if (centuryByIndividnummer != null && centuryByIndividnummer.Equals(century) && FodselsnummerValidator.IsValid(fodselsnummer.GetValue()))
                    {
                        result.Add(fodselsnummer);
                    }
                }
                catch (ArgumentException)
                {
                    continue;
                }
            }
            return result;
        }

        private static String getCentury(DateTime date)
        {
            return Convert.ToString(date.Year).Substring(0, 2);
        }

        private static List<Fodselsnummer> splitByGender(KJONN kjonn, List<Fodselsnummer> result)
        {
            return (from f in result where f.getKjonn() == kjonn select f).ToList();
            
        }

    }
}
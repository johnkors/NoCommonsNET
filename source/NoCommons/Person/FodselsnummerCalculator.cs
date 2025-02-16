using System.Text;

namespace NoCommons.Person;

/**
 * This class calculates valid Fodselsnummer instances for a given date.
 */
public class FodselsnummerCalculator
{
    /**
     * Returns a List with valid Fodselsnummer instances for a given Date and gender.
     */
    public static List<Fodselsnummer> GetFodselsnummerForDateAndGender(DateTime date, KJONN kjonn)
    {
        List<Fodselsnummer> result = GetManyFodselsnummerForDate(date);
        result = SplitByGender(kjonn, result);
        return result;
    }

    /**
     * Return one random valid fodselsnummer on a given date
     */
    public static Fodselsnummer GetFodselsnummerForDate(DateTime date)
    {
        List<Fodselsnummer> fodselsnummerList = GetManyFodselsnummerForDate(date);
        //Collections.shuffle(fodselsnummerList);
        return fodselsnummerList[0];
    }

    /**
     * Returns a List with with VALID Fodselsnummer instances for a given Date.
     *
     * @param date The Date instance
     * @return A List with Fodelsnummer instances
     */
    public static List<Fodselsnummer> GetManyFodselsnummerForDate(DateTime date, bool dNumber = false)
    {
        string century = GetCentury(date);

        string dateString = date.ToString("ddMMyy");
        if (dNumber)
        {
            int day = date.Day + 40;
            dateString = $"{day}{date.ToString("MMyy")}";
        }

        List<Fodselsnummer> result = new();
        for (int i = 999; i >= 0; i--)
        {
            StringBuilder sb = new(dateString);
            if (i < 10)
            {
                sb.Append("00");
            }
            else if (i < 100)
            {
                sb.Append("0");
            }

            sb.Append(i);
            Fodselsnummer fodselsnummer = new(sb.ToString());
            try
            {
                sb.Append(FodselsnummerValidator.CalculateFirstChecksumDigit(fodselsnummer));
                fodselsnummer = new Fodselsnummer(sb.ToString());

                sb.Append(FodselsnummerValidator.CalculateSecondChecksumDigit(fodselsnummer));
                fodselsnummer = new Fodselsnummer(sb.ToString());

                string? centuryByIndividnummer = fodselsnummer.GetCentury();
                if (centuryByIndividnummer != null && centuryByIndividnummer.Equals(century) &&
                    FodselsnummerValidator.IsValid(fodselsnummer.GetValue()))
                {
                    result.Add(fodselsnummer);
                }
            }
            catch (ArgumentException)
            {
            }
        }

        return result;
    }

    private static string GetCentury(DateTime date)
    {
        return Convert.ToString(date.Year).Substring(0, 2);
    }

    private static List<Fodselsnummer> SplitByGender(KJONN kjonn, List<Fodselsnummer> result)
    {
        return (from f in result where f.GetKjonn() == kjonn select f).ToList();
    }
}

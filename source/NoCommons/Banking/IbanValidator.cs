using System.Text;
using System.Text.RegularExpressions;

namespace NoCommons.Banking;

public static class IbanValidator
{
    public static bool IsValid(string ibanValue)
    {
        try
        {
            return Validate(ibanValue);
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static bool Validate(string ibanValue)
    {
        if (Regex.IsMatch(ibanValue, "^[A-Z0-9]"))
        {
            string ibanLeftShiftedBy4 = ibanValue.Substring(4, ibanValue.Length - 4) + ibanValue.Substring(0, 4);
            string checkSumString = CheckSumString(ibanLeftShiftedBy4);
            int checksum = int.Parse(checkSumString.Substring(0, 1));
            return HasCorrectChecksum(checksum, checkSumString);
        }

        return false;
    }

    private static bool HasCorrectChecksum(int checksum, string checkSumString)
    {
        for (int i = 1; i < checkSumString.Length; i++)
        {
            int v = int.Parse(checkSumString.Substring(i, 1));
            checksum *= 10;
            checksum += v;
            checksum %= 97;
        }

        return checksum == 1;
    }

    private static string CheckSumString(string ibanLeftShiftedBy4)
    {
        const int asciiShift = 55;
        StringBuilder sb = new();
        foreach (char c in ibanLeftShiftedBy4)
        {
            int v;
            if (char.IsLetter(c))
            {
                v = c - asciiShift;
            }
            else
            {
                v = int.Parse(c.ToString());
            }

            sb.Append(v);
        }

        string checkSumString = sb.ToString();
        return checkSumString;
    }
}

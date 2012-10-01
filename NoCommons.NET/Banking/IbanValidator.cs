using System;
using System.Globalization;
using System.Text;

namespace NoCommons.Banking
{
    public class IbanValidator
    {
        public static bool IsValid(string ibanValue)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(ibanValue, "^[A-Z0-9]"))
            {
                ibanValue = ibanValue.Replace(" ", string.Empty);
                string iban = ibanValue.Substring(4, ibanValue.Length - 4) + ibanValue.Substring(0, 4);
                const int asciiShift = 55;
                var sb = new StringBuilder();
                foreach (char c in iban)
                {
                    int v;
                    if (Char.IsLetter(c))
                    {
                        v = c - asciiShift;
                    }
                    else
                    {
                        v = int.Parse(c.ToString(CultureInfo.InvariantCulture));
                    }
                    sb.Append(v);
                }
                string checkSumString = sb.ToString();
                int checksum = int.Parse(checkSumString.Substring(0, 1));
                for (int i = 1; i < checkSumString.Length; i++)
                {
                    int v = int.Parse(checkSumString.Substring(i, 1));
                    checksum *= 10;
                    checksum += v;
                    checksum %= 97;
                }
                return checksum == 1;
            }
            return false;
        }
    }
}

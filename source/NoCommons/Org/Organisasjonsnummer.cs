using NoCommons.Common;

namespace NoCommons.Org
{
    /**
     * This class represent a Norwegian organization number - an
     * Organisasjonsnummer. An Organisasjonsnummer consists of 9 digits, and the
     * last digit is a checksum digit.
     */
    public class Organisasjonsnummer : StringNumber
    {
        internal Organisasjonsnummer(string organisasjonsnummer) : base(organisasjonsnummer)
        {
        }
    }
}
using NoCommons.Common;

namespace NoCommons.Banking
{
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
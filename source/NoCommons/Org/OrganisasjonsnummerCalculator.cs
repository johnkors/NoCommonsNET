using System.Text;

namespace NoCommons.Org;

/**
 * This class calculates valid Organisasjonsnummer instances.
 */
public class OrganisasjonsnummerCalculator
{
    private const int LENGTH = 9;

    private OrganisasjonsnummerCalculator()
    {
    }

    /**
     * Returns a List with completely random but syntactically valid
     * Organisasjonsnummer instances.
     * 
     * @param length
     * Specifies the number of Organisasjonsnummer instances to
     * create in the returned List
     * 
     * @return A List with random valid Organisasjonsnummer instances
     */
    public static List<Organisasjonsnummer> GetOrganisasjonsnummerList(int length)
    {
        List<Organisasjonsnummer> result = new();
        int numAddedToList = 0;
        while (numAddedToList < length)
        {
            StringBuilder orgnrBuffer = new(LENGTH);
            for (int i = 0; i < LENGTH; i++)
            {
                Random rand = new();
                int rand10 = rand.Next(0, 10);
                orgnrBuffer.Append(rand10);
            }

            Organisasjonsnummer orgNr;
            try
            {
                orgNr = OrganisasjonsnummerValidator.GetAndForceValidOrganisasjonsnummer(orgnrBuffer.ToString());
            }
            catch (ArgumentException)
            {
                // this number has no valid checksum
                continue;
            }

            result.Add(orgNr);
            numAddedToList++;
        }

        return result;
    }
}

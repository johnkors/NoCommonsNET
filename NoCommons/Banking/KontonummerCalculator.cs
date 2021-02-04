using System;
using System.Collections.Generic;
using System.Text;

namespace NoCommons.Banking
{
    /**
     * This class calculates valid Kontonummer instances.
     */
    public class KontonummerCalculator
    {
        private static List<Kontonummer> GetKontonummerListUsingGenerator(KontonummerDigitGenerator generator, int length)
        {
            var result = new List<Kontonummer>();
            int numAddedToList = 0;
            while (numAddedToList < length)
            {
                Kontonummer kontoNr;
                try
                {
                    kontoNr = KontonummerValidator.GetAndForceValidKontonummer(generator.GenerateKontonummer());
                }
                catch (ArgumentException)
                {
                    // this number has no valid checksum
                    continue;
                }
                result.Add(kontoNr);
                numAddedToList++;
            }
            return result;
        }

        /**
	     * Returns a List with random but syntactically valid Kontonummer instances
	     * for a given AccountType.
	     * 
	     * @param accountType
	     *            A string representing the AccountType to use for all
	     *            Kontonummer instances
	     * @param length
	     *            Specifies the number of Kontonummer instances to create in the
	     *            returned List
	     * @return A List with Kontonummer instances
	     */
        public static List<Kontonummer> GetKontonummerListForAccountType(string accountType, int length)
        {
            KontonummerValidator.ValidateAccountTypeSyntax(accountType);

            return GetKontonummerListUsingGenerator(new AccountTypeKontonrDigitGenerator(accountType), length);
        }

        /**
	     * Returns a List with random but syntactically valid Kontonummer instances
	     * for a given Registernummer.
	     * 
	     * @param registernummer
	     *            A string representing the Registernummer to use for all
	     *            Kontonummer instances
	     * @param length
	     *            Specifies the number of Kontonummer instances to create in the
	     *            returned List
	     * @return A List with Kontonummer instances
	     */
        public static List<Kontonummer> GetKontonummerListForRegisternummer(string registernummer, int length)
        {
            KontonummerValidator.ValidateRegisternummerSyntax(registernummer);

            return GetKontonummerListUsingGenerator(new RegisternummerKontonrDigitGenerator(registernummer), length);
        }

        /**
	     * Returns a List with completely random but syntactically valid Kontonummer
	     * instances.
	     * 
	     * @param length
	     *            Specifies the number of Kontonummer instances to create in the
	     *            returned List
	     * 
	     * @return A List with random valid Kontonummer instances
	     */
        public static List<Kontonummer> GetKontonummerList(int length)
        {
            return GetKontonummerListUsingGenerator(new NormalKontonrDigitGenerator(), length);
        }
    }

    internal abstract class KontonummerDigitGenerator
    {
        protected const int REGISTERNUMMER_START_DIGIT = 0;
        protected const int LENGTH = 11;
        internal abstract string GenerateKontonummer();
    }

    internal class AccountTypeKontonrDigitGenerator : KontonummerDigitGenerator
    {
        private const int ACCOUNTTYPE_START_DIGIT = 4;

        private readonly string _accountType;

        internal AccountTypeKontonrDigitGenerator(string accountType)
        {
            _accountType = accountType;
        }

        internal override string GenerateKontonummer()
        {
            var kontonrBuffer = new StringBuilder(LENGTH);
            for (var i = 0; i < LENGTH; )
            {
                if (i == ACCOUNTTYPE_START_DIGIT)
                {
                    kontonrBuffer.Append(_accountType);
                    i += _accountType.Length;
                }
                else
                {
                    var randomNum = new Random();
                    var ran = randomNum.Next(0, 10);
                    kontonrBuffer.Append(ran);
                    i++;
                }
            }
            return kontonrBuffer.ToString();
        }
    }

    internal class RegisternummerKontonrDigitGenerator : KontonummerDigitGenerator
    {
        private readonly string _registerNr;

        internal RegisternummerKontonrDigitGenerator(string registerNr)
        {
            _registerNr = registerNr;
        }


        internal override string GenerateKontonummer()
        {
            var kontonrBuffer = new StringBuilder(LENGTH);
            for (int i = 0; i < LENGTH; )
            {
                if (i == REGISTERNUMMER_START_DIGIT)
                {
                    kontonrBuffer.Append(_registerNr);
                    i += _registerNr.Length;
                }
                else
                {
                    var rand = new Random();
                    var ran = rand.Next(0, 10);
                    kontonrBuffer.Append(ran);
                    i++;
                }
            }
            return kontonrBuffer.ToString();
        }
    }

    internal class NormalKontonrDigitGenerator : KontonummerDigitGenerator
    {
        internal override string GenerateKontonummer()
        {
            var kontonrBuffer = new StringBuilder(LENGTH);
            for (int i = 0; i < LENGTH; i++)
            {
                var random = new Random();
                var ran = random.Next(0, 10);
                kontonrBuffer.Append(ran);
            }
            return kontonrBuffer.ToString();
        }
    }

}
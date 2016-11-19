using System;
using System.Linq;

namespace NoCommons.Common
{
    public abstract class StringNumberValidator {

       public static string ERROR_INVALID_CHECKSUM = "Invalid checksum : ";

       public static string ERROR_SYNTAX = "Only digits are allowed : ";

       private static readonly int[] BASE_MOD11_WEIGHTS = new []{2, 3, 4, 5, 6, 7};
        
       protected static int CalculateMod11CheckSum(int[] weights, StringNumber number) {
          int c = CalculateChecksum(weights, number, false) % 11;
          if (c == 1) {
             throw new ArgumentException(ERROR_INVALID_CHECKSUM + number);
          }
          return c == 0 ? 0 : 11 - c;
       }
    
       protected static int CalculateMod10CheckSum(int[] weights, StringNumber number) {
          int c = CalculateChecksum(weights, number, true) % 10;
          return c == 0 ? 0 : 10 - c;
       }

       private static int CalculateChecksum(int[] weights, StringNumber number, bool tverrsum) {
          int checkSum = 0;
          for (int i = 0; i < weights.Length; i++) {
             int product = weights[i] * number.GetAt(weights.Length - 1 - i);
             if (tverrsum) {
                checkSum += (product > 9 ? product - 9 : product);
             } else {
                checkSum += product;
             }
          }
          return checkSum;
       }

       protected static void ValidateLengthAndAllDigits(string numberString,
                                                        int length) {
          if (numberString == null || numberString.Length != length) {
             throw new ArgumentException(ERROR_SYNTAX + numberString);
          }
          ValidateAllDigits(numberString);
       }

       protected static void ValidateAllDigits(string numberString) {
           if (numberString == null || numberString.Length <= 0) {
             throw new ArgumentException(ERROR_SYNTAX + numberString);
          }
           if (numberString.Any(t => !char.IsDigit((t)))) {
               throw new ArgumentException(ERROR_SYNTAX + numberString);
           }
       }

        protected static int[] GetMod10Weights(StringNumber k) {
          var weights = new int[k.GetLength() - 1];
          for (int i = 0; i < weights.Length; i++) {
             if ((i % 2) == 0) {
                weights[i] = 2;
             } else {
                weights[i] = 1;
             }
          }
          return weights;
       }

       protected static int[] GetMod11Weights(StringNumber k) {
          var weights = new int[k.GetLength() - 1];
          for (int i = 0; i < weights.Length; i++) {
             int j = i % BASE_MOD11_WEIGHTS.Length;
             weights[i] = BASE_MOD11_WEIGHTS[j];
          }
          return weights;
       }

    }
}

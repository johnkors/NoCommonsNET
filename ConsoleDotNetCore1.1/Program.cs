using System;
using NoCommons.Person;

namespace ConsoleDotNetCore1._1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hi, this is a fodselsnr: " + FodselsnummerValidator.IsValid("12312312"));
            Console.ReadKey();
        }
    }
}

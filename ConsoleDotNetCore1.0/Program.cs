using System;
using System.Linq;

namespace ConsoleDotNetCore1._0
{
    public class Program
    {
        public static void Main(string[] args)
        {
			Console.WriteLine("Hi, this is a fodselsnr: " + "12312312".Any(c => c == '1'));
	        Console.ReadKey();
        }
    }
}

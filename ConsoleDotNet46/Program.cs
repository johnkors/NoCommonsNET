using System;
using NoCommons.Person;

namespace ConsoleDotNet46
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hi, this is a fodselsnr: " + FodselsnummerValidator.IsValid("12312312"));
			Console.ReadKey();
		}
	}
}

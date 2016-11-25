using System;
using NoCommons.Person;

namespace ConsoleDotNet45
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hi, this is a fodselsnr: " + FodselsnummerValidator.IsValid("123"));
			Console.ReadKey();
		}
	}
}

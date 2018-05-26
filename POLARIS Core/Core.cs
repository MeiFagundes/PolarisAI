using System;
using System.IO;
using System.Text;

namespace POLARIS_Core
{
    class Core
    {

		static String[] verbs = File.ReadAllLines(@"C:\Users\Mei\OneDrive\1. Code\GitHub\Project-POLARIS/verbs.txt", Encoding.UTF8);
		static String[] pronouns = File.ReadAllLines(@"C:\Users\Mei\OneDrive\1. Code\GitHub\Project-POLARIS/pronouns.txt", Encoding.UTF8);
		static String[] inputArr;

		static void Main(string[] args)
        {
			String input = Console.ReadLine();
			input = input.ToLower();
			inputArr = input.Split(" ");

			// DEBUG
			for (int i = 0; i < inputArr.Length; i++)
			{
				Console.Write(inputArr[i] + " ");
			}
			
			Console.WriteLine();
			Boolean isQuestion = CheckQuestion();
			Console.WriteLine("It's a Question? : " + isQuestion);
			Console.WriteLine("# of verbs: " + verbs.Length + "\n");
			Console.WriteLine("# of pronouns: " + pronouns.Length + "\n");
			System.Threading.Thread.Sleep(60000);
        }

		private static Boolean CheckQuestion()
		{
			int verbIndex = -1, pronounIndex = -1;

			for (int i = 0; i < inputArr.Length; i++)
			{
				for (int j = 0; j < verbs.Length; j++)
				{
					if (inputArr[i] == verbs[j] && verbIndex == -1)
					{
						verbIndex = i;
					}
				}
				for (int j = 0; j < pronouns.Length; j++)
				{
					if (inputArr[i] == pronouns[j] && pronounIndex == -1)
					{
						pronounIndex = i;
					}
				}
			}

			// DEBUG
			Console.WriteLine("verbIndex: " + verbIndex + "\npronounIndex: " + pronounIndex);

			if (verbIndex < pronounIndex && verbIndex != -1 && pronounIndex != -1)
			{
				return true;
			}
			else
				return false;
		}
    }
}

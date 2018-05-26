using System;
using System.IO;
using System.Text;

namespace POLARIS_Core
{
    class Core
    {
		// Storing library of assets
		static String[] verbs = File.ReadAllLines(@"C:\Users\Mei\OneDrive\1. Code\GitHub\Project-POLARIS/verbs.txt", Encoding.UTF8);
		static String[] pronouns = File.ReadAllLines(@"C:\Users\Mei\OneDrive\1. Code\GitHub\Project-POLARIS/pronouns.txt", Encoding.UTF8);
		static String[] inputArr;

		static void Main(string[] args)
        {
			// Preparing String[]
			Console.WriteLine("Type something: ");
			String input = Console.ReadLine();
			input = input.ToLower();
			inputArr = input.Split(" ");

			// DEBUG
			Boolean isQuestion = CheckQuestion();
			Console.WriteLine("It's a Question? : " + isQuestion);

			System.Threading.Thread.Sleep(60000);
        }

		private static Boolean CheckQuestion()
		{
			int verbIndex = -1, pronounIndex = -1, knowIndex = -1;
			Boolean isThereAKnow = false;

			for (int i = 0; i < inputArr.Length; i++)
			{

				// Storing index of the first Verb
				for (int j = 0; j < verbs.Length; j++)
				{
					if (inputArr[i] == verbs[j] && verbIndex == -1)
					{
						verbIndex = i;
					}
				}

				// Storing index of the first Pronoun
				for (int j = 0; j < pronouns.Length; j++)
				{
					if (inputArr[i] == pronouns[j] && pronounIndex == -1)
					{
						pronounIndex = i;
					}
				}

				// Checking if there's a "know"
				if (inputArr[i] == "know")
				{
					isThereAKnow = true;
					knowIndex = i;
				}
			}

			// isQuestion if the last Char is a "?"
			if (inputArr[inputArr.Length - 1].Substring(inputArr[inputArr.Length - 1].Length - 1) == "?")
				return true;

			if ((verbIndex < pronounIndex || // isQuestion if the Pronoun is after the Verb. || Example: "Do you know if..."
				(isThereAKnow && verbIndex > pronounIndex && knowIndex > verbIndex)) // isQuestion if there's a (Pronoun + Verb + "know") || Example: "I Want to know if..."
				&& verbIndex != -1 && pronounIndex != -1) // There has to be a Verb and a Pronoun to be a question in those cases
				return true;
			else
				return false;
		}
    }
}

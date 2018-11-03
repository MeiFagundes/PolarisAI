using System;

namespace POLARIS.Skills
{
	public static class Debug{
		public static void Execute(Dialog dialog) {

			switch (dialog.Phrase[dialog.SkillsIndex[0] + 1]){

				case "cognition":
					dialog.Debug();
					break;

				case "vocabulary":
					dialog.vocabulary.Debug();
					break;

				default:
					Console.WriteLine("Sorry, there's no Debug argument named '"
                        + dialog.Phrase[dialog.SkillsIndex[0] + 1] + "', maybe you misspelled it.");
					break;
			}
		}
	}
}

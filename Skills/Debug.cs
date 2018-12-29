using System;

namespace PolarisCore.Skills
{
	public static class Debug{
		public static void Execute(Dialog d) {

			switch (d.Phrase[d.SkillsIndex[0] + 1]){

				case "cognition":
					d.Debug();
					break;

				case "vocabulary":
					d.vocabulary.Debug();
					break;

				default:
					Console.WriteLine("Sorry, there's no Debug argument named '"
                        + d.Phrase[d.SkillsIndex[0] + 1] + "', maybe you misspelled it.");
					break;
			}
		}
	}
}

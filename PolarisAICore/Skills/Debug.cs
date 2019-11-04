using System;

namespace PolarisAICore.Skills
{
	public static class Debug{
		public static void Execute(Utterance d) {

			switch (d.Phrase[d.SkillsIndex[0] + 1]){

				case "cognition":
					d.GetDebugLog();
					break;

				case "vocabulary":
					d.Vocabulary.PrintDebugLog();
					break;

				default:
					Console.WriteLine("Sorry, there's no Debug argument named '"
                        + d.Phrase[d.SkillsIndex[0] + 1] + "', maybe you misspelled it.");
					break;
			}
		}
	}
}

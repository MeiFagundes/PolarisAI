using System;
using System.Threading.Tasks;

namespace POLARIS {
	class PolarisCore {
		public static void Main(string[] args) {

			Dialog dialog;
			Vocabulary vocabulary = new Vocabulary();
			String input = "Polaris, search this please.";

			while (true) { 
				
				Console.WriteLine("Input: '" + input + "'");
				dialog = new Dialog(input, vocabulary);
				MainPipeline(dialog);
				vocabulary.Debug();
				dialog.Debug();

				Console.WriteLine("Type something: ");
				input = Console.ReadLine();
				Console.Clear();
			}
		}

		public static void MainPipeline(Dialog dialog) {

			Task cognitionCoreTask = new Task(() => Cognition.CognitionCore.Fetch(dialog));
			cognitionCoreTask.RunSynchronously();

			if (!dialog.IsSkillsEmpty) {
				Task skillsCoreTask = new Task(() => Skills.SkillsCore.FetchSkill(dialog));
				skillsCoreTask.RunSynchronously();
			}
		}
	}
}

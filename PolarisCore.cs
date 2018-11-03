using System;
using System.Threading.Tasks;

namespace POLARIS {
	class PolarisCore {
		public static void Main(string[] args) {

			Dialog dialog;
			Vocabulary vocabulary = new Vocabulary();
			String input = "Polaris, can you run Crysis?";

			while (true) { 
				
				Console.WriteLine("Input: '" + input + "'");
				dialog = new Dialog(input, vocabulary);

                Task.Run(() => {
                    MainPipeline(dialog);
                    dialog.Debug();

                }).ContinueWith(t => {

                    Console.WriteLine("Hint! Try asking: 'Search about cute kittens please'");
                    Console.Write("\n>");

                });
                
				input = Console.ReadLine();
				Console.Clear();
			}
		}

		public static void MainPipeline(Dialog dialog) {

			Task cognitionCoreTask = new Task(() => Cognition.CognitionCore.FetchCognition(dialog));
			cognitionCoreTask.RunSynchronously();
			cognitionCoreTask.Wait();

			if (!dialog.IsSkillsEmpty) {
				Task skillsCoreTask = new Task(() => Skills.SkillsCore.FetchSkill(dialog));
				skillsCoreTask.RunSynchronously();
            }
		}
	}
}

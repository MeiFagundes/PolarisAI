using PolarisCore.Vocabulary;
using System;
using System.Threading.Tasks;

namespace PolarisCore {
	class PolarisCore {
		public static void Main(string[] args) {
            
			VocabularyModel vocabulary = new VocabularyModel();
            Dialog dialog;

            if (args == null || args.Length == 0) {

                String input = "Polaris, how are you right now?";

                while (true) {
                    // Test Mode
                    
                    Console.WriteLine("Input: '" + input + "'");
                    Console.WriteLine("\nHint! Try asking: 'Search about cute kittens please'");
                    dialog = new Dialog(input, vocabulary);

                    Task.Run(() => {
                        MainPipeline(dialog);
                        dialog.Debug();
                        Console.WriteLine("Response (Alpha): " + dialog.Response);

                    }).ContinueWith(t => {

                        Console.Write("\n>");

                    });
                    
                    input = Console.ReadLine();
                    Console.Clear();
                }
            }
            else {
                // Server Mode

                dialog = new Dialog(args[0], vocabulary);
                MainPipeline(dialog);
                Console.Write(dialog.ToJson());
            }
		}

		public static void MainPipeline(Dialog dialog) {

            // Executing Cognition pipeline
			Task cognitionCoreTask = new Task(() => Cognitions.CognitionsController.FetchCognition(dialog));
			cognitionCoreTask.RunSynchronously();
			cognitionCoreTask.Wait();

            // Try and executing Skills
            if (!dialog.IsSkillsEmpty) {
				Task skillsCoreTask = new Task(() => Skills.SkillsController.FetchSkill(dialog));
				skillsCoreTask.RunSynchronously();
            }

            // Generating a response
            Task responseCoreTask = new Task(() => Responses.ResponseController.GenerateResponse(dialog));
            responseCoreTask.RunSynchronously();
        }
	}
}

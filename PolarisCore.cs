using System;
using System.Threading.Tasks;

namespace PolarisCore {
	class PolarisCore {
		public static void Main(string[] args) {
            
			Vocabulary vocabulary = new Vocabulary();
            Dialog dialog;

            if (args == null || args.Length == 0) {

                String input = "Polaris, can you run Crysis?";

                while (true) {
                    // Test Mode
                    
                    Console.WriteLine("Input: '" + input + "'");
                    Console.WriteLine("\nHint! Try asking: 'Search about cute kittens please'");
                    dialog = new Dialog(input, vocabulary);

                    Task.Run(() => {
                        MainPipeline(dialog);
                        dialog.Debug();
                        Console.WriteLine("Response: " + dialog.Response);

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
			Task cognitionCoreTask = new Task(() => Cognition.CognitionCore.FetchCognition(dialog));
			cognitionCoreTask.RunSynchronously();
			cognitionCoreTask.Wait();

            // Try and executing Skills
            if (!dialog.IsSkillsEmpty) {
				Task skillsCoreTask = new Task(() => Skills.SkillsCore.FetchSkill(dialog));
				skillsCoreTask.RunSynchronously();
            }

            // Generating a response
            Task responseCoreTask = new Task(() => Response.ResponseCore.GenerateResponse(dialog));
            responseCoreTask.RunSynchronously();
        }
	}
}

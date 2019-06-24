using PolarisAICore.Vocabulary;
using System;
using System.Threading.Tasks;

namespace PolarisAICore {
	public class Main {
		public static String Cognize(String query, bool debugMode) {
            
			VocabularyModel vocabulary = new VocabularyModel();
            Dialog dialog;

            if (debugMode) {

                // Debug Mode
                String debugInfo = "";

                debugInfo += "Input: '" + query + "'\n";
                debugInfo += "\nHint! Try asking: 'Search about cute kittens please'\n";
                dialog = new Dialog(query, vocabulary);

                MainPipeline(dialog);
                debugInfo += dialog.GetDebugInfo();
                debugInfo += "Response (Alpha): " + dialog.Response + "\n";
                debugInfo += "\n >";
                return debugInfo;

            }
            else {
                // Server Mode

                dialog = new Dialog(query, vocabulary);
                MainPipeline(dialog);
                return dialog.ToJson();
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

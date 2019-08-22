using PolarisAICore.Vocabulary;
using System;
using System.Threading.Tasks;
using Starlight;
using System.IO;

namespace PolarisAICore {
	public class PolarisAICore {

        static void Main(string[] args) {
            Console.WriteLine(CognizeML("test"));
        }

        public static String CognizeML(String query) {

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            Console.SetError(stringWriter);

            ClassificationController cc = new ClassificationController();

            Console.WriteLine("Enter a phrase:");
            cc.Cognize(query);

            StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);

            return stringWriter.ToString();
        }

		public static String CognizeLegacy(String query, bool debugMode) {

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

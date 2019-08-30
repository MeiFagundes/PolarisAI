using PolarisAICore.Vocabulary;
using System;
using System.Threading.Tasks;
using Starlight;
using System.IO;

namespace PolarisAICore {
	public class PolarisAICore {

        static void Main(string[] args) {

            Console.WriteLine(Cognize(Console.ReadLine(), true));
        }

        public static String Cognize(String query, bool debug = false){

            if (debug) {
                Console.WriteLine("Enter a phrase:");
                Console.Write("> ");
            }
            String output = CognizeLegacy(query, debug);
            output += CognizeML(query);
            return output;
        }

        public static String CognizeML(String query) {

            StringWriter stringWriter = new StringWriter();

            if (!debugMode) {
                Console.SetOut(stringWriter);
                Console.SetError(stringWriter);
            }

            ClassificationController cc = new ClassificationController();

            
            cc.Cognize(query);

            StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);

            var result = !debugMode ? stringWriter.ToString() : null;
            stringWriter.Close();

            return result;
        }

		public static String CognizeLegacy(String query, bool debugMode = false) {

			VocabularyModel vocabulary = new VocabularyModel();
            Utterance utterance = new Utterance(query, vocabulary);

            MainPipeline(utterance);

            if (debugMode)
                return utterance.GetDebugLog();
            else
                return utterance.ToJson();
        }

		public static void MainPipeline(Utterance dialog) {

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

using PolarisAICore.Vocabulary;
using System;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;

namespace PolarisAICore {
	public class PolarisAICore {

        static void Main(string[] args) {

            Console.WriteLine(Cognize(Console.ReadLine()).ToString());
        }

        public static JObject Cognize(String query){

            //String output = CognizeLegacy(query);
            VocabularyModel vocabulary = new VocabularyModel();
            Utterance utterance = new Utterance(query, vocabulary);
            utterance.SetMLResponse(CognizeML(query));
            return utterance.GetResponse();
        }

        public static String CognizeDebug(String query) {

            String output = CognizeLegacy(query);
            output += CognizeML(query, true).ToString();
            return output;
        }

        private static JObject CognizeML(String query, bool debug = false) {

            return CognitionSingleton.Instance.Cognize(query);
        }

		private static String CognizeLegacy(String query) {

			VocabularyModel vocabulary = new VocabularyModel();
            Utterance utterance = new Utterance(query, vocabulary);

            MainPipeline(utterance);

            return utterance.GetDebugLog();
        }

		private static void MainPipeline(Utterance dialog) {

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

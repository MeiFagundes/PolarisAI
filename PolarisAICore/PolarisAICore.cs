using PolarisAICore.Vocabulary;
using System;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;

namespace PolarisAICore {
	public class PolarisAICore {

        static void Main() {

            Console.WriteLine("Enter a test query:");
            Console.WriteLine(CognizeDebug(Console.ReadLine()));
        }

        public static JObject Cognize(String query){

            Utterance utterance = new Utterance(CognizeNLP(query));

            return utterance.GetResponse();
        }

        public static String CognizeDebug(String query) {

            Utterance utterance = new Utterance(CognizeNLP(query));

            return utterance.GetDebugLog();
        }

        private static JObject CognizeNLP(String query) {

            return CognitionSingleton.Instance.Cognize(query);
        }

		private static String CognizeLegacy(Utterance utterance) {

            utterance.Vocabulary = new VocabularyModel();

            // Executing Cognition pipeline
            Task cognitionCoreTask = new Task(() => Cognitions.CognitionsController.FetchCognition(utterance));
            cognitionCoreTask.RunSynchronously();
            cognitionCoreTask.Wait();

            // Try and executing Skills
            if (!utterance.IsSkillsEmpty) {
                Task skillsCoreTask = new Task(() => Skills.SkillsController.FetchSkill(utterance));
                skillsCoreTask.RunSynchronously();
            }

            // Generating a response
            Task responseCoreTask = new Task(() => Responses.ResponseController.GenerateResponse(utterance));
            responseCoreTask.RunSynchronously();

            return utterance.GetDebugLog();
        }
	}
}

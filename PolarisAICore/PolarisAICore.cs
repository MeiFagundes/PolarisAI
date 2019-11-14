using PolarisAICore.Vocabulary;
using System;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using PolarisAICore.Properties;

namespace PolarisAICore {
	public class PolarisAICore {

        static readonly PolarisAIDatabaseConnection _database = new PolarisAIDatabaseConnection(
            Resources.ResourceManager.GetString("DBsource"),
            Resources.ResourceManager.GetString("DBname"),
            Resources.ResourceManager.GetString("DBlogin"),
            Resources.ResourceManager.GetString("DBpassword"));

        static void Main() {

            while (true) {
                Console.WriteLine("Enter a test query:");
                Console.WriteLine(CognizeDebug(Console.ReadLine()));
            }
        }

        public static JObject Cognize(String query){

            Utterance utterance = new Utterance(CognizeNLP(query));
            utterance.Response = Response.ResponseController.SetResponse(utterance);

            _database.InsertRequestDetails(utterance);

            return utterance.GetResponse();
        }

        public static String CognizeDebug(String query) {

            Utterance utterance = new Utterance(CognizeNLP(query));
            utterance.Response = Response.ResponseController.SetResponse(utterance);

            return utterance.GetDebugLog();
        }

        private static JObject CognizeNLP(String query) {

            return IntentClassificatorSingleton.Instance.Cognize(query);
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
            Task responseCoreTask = new Task(() => Response.ResponseController.SetResponseLegacy(utterance));
            responseCoreTask.RunSynchronously();

            return utterance.GetDebugLog();
        }
	}
}

using PolarisAICore.Vocabulary;
using System;
using System.Threading.Tasks;
using System.IO;

namespace PolarisAICore {
	public class PolarisAICore {

        static void Main(string[] args) {

            Console.WriteLine(Cognize(Console.ReadLine(), true));
        }

        public static String Cognize(String query, bool debug = false){

            String output = CognizeLegacy(query, debug);
            output += CognizeML(query);
            return output;
        }

        public static String CognizeML(String query, bool debug = false) {

            StringWriter stringWriter = new StringWriter();

            if (!debug) {
                Console.SetOut(stringWriter);
                Console.SetError(stringWriter);
                Console.WriteLine("=============== Starlight ML Cognition ===============\n");
            }
            
            Console.WriteLine(CognitionSingleton.Instance.Cognize(query));

            StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);

            var result = !debug ? stringWriter.ToString() : null;
            stringWriter.Close();

            return result;
        }

		public static String CognizeLegacy(String query, bool debug = false) {

			VocabularyModel vocabulary = new VocabularyModel();
            Utterance utterance = new Utterance(query, vocabulary);

            MainPipeline(utterance);

            if (debug)
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

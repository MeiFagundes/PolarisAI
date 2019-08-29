using System;
using System.Reflection;
using System.Threading.Tasks;

namespace PolarisAICore.Cognitions {
	public static class CognitionsController {

		/// <summary>
		/// Fetches and executes the 'Cognize' Methods from all the Classes inside the 'Cognition' Namespace
		/// </summary>
		/// <param name="dialog"></param>
		public static void FetchCognition(Utterance dialog) {

			Type[] classTypes = Utilities.Reflection.GetTypesInNamespace(Assembly.GetExecutingAssembly(), "PolarisAICore.Cognitions");
			
			for (int i = 0; i < classTypes.Length; i++) {
				// The StartsWith("<>") is in there to avoid calling the '<>c__DisplayClass1_...' class from the Debugger, if this happens an 'NullReferenceException' will be thrown
				if (classTypes[i].Name != "CognitionsController" && !classTypes[i].Name.StartsWith("<>")) {

					MethodInfo classMethod = classTypes[i].GetMethod("Cognize");
					Task cognitionTask = new Task(() => classMethod.Invoke(null, new object[] { dialog }));
					cognitionTask.RunSynchronously();
				}
			}
		}
	}


}

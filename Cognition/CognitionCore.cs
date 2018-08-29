using System;
using System.Reflection;
using System.Threading.Tasks;

namespace POLARIS.Cognition {
	public static class CognitionCore {

		/// <summary>
		/// Fetches and executes the 'Cognize' Methods from all the Classes inside the 'Cognition' Namespace
		/// </summary>
		/// <param name="dialog"></param>
		public static void FetchCognition(Dialog dialog) {

			Type[] classTypes = POLARIS.Utilities.GetTypesInNamespace(Assembly.GetExecutingAssembly(), "POLARIS.Cognition");
			
			for (int i = 0; i < classTypes.Length; i++) {
				// The StartsWith("<>") is in there to avoid calling the '<>c__DisplayClass1_...' class from the Debugger, if this happens an 'NullReferenceException' will be thrown
				if (classTypes[i].Name != "CognitionCore" && !classTypes[i].Name.StartsWith("<>")) {

					MethodInfo classMethod = classTypes[i].GetMethod("Cognize");
					Task cognitionTask = new Task(() => classMethod.Invoke(null, new object[] { dialog }));
					cognitionTask.RunSynchronously();
				}
			}
		}
	}


}

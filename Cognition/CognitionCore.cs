using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace POLARIS.Cognition {
	public static class CognitionCore {

		/// <summary>
		/// Fetches and executes the 'Cognize' Methods from all the Classes inside the 'Cognition' Namespace
		/// </summary>
		/// <param name="dialog"></param>
		public static void Fetch(Dialog dialog) {

			Type[] classTypes = POLARIS.Utilities.GetTypesInNamespace(Assembly.GetExecutingAssembly(), "POLARIS.Cognition");

			// If this throws a 'NullReferenceException' just change the FOR condition to 'i < classTypes.Length - 1' to make it not trigger the '<>c__DisplayClass1_0' Class
			for (int i = 0; i < classTypes.Length; i++) {
				if (classTypes[i].Name != "CognitionCore") {
					MethodInfo classMethod = classTypes[i].GetMethod("Cognize");
					//Task.Factory.StartNew(() => classMethod.Invoke(null, new object[] { dialog }));
					classMethod.Invoke(null, new object[] { dialog });
				}
			}
		}
	}


}

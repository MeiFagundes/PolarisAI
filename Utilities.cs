using System;
using System.Linq;
using System.Reflection;

namespace PolarisCore {
	public static class Utilities {

		/// <summary>
		/// Returns all the Types from the current assembly found in a Namespace
		/// </summary>
		/// <param name="assembly"></param>
		/// <param name="nameSpace"></param>
		/// <returns></returns>
		public static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace) {
			return
			  assembly.GetTypes()
					  .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
					  .ToArray();
		}

		public static void ReachUtility(MethodBase methodBase) {
			Console.WriteLine("Reached Method '" + methodBase.Name + "' from Class '" + methodBase.DeclaringType.Name + "'");
		}
	}
}

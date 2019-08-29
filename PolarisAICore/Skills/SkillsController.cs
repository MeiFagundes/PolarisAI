using System;
using System.Linq;
using System.Reflection;

namespace PolarisAICore.Skills {
	public static class SkillsController {
        
		/// <summary>
		/// Fetches and executes the 'Execute' Method from the respective Class named after the Skill found in the Dialog object inside the 'Skills' Namespace
		/// </summary>
		/// <param name="d"></param>
		public static void FetchSkill(Utterance d) {

			String skillName = d.Phrase[d.SkillsIndex[0]];
			skillName = skillName.First().ToString().ToUpper() + skillName.Substring(1);
			Type classType = Type.GetType("PolarisCore.Skills." + skillName);

			if (classType == null) {
                d.IsRequestingUnimplementedSkill = true;
				return;
			}

			// The StartsWith("<>") is in there to avoid calling the '<>c__DisplayClass1_...' class from the Debugger, if this happens an 'NullReferenceException' will be thrown
			if (classType.Name != "SkillsController" && !classType.Name.StartsWith("<>")) {

				MethodInfo classMethod = classType.GetMethod("Execute");
                classMethod.Invoke(null, new object[] { d });
            }
		}
	}
}

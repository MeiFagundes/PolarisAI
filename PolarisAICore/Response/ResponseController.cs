using System;
using System.Linq;
using System.Reflection;

namespace PolarisAICore.Response {
    public static class ResponseController {

        public static String SetResponse(Utterance u) {

            String intentName = u.TopScoringIntent.Name;

            intentName = intentName.First().ToString().ToUpper() + intentName.Substring(1);
            Type classType = Type.GetType("PolarisAICore.Response." + intentName);

            // The StartsWith("<>") is in there to avoid calling the '<>c__DisplayClass1_...' class from the Debugger, if this happens an 'NullReferenceException' will be thrown
            if (classType != null && classType.Name != "ResponseController" && !classType.Name.StartsWith("<>")) {

                MethodInfo classMethod = classType.GetMethod("SetResponse");
                return (String)classMethod.Invoke(null, new object[] { u });
            }
            else
                return null;
        }
    }
}

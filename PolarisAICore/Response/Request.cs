using System;

namespace PolarisAICore.Response {

    [Obsolete("Static cognition is obsolete, please use NLP cognition instead")]
    public static class Request {
        public static void SetResponse(Utterance d) {

            if (!d.IsSkillsEmpty) {
                if (d.IsRequestingUnimplementedSkill) {
                    d.Response = "Sorry, I recognize that you want me to " + d.Phrase[d.SkillsIndex[0]] + " something, but I don't know how to do it yet.";
                }
                else {
                    if (d.Response == String.Empty)
                        Request.SetGenericSuccessfulResponse(d);
                }
            }
            else {
                d.Response = "Sorry, i'm not quite sure if I understood what you asked me to do.";
            }
            
        }

        public static void SetGenericSuccessfulResponse(Utterance d) {

            d.Response = "Sure thing, i'm going to " + d.Phrase[d.SkillsIndex[0]] + " it for you.";
        }
    }
}

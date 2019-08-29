using System;

namespace PolarisAICore.Cognitions {

	public static class Request {

		/// <summary>
		/// Check if the Phrase is a Request
		/// </summary>
		/// <param name="d"></param>
		public static void Cognize(Utterance d) {

            // There has to be a Verb to be a Request and it has to be up to the third word.
            if (!d.IsVerbsEmpty && d.VerbsIndex[0] < 3) {

                // isRequest if the first word represents a skill.
                if (!d.IsSkillsEmpty && d.SkillsIndex[0] <= 2) {
                    d.IsRequest = true;
                    return;
                }

                if (!d.IsPronounsEmpty){

                    /*if (d.Contains("you")) {

                        for (Byte i = 0; i < d.SkillsIndex.Count; i++) {
                            Byte differenceTemp = (Byte)(d.SkillsIndex[i] - d.GetFirstOccurrenceIndex("you"));
                            if (differenceTemp <= 2 && differenceTemp > 0) {
                                d.IsRequest = true;
                                return;
                            }
                        }
                    }*/

                    // isRequest if the Pronoun is (not immediately) after the Verb
                    if (d.PronounsIndex[0] > d.VerbsIndex[0] && d.PronounsIndex[0] - d.VerbsIndex[0] > 3){
                        d.IsRequest = true;
                        return;
                    }
                }



				// isRequest if there's just a Verb. || Example: "Do this..."
				if (d.IsPronounsEmpty && d.VerbsIndex[0] < 1) {
					d.IsRequest = true;
					return;
				}
				
			}
			d.IsRequest = false;
			return;
		}
	}
}

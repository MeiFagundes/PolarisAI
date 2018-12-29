using System;

namespace PolarisCore.Cognition {

	public static class Request {

		/// <summary>
		/// Check if the Phrase is a Request
		/// </summary>
		/// <param name="dialog"></param>
		public static void Cognize(Dialog dialog) {

			// There has to be a Verb to be a Request and it has to be up to the third word.
			if (!dialog.IsVerbsEmpty && dialog.VerbsIndex[0] < 3) {


                if (!dialog.IsPronounsEmpty){
                    // isRequest if an "you" is succeeded almost immediately by an ActionVerb.
                    for (int i = 0; i < dialog.PronounsIndex.Count; i++){
                        if (dialog.Phrase[dialog.PronounsIndex[i]] == "you"){
                            for (int j = 0; j < dialog.SkillsIndex.Count; j++){
                                Int32 differenceTemp = dialog.SkillsIndex[j] - dialog.PronounsIndex[i];
                                if (differenceTemp <= 2 && differenceTemp > 0){
                                    dialog.IsRequest = true;
                                    return;
                                }
                            }
                        }
                    }

                    // isRequest if the Pronoun is (not immediately) after the Verb
				    if (dialog.PronounsIndex[0] > dialog.VerbsIndex[0] && dialog.PronounsIndex[0] - dialog.VerbsIndex[0] > 3){
                        dialog.IsRequest = true;
                        return;
                    }
                }

				// isRequest if there's just a Verb. || Example: "Do this..."
				if (dialog.IsPronounsEmpty && dialog.VerbsIndex[0] < 1) {
					dialog.IsRequest = true;
					return;
				}
				
			}
			dialog.IsRequest = false;
			return;
		}
	}
}

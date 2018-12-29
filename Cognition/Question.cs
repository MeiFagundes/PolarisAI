using System;

namespace PolarisCore.Cognition {

	public static class Question {
		
		/// <summary>
		/// Check if the Phrase is a question
		/// </summary>
		/// <param name="dialog"></param>
		public static void Cognize(Dialog dialog) {
			Int32 knowIndex = -1;
			Boolean isThereAKnow = false;

			for (int i = 0; i < dialog.Phrase.Count; i++) {

				// Checking if there's a "know"
				if (dialog.Phrase[i] == "know") {
					isThereAKnow = true;
					knowIndex = i;
				}
			}

            // There has to be a Verb to be a question in those cases
            if (!dialog.IsVerbsEmpty) {

                // There has to be a Interrogative pronoun and a Verb to be a question in those cases.
                if (!dialog.IsIntWordsEmpty){
                    if (dialog.VerbsIndex[0] - dialog.IntWordsIndex[0] <= 1 && dialog.VerbsIndex[0] - dialog.IntWordsIndex[0] > 0){
                        dialog.IsQuestion = true;
                        return;
                    }
                }

                // There has to be a Verb and a Pronoun to be a question in those cases
                if (!dialog.IsPronounsEmpty){
                    // isQuestion if an Verb is immediately after an Adverb and a Pronoun is immediately after it. || Example: "How are you"
                    if (!dialog.IsAdverbsEmpty){
                        if (dialog.AdverbsIndex[0] < dialog.VerbsIndex[0] && dialog.VerbsIndex[0] - dialog.AdverbsIndex[0] <= 2){
                            if (dialog.VerbsIndex[0] < dialog.PronounsIndex[0] && dialog.PronounsIndex[0] - dialog.VerbsIndex[0] <= 2){
                                dialog.IsQuestion = true;
                                return;
                            }
                        }
                    }

                    // isQuestion if the Pronoun is immediately after the Verb. || Example: "Do you know if..."
                    if (dialog.PronounsIndex[0] > dialog.VerbsIndex[0] && dialog.PronounsIndex[0] - dialog.VerbsIndex[0] <= 2){
                        dialog.IsQuestion = true;
                        return;
                    }

                    // isQuestion if there's a (Pronoun + Verb + "know") || Example: "I Want to know if..."
                    else if (isThereAKnow && dialog.VerbsIndex[0] > dialog.PronounsIndex[0] && knowIndex > dialog.VerbsIndex[0]){
                        dialog.IsQuestion = true;
                        return;
                    }
                }
			}
			dialog.IsQuestion = false;
			return;
		}

	}
}

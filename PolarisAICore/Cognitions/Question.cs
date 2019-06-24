using System;

namespace PolarisAICore.Cognitions {

	public static class Question {
		
		/// <summary>
		/// Check if the Phrase is a question
		/// </summary>
		/// <param name="d"></param>
		public static void Cognize(Dialog d) {
			Byte? knowIndex;
			Boolean isThereAKnow = false;

            // Checking if there's a "know"
            knowIndex = d.GetFirstOccurrenceIndex("know");
            if (knowIndex != null)
                isThereAKnow = true;

            // There has to be a Verb to be a question in those cases
            if (!d.IsVerbsEmpty) {

                // There has to be a Interrogative pronoun and a Verb to be a question in those cases.
                if (!d.IsIntWordsEmpty){
                    if (d.VerbsIndex[0] - d.IntWordsIndex[0] <= 1 && d.VerbsIndex[0] - d.IntWordsIndex[0] > 0){
                        d.IsQuestion = true;
                        return;
                    }
                }

                // There has to be a Verb and a Pronoun to be a question in those cases
                if (!d.IsPronounsEmpty){
                    // isQuestion if an Verb is immediately after an Adverb and a Pronoun is immediately after it. || Example: "How are you"
                    if (!d.IsAdverbsEmpty){
                        if (d.AdverbsIndex[0] < d.VerbsIndex[0] && d.VerbsIndex[0] - d.AdverbsIndex[0] <= 2){
                            if (d.VerbsIndex[0] < d.PronounsIndex[0] && d.PronounsIndex[0] - d.VerbsIndex[0] <= 2){
                                d.IsQuestion = true;
                                return;
                            }
                        }
                    }

                    // isQuestion if the Pronoun is immediately after the Verb. || Example: "Do you know if..."
                    if (d.PronounsIndex[0] > d.VerbsIndex[0] && d.PronounsIndex[0] - d.VerbsIndex[0] <= 2){
                        d.IsQuestion = true;
                        return;
                    }

                    // isQuestion if there's a (Pronoun + Verb + "know") || Example: "I Want to know if..."
                    else if (isThereAKnow && d.VerbsIndex[0] > d.PronounsIndex[0] && knowIndex > d.VerbsIndex[0]){
                        d.IsQuestion = true;
                        return;
                    }
                }
			}
			d.IsQuestion = false;
			return;
		}

	}
}

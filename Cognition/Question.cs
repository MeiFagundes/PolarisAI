using System;

namespace POLARIS.Cognition {

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

			// isQuestion and if the last Char is a "?"
			if (dialog.Phrase[dialog.Phrase.Count - 1] == "?") {
				dialog.IsQuestion = true;
				return;
			}

			// There has to be a Verb and a Pronoun to be a question in those cases
			if (!dialog.IsVerbsEmpty && !dialog.IsPronounsEmpty) {

				// isQuestion if the Pronoun is immediately after the Verb. || Example: "Do you know if..."
				if (dialog.PronounsIndex[0] > dialog.VerbsIndex[0] && dialog.PronounsIndex[0] - dialog.VerbsIndex[0] <= 1 &&  !dialog.IsNounsEmpty) {
					dialog.IsQuestion = true;
					return;
				}

				// isQuestion if there's a (Pronoun + Verb + "know") || Example: "I Want to know if..."
				else if (isThereAKnow && dialog.VerbsIndex[0] > dialog.PronounsIndex[0] && knowIndex > dialog.VerbsIndex[0]) {
					dialog.IsQuestion = true;
					return;
				}
			}
			dialog.IsQuestion = false;
			return;
		}

	}
}

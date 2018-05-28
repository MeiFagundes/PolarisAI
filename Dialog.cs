using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLARIS {
	class Dialog : Phrase {

		// --- VARIABLES ---
		public Boolean isQuestion = false, isRequest = false;

		// --- CONSTRUCTORS ---
		public Dialog(String input) : base(input) {
			phraseIn = input.ToLower().Split(' ');
			CheckRequest();
		}
		public Dialog() : base() {}

		// --- METHODS ---

		// Check if the Phrase is a Request
		public void CheckRequest() {
			int verbIndex = -1, pronounIndex = -1;

			for (int i = 0; i < phraseIn.Length; i++) {

				// Storing index of the first Verb
				for (int j = 0; j < verbs.Length; j++) {
					if (phraseIn[i] == verbs[j] && verbIndex == -1) {
						verbIndex = i;
					}
				}

				// Storing index of the first Pronoun
				for (int j = 0; j < pronouns.Length; j++) {
					if (phraseIn[i] == pronouns[j] && pronounIndex == -1) {
						pronounIndex = i;
					}
				}
			}

			// isQuestion and isRequest if the last Char is a "?"
			if (phraseIn[phraseIn.Length - 1].Substring(phraseIn[phraseIn.Length - 1].Length - 1) == "?") {
				isRequest = true;
				isQuestion = true;
				return;
			}

			if (pronounIndex > verbIndex || pronounIndex == -1 // isRequest if the Pronoun is after the Verb or if there's just a Verb. || Example: "Do this..."
				&& verbIndex != -1 && verbIndex < 3) { // There has to be a Verb and it has to be up to the third word.
				isRequest = true;
				CheckQuestion(verbIndex, pronounIndex);
			}
		}

		// Check if the Phrase is a question
		public void CheckQuestion(int verbIndex, int pronounIndex) {
			int knowIndex = -1;
			Boolean isThereAKnow = false;

			for (int i = 0; i < phraseIn.Length; i++) {

				// Checking if there's a "know"
				if (phraseIn[i] == "know") {
					isThereAKnow = true;
					knowIndex = i;
				}
			}

			if ((pronounIndex > verbIndex && pronounIndex - verbIndex <= 1 || // isQuestion if the Pronoun is immediately after the Verb. || Example: "Do you know if..."
				(isThereAKnow && verbIndex > pronounIndex && knowIndex > verbIndex)) // isQuestion if there's a (Pronoun + Verb + "know") || Example: "I Want to know if..."
				&& verbIndex != -1 && pronounIndex != -1) // There has to be a Verb and a Pronoun to be a question in those cases
				isQuestion = true;
		}

		override
		public void Debug() {
			Console.WriteLine("\nIs this a Request? : " + isRequest);
			Console.WriteLine(" -> Is this a Question? : " + isQuestion);
			Console.WriteLine("\nVocabulary size:\n   Nº of Verbs: " + verbs.Length + "\n   Nº of Pronouns: " + pronouns.Length);
		}
	}
}

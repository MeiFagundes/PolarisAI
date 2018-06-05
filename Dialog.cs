using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLARIS {
	class Dialog : Phrase {

		// --- VARIABLES ---

		List<int> verbsIndex = new List<int>();
		List<int> pronounsIndex = new List<int>();
		List<int> adverbsIndex = new List<int>();
		public bool isQuestion, isRequest, isVerbsEmpty, isPronounsEmpty, isAdverbsEmpty;

		// --- CONSTRUCTORS ---

		public Dialog(String input) : base(input) {
			phraseIn = input.ToLower().Split(' ');
			
			// Storing index of all known Pronouns and Verbs
			for (int i = 0; i < phraseIn.Length; i++) {

				// Verbs
				for (int j = 0; j < verbsSrc.Length; j++) {
					if (phraseIn[i] == verbsSrc[j]) {
						verbsIndex.Add(i);
					}
				}

				// Pronouns
				for (int j = 0; j < pronounsSrc.Length; j++) {
					if (phraseIn[i] == pronounsSrc[j]) {
						pronounsIndex.Add(i);
					}
				}

				// Adverbs
				for (int j = 0; j < adverbsSrc.Length; j++) {
					if (phraseIn[i] == adverbsSrc[j]) {
						adverbsIndex.Add(i);
					}
				}
			}
			isVerbsEmpty = !verbsIndex.Any();
			isPronounsEmpty = !pronounsIndex.Any();
			isAdverbsEmpty = !adverbsIndex.Any();

			Pipeline();
		}

		public Dialog() : base() {}

		// --- METHODS ---
		
		// Main Pipeline to aggregate all Checks
		public void Pipeline() {

			isRequest = CheckRequest();
			isQuestion = CheckQuestion();
		}
		
		// Check if the Phrase is a Request
		public bool CheckRequest() {

			

			// There has to be a Verb to be a Request and it has to be up to the third word.
			if (!isVerbsEmpty && verbsIndex[0] < 3) {

				// isRequest if there's just a Verb. || Example: "Do this..."
				if (isPronounsEmpty) {
					return true;
				}

				// isRequest if the Pronoun is (not immediately) after the Verb
				else if (pronounsIndex[0] > verbsIndex[0] && pronounsIndex[0] - verbsIndex[0] > 1) {
					return true;
				}
			}
			return false;
		}

		// Check if the Phrase is a question
		public bool CheckQuestion() {
			int knowIndex = -1;
			bool isThereAKnow = false;

			for (int i = 0; i < phraseIn.Length; i++) {

				// Checking if there's a "know"
				if (phraseIn[i] == "know") {
					isThereAKnow = true;
					knowIndex = i;
				}
			}

			// isQuestion and if the last Char is a "?"
			if (phraseIn[phraseIn.Length - 1].Substring(phraseIn[phraseIn.Length - 1].Length - 1) == "?") {
				return true;
			}

			// There has to be a Verb and a Pronoun to be a question in those cases
			if (!isVerbsEmpty && !isPronounsEmpty) {

				// isQuestion if the Pronoun is immediately after the Verb. || Example: "Do you know if..."
				if (pronounsIndex[0] > verbsIndex[0] && pronounsIndex[0] - verbsIndex[0] <= 1) {
					return true;
				}

				// isQuestion if there's a (Pronoun + Verb + "know") || Example: "I Want to know if..."
				else if (isThereAKnow && verbsIndex[0] > pronounsIndex[0] && knowIndex > verbsIndex[0]) { 
					return true;
				}
			}
			return false;
		}

		override
		public void Debug() {
			Console.WriteLine("\nisPronounsEmpty? : " + isPronounsEmpty);
			Console.WriteLine("isVerbsEmpty? : " + isVerbsEmpty + "\n");
			Console.WriteLine("Is this a Request? : " + isRequest);
			Console.WriteLine("Is this a Question? : " + isQuestion);
			Console.WriteLine("\nVocabulary size:\n   Nº of Verbs: " + verbsSrc.Length + "\n   Nº of Pronouns: " + pronounsSrc.Length + "\n   Nº of Adverbs: " + adverbsSrc.Length);
		}
	}
}

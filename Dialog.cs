using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLARIS {
	class Dialog : Phrase {

		// --- VARIABLES ---

		List<Int32> verbsIndex = new List<Int32>();
		List<Int32> pronounsIndex = new List<Int32>();
		List<Int32> adverbsIndex = new List<Int32>();
		List<Int32> actionVerbsIndex = new List<Int32>();
		public Boolean isQuestion, isRequest, isVerbsEmpty, isPronounsEmpty, isAdverbsEmpty, isActionVerbsEmpty;

		// --- CONSTRUCTORS ---

		public Dialog(String input) : base(input) {
			
			String[] Aux;
			Aux = input.ToLower().Split(' ');

			// Copying input from Aux to the List 'phrase'
			for (int i = 0; i < Aux.Length; i++) {
				phrase.Add(Aux[i]);
			}

			// Isolating ponctuation mark as a last String
			String lastString = phrase[phrase.Count - 1].Substring(phrase[phrase.Count - 1].Length - 1);
			if (lastString.Substring(lastString.Length - 1) == "?" || lastString.Substring(lastString.Length - 1) == "." || lastString.Substring(lastString.Length - 1) == "!") {
				String dot = lastString.Substring(lastString.Length - 1);
				phrase[phrase.Count - 1].Remove(phrase[phrase.Count - 1].Length - 1);
				phrase.Add(dot);
			}

			// Storing index of all known Pronouns, Verbs, ActionVerbs and Adverbs
			for (int i = 0; i < phrase.Count; i++) {

				// Verbs
				for (int j = 0; j < verbsFile.Length; j++) {
					if (phrase[i] == verbsFile[j]) {
						verbsIndex.Add(i);
					}
				}

				// Pronouns
				for (int j = 0; j < pronounsFile.Length; j++) {
					if (phrase[i] == pronounsFile[j]) {
						pronounsIndex.Add(i);
					}
				}

				// Adverbs
				for (int j = 0; j < adverbsFile.Length; j++) {
					if (phrase[i] == adverbsFile[j]) {
						adverbsIndex.Add(i);
					}
				}

				// Action Verbs
				for (int j = 0; j < actionVerbsFile.Length; j++) {
					if (phrase[i] == actionVerbsFile[j]) {
						actionVerbsIndex.Add(i);
					}
				}
			}

			isActionVerbsEmpty = !actionVerbsIndex.Any();
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
		public Boolean CheckRequest() {
			
			// There has to be a Verb to be a Request and it has to be up to the third word.
			if (!isVerbsEmpty && verbsIndex[0] < 3) {

				// isRequest if an "you" is succeeded almost immediately by an ActionVerb.
				for (int i = 0; i < pronounsIndex.Count; i++) {
					if (phrase[pronounsIndex[i]] == "you") {
						for (int j = 0; j < actionVerbsIndex.Count; j++) {
							Int32 differenceTemp = actionVerbsIndex[j] - pronounsIndex[i];
							if (differenceTemp <= 2 && differenceTemp > 0) {
								return true;
							}
						}
					}
				}

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
		public Boolean CheckQuestion() {
			Int32 knowIndex = -1;
			Boolean isThereAKnow = false;

			for (int i = 0; i < phrase.Count; i++) {

				// Checking if there's a "know"
				if (phrase[i] == "know") {
					isThereAKnow = true;
					knowIndex = i;
				}
			}

			// isQuestion and if the last Char is a "?"
			if (phrase[phrase.Count - 1] == "?") {
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
			Console.WriteLine("\nIs this a Request? : " + isRequest);
			Console.WriteLine("Is this a Question? : " + isQuestion);
			Console.WriteLine("\nVocabulary size:\n   Nº of Verbs: " + verbsFile.Length + "\n   Nº of Pronouns: " + pronounsFile.Length + "\n   Nº of Adverbs: " + adverbsFile.Length + "\n");
		}
	}
}

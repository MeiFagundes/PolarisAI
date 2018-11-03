using System;
using System.Collections.Generic;
using System.Linq;

namespace POLARIS {
	public class Dialog {

		// --- VARIABLES ---

		public Vocabulary vocabulary;

		public List<String> Phrase { get; set; } = new List<String>();
		public List<UInt16> VerbsIndex { get; set; } = new List<UInt16>();
		public List<UInt16> PronounsIndex { get; set; } = new List<UInt16>();
		public List<UInt16> AdverbsIndex { get; set; } = new List<UInt16>();
		public List<UInt16> SkillsIndex { get; set; } = new List<UInt16>();
		public List<UInt16> NounsIndex { get; set; } = new List<UInt16>();
        public List<UInt16> IntWordsIndex { get; set; } = new List<UInt16>();

        public Boolean IsVerbsEmpty { get; set; }
		public Boolean IsPronounsEmpty { get; set; }
		public Boolean IsAdverbsEmpty { get; set; }
		public Boolean IsSkillsEmpty { get; set; }
		public Boolean IsNounsEmpty { get; set; }
        public Boolean IsIntWordsEmpty { get; set; }
        public Boolean IsRequest { get; set; }
		public Boolean IsQuestion { get; set; }

		// --- CONSTRUCTORS ---

		public Dialog(String input, Vocabulary vocabularyIn) {

			this.vocabulary = vocabularyIn;

			Phrase = input.ToLower().Split(' ').ToList();
			Phrase.RemoveAll(String.IsNullOrEmpty);


            // Deleting all Punctuation Marks from the Phrase
            for (int i = 0; i < Phrase.Count; i++) {
                foreach (String pontMark in vocabulary.PunctuationMarks) {
                    Phrase[i] = Phrase[i].Replace(pontMark, String.Empty);
                }
            }

			// Isolating ponctuation mark as a last String
			String lastString = Phrase[Phrase.Count - 1].Substring(Phrase[Phrase.Count - 1].Length - 1);
			if (lastString.Substring(lastString.Length - 1) == "?" || lastString.Substring(lastString.Length - 1) == "." || lastString.Substring(lastString.Length - 1) == "!") {
				String dot = lastString.Substring(lastString.Length - 1);
				Phrase[Phrase.Count - 1].Remove(Phrase[Phrase.Count - 1].Length - 1);
				Phrase.Add(dot);
			}
			
			IsSkillsEmpty = IndexVocabulary(vocabulary.Skills, SkillsIndex);
			IsVerbsEmpty = IndexVocabulary(vocabulary.Verbs, VerbsIndex);
			IsPronounsEmpty = IndexVocabulary(vocabulary.Pronouns, PronounsIndex);
			IsAdverbsEmpty = IndexVocabulary(vocabulary.Adverbs, AdverbsIndex);
			IsNounsEmpty = IndexVocabulary(vocabulary.Nouns, NounsIndex);
            IsIntWordsEmpty = IndexVocabulary(vocabulary.IntWords, IntWordsIndex);

        }
		public Dialog() { }

		// --- METHODS ---

		/// <summary>
		/// Stores Vocabulary elements and their indexes
		/// </summary>
		/// <param name="VocabularyFile"></param>
		/// <param name="Indexes"></param>
		/// <returns></returns>
		private Boolean IndexVocabulary(List<String> VocabularyFile, List<UInt16> Indexes) {

			for (UInt16 i = 0; i < Phrase.Count; i++) {
				foreach (String currentFile in VocabularyFile) {
					if (Phrase[i] == currentFile) {
						Indexes.Add(i);
					}
				}
			}
			return !Indexes.Any();
		} 

		public void Debug() {
			Console.WriteLine("\n ------ Dialog Debug ------ \n");
            Console.WriteLine("  Is this a Request ? : " + IsRequest);
            Console.WriteLine("  Is this a Question? : " + IsQuestion + "\n");

            List<String>[] phraseDebug = new List<String>[Phrase.Count];

            for (int i = 0; i < phraseDebug.Length; i++) {
                phraseDebug[i] = new List<String>();
            }

            for (int i = 0; i < Phrase.Count; i++) {
                if (VerbsIndex.Exists(index => index == i))
                    phraseDebug[i].Add("Verb");
                if (SkillsIndex.Exists(index => index == i))
                    phraseDebug[i].Add("Skill");
                if (PronounsIndex.Exists(index => index == i))
                    phraseDebug[i].Add("Pronoun");
                if (AdverbsIndex.Exists(index => index == i))
                    phraseDebug[i].Add("Adverb");
                if (NounsIndex.Exists(index => index == i))
                    phraseDebug[i].Add("Noun");
                if (IntWordsIndex.Exists(index => index == i))
                    phraseDebug[i].Add("Interrogative Word");
            }

            for (int i = 0; i < phraseDebug.Length; i++) {

                Console.Write("  '" + Phrase[i] + "' -> ");
                foreach (String type in phraseDebug[i]) {
                    Console.Write(type + "; ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n -------------------------- \n");
        }
	}
}

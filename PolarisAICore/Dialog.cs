using PolarisAICore.Vocabulary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PolarisAICore {
	public class Dialog {

		// --- VARIABLES ---

		public VocabularyModel vocabulary;
        
		public List<String> Phrase { get; set; } = new List<String>();
		public List<Byte> VerbsIndex { get; set; } = new List<Byte>();
		public List<Byte> PronounsIndex { get; set; } = new List<Byte>();
		public List<Byte> AdverbsIndex { get; set; } = new List<Byte>();
		public List<Byte> SkillsIndex { get; set; } = new List<Byte>();
		public List<Byte> NounsIndex { get; set; } = new List<Byte>();
        public List<Byte> IntWordsIndex { get; set; } = new List<Byte>();

        public String FirstVerb { get; set; }
        public String FirstPronoun { get; set; }
        public String FirstAdverb { get; set; }
        public String FirstSkill { get; set; }
        public String FirstNoun { get; set; }
        public String FirstIntWord { get; set; }

        public Boolean IsVerbsEmpty { get; set; }
		public Boolean IsPronounsEmpty { get; set; }
		public Boolean IsAdverbsEmpty { get; set; }
		public Boolean IsSkillsEmpty { get; set; }
		public Boolean IsNounsEmpty { get; set; }
        public Boolean IsIntWordsEmpty { get; set; }
        public Boolean IsRequest { get; set; }
		public Boolean IsQuestion { get; set; }
        public Boolean IsRequestingUnimplementedSkill { get; set; }

        /// <summary>
        /// Code:
        /// 0: No action available
        /// ...
        /// </summary>
        public Int32 Code { get; set; } = 0;
        public String Response { get; set; }
        public String ResponseData { get; set; }


        // --- CONSTRUCTORS ---

        public Dialog(String input, VocabularyModel vocabularyIn) {

			this.vocabulary = vocabularyIn;

			Phrase = input.ToLower().Split(' ').ToList();
			Phrase.RemoveAll(String.IsNullOrEmpty);


            // Deleting all Punctuation Marks from the Phrase
            for (int i = 0; i < Phrase.Count; i++) {
                foreach (String pontMark in vocabulary.PunctuationMarks) {
                    Phrase[i] = Phrase[i].Replace(pontMark, String.Empty);
                }
            }
            Phrase.RemoveAll(t => t == String.Empty);

            // Isolating ponctuation mark as a last String
			/*String lastString = Phrase[Phrase.Count - 1].Substring(Phrase[Phrase.Count - 1].Length - 1);
			if (lastString.Substring(lastString.Length - 1) == "?" || lastString.Substring(lastString.Length - 1) == "." || lastString.Substring(lastString.Length - 1) == "!") {
				String dot = lastString.Substring(lastString.Length - 1);
				Phrase[Phrase.Count - 1].Remove(Phrase[Phrase.Count - 1].Length - 1);
				Phrase.Add(dot);
			}*/
			
			IsSkillsEmpty = IndexInput(vocabulary.Skills, SkillsIndex, FirstSkill);
			IsVerbsEmpty = IndexInput(vocabulary.Verbs, VerbsIndex, FirstVerb);
			IsPronounsEmpty = IndexInput(vocabulary.Pronouns, PronounsIndex, FirstPronoun);
			IsAdverbsEmpty = IndexInput(vocabulary.Adverbs, AdverbsIndex, FirstAdverb);
			IsNounsEmpty = IndexInput(vocabulary.Nouns, NounsIndex, FirstNoun);
            IsIntWordsEmpty = IndexInput(vocabulary.IntWords, IntWordsIndex, FirstIntWord);

        }
		public Dialog() { }

        // --- METHODS ---

        /// <summary>
        /// Stores Vocabulary elements, their indexes and first-of-type pointers
        /// </summary>
        /// <param name="VocabularyFile"></param>
        /// <param name="Indexes"></param>
        /// <param name="FirstOfTypePointer"></param>
        /// <returns></returns>
        private Boolean IndexInput(List<String> VocabularyFile, List<Byte> Indexes, String FirstOfTypePointer) {

			for (Byte i = 0; i < Phrase.Count; i++) {
				foreach (String currentFile in VocabularyFile) {
					if (Phrase[i] == currentFile) {
						Indexes.Add(i);
					}
				}
			}

            if (Indexes.Any())
                FirstOfTypePointer = Phrase[Indexes[0]];

			return !Indexes.Any();
		}

        public Boolean Contains(String word) {
            return Phrase.Exists(t => t.Equals(word));
        }

        public Byte? GetFirstOccurrenceIndex(String word) {

            for (Byte i = 0; i < Phrase.Count; i++) {
                if (word.Equals(Phrase[i]))
                    return i;
            }
            return null;
        }

        /// <summary>
        /// Calculates the first Word position minus the second Word.
        ///     Negative values: First word comes before.
        ///     Positive values: First word comes after.
        ///     Zero: The words are the same.
        /// </summary>
        /// <param name="firstWord"></param>
        /// <param name="secondWord"></param>
        /// <returns></returns>
        public Int16? GetPositionDifference(String firstWord, String secondWord) {

            if (Contains(firstWord) && Contains(secondWord)) {

                return (Int16)(GetFirstOccurrenceIndex(firstWord) - GetFirstOccurrenceIndex(secondWord));
            }
            return null;
        }

        public Boolean ComesFirst(String firstWord, String secondWord) {
            return GetPositionDifference(firstWord, secondWord) < 0;
        }

        public Boolean ComesAfter(String firstWord, String secondWord) {
            return GetPositionDifference(firstWord, secondWord) > 0;
        }

        public String ToJson() {

            return "{" +
                "\"Code\":" + Code + "," +
                "\"Response\":" + (Response == String.Empty || Response == null ? "null" : "\"" + Response + "\"") + "," +
                "\"ResponseData\":" + (ResponseData == String.Empty || ResponseData == null ? "null" : "\"" + ResponseData + "\"") +
                "}";
        }

		public string GetDebugInfo() {

            string debugInfo = "";
			debugInfo += "\n ------ Dialog Debug ------ \n\n";
            debugInfo += "  Is this a Request?  : " + IsRequest + "\n";
            debugInfo += "  Is this a Question? : " + IsQuestion + "\n\n";

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

                debugInfo += "  '" + Phrase[i] + "' -> ";
                foreach (String type in phraseDebug[i]) {
                    debugInfo += type + "; ";
                }
                debugInfo += "\n";
            }
            debugInfo += "\n  JSON Output: " + ToJson() + "\n";

            debugInfo += "\n -------------------------- \n\n";

            return debugInfo;
        }

        private class Output {

            public Output() { }

            public Int32 Code { get; set; }
            public String Response { get; set; }
            public String ResponseData { get; set; }

        }
    }
}

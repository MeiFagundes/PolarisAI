using Newtonsoft.Json.Linq;
using PolarisAICore.Vocabulary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PolarisAICore {
	public class Utterance {

		// --- VARIABLES ---

		public VocabularyModel Vocabulary { get; set; }

        public string query;
        public List<String> Phrase { get; set; } = new List<String>();
		public List<Byte> VerbsIndex { get; set; } = new List<Byte>();
		public List<Byte> PronounsIndex { get; set; } = new List<Byte>();
		public List<Byte> AdverbsIndex { get; set; } = new List<Byte>();
		public List<Byte> SkillsIndex { get; set; } = new List<Byte>();
		public List<Byte> NounsIndex { get; set; } = new List<Byte>();
        public List<Byte> IntWordsIndex { get; set; } = new List<Byte>();

        public Boolean IsVerbsEmpty { get; set; }
		public Boolean IsPronounsEmpty { get; set; }
		public Boolean IsAdverbsEmpty { get; set; }
		public Boolean IsSkillsEmpty { get; set; }
		public Boolean IsNounsEmpty { get; set; }
        public Boolean IsIntWordsEmpty { get; set; }
        public Boolean IsRequest { get; set; }
		public Boolean IsQuestion { get; set; }
        public Boolean IsRequestingUnimplementedSkill { get; set; }

        public IDictionary<String, float?> Intents { get; set; }

        public Int32 Code { get; set; } = 0;
        public String Response { get; set; }
        public String ResponseData { get; set; }


        // --- CONSTRUCTORS ---

        public Utterance(String input, VocabularyModel vocabularyIn) {

			this.Vocabulary = vocabularyIn;

            query = input.ToLower();
			Phrase = query.Split(' ').ToList();
			Phrase.RemoveAll(String.IsNullOrEmpty);


            // Deleting all Punctuation Marks from the Phrase
            for (int i = 0; i < Phrase.Count; i++) {
                foreach (String pontMark in Vocabulary.PunctuationMarks) {
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
			
			IsSkillsEmpty = IndexInput(Vocabulary.Skills, SkillsIndex);
			IsVerbsEmpty = IndexInput(Vocabulary.Verbs, VerbsIndex);
			IsPronounsEmpty = IndexInput(Vocabulary.Pronouns, PronounsIndex);
			IsAdverbsEmpty = IndexInput(Vocabulary.Adverbs, AdverbsIndex);
			IsNounsEmpty = IndexInput(Vocabulary.Nouns, NounsIndex);
            IsIntWordsEmpty = IndexInput(Vocabulary.IntWords, IntWordsIndex);

        }
		public Utterance() { }

        // --- METHODS ---

        /// <summary>
        /// Stores Vocabulary elements, their indexes and first-of-type pointers
        /// </summary>
        /// <param name="VocabularyFile"></param>
        /// <param name="Indexes"></param>
        /// <param name="FirstOfTypePointer"></param>
        /// <returns></returns>
        private Boolean IndexInput(List<String> VocabularyFile, List<Byte> Indexes) {

			for (Byte i = 0; i < Phrase.Count; i++) {
				foreach (String currentFile in VocabularyFile) {
					if (Phrase[i] == currentFile) {
						Indexes.Add(i);
					}
				}
			}

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

        public void SetMLResponse(JObject json) {

            Intents = new Dictionary<String, float?>();

            for (int i = 0; i < json["intents"].Count(); i++) {
                Intents.Add(json["intents"][i]["intent"].ToString(), float.Parse(json["intents"][i]["score"].ToString()));
            }

            if (Intents.Any())
                // Temp
                Response = $"The top scoring Intent for this request is {Intents.First().Key}, with a score of {((float) Intents.First().Value * 100).ToString("n1")}%";
        }

        public JObject GetResponse() {

            JObject reponse =
                new JObject(
                    new JProperty("code", Code),
                    new JProperty("response", Response == String.Empty ? null : Response),
                    new JProperty("responseData", ResponseData == String.Empty ? null : ResponseData)
                );

            return reponse;
        }

		public string GetDebugLog() {

            string debugInfo = "";

            debugInfo += "Input: '" + query + "'\n";
            debugInfo += "\n=============== PolarisAI Utterance Cognition ===============\n\n";
            debugInfo += "Is this a Request?  : " + IsRequest + "\n";
            debugInfo += "Is this a Question? : " + IsQuestion + "\n\n";

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
            debugInfo += "\n  JSON Output: " + GetResponse() + "\n";

            debugInfo += "Response: " + Response + "\n\n";

            return debugInfo;
        }
    }
}

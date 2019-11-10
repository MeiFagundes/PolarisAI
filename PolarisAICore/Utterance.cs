using Newtonsoft.Json.Linq;
using PolarisAICore.Vocabulary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PolarisAICore {
	public class Utterance {

		// --- Attributes ---


        public string Query;

        public List<Intent> Intents { get; set; }
        public Int32 Code { get; set; } = 0;
        public String Response { get; set; }
        public JObject Entity { get; set; }

        private readonly JObject _nlpResponse;

        // Legacy

        public VocabularyModel Vocabulary { get; set; }
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


        // --- Constructors ---

        public Utterance(JObject NLPResponse) {

            _nlpResponse = NLPResponse;

            Query = _nlpResponse["query"].ToString();

            // Set intents
            Intents = new List<Intent>();

            for (int i = 0; i < _nlpResponse["intents"].Count(); i++) {
                Intents.Add(new Intent(_nlpResponse["intents"][i]["intent"].ToString(), float.Parse(_nlpResponse["intents"][i]["score"].ToString())));
            }

            // Set entity
            if (Intents.Any()) {
                Entity = (JObject) _nlpResponse["entities"];
            }
        }

        [Obsolete("Static cognition is obsolete, please use Utterance(JObject NLPResponse) instead")]
        public Utterance(String input) {

            Query = input.ToLower();
			Phrase = Query.Split(' ').ToList();
			Phrase.RemoveAll(String.IsNullOrEmpty);


            // Deleting all Punctuation Marks from the Phrase
            for (int i = 0; i < Phrase.Count; i++) {
                foreach (String pontMark in Vocabulary.PunctuationMarks) {
                    Phrase[i] = Phrase[i].Replace(pontMark, String.Empty);
                }
            }
            Phrase.RemoveAll(t => t == String.Empty);
			
			IsSkillsEmpty = IndexInput(Vocabulary.Skills, SkillsIndex);
			IsVerbsEmpty = IndexInput(Vocabulary.Verbs, VerbsIndex);
			IsPronounsEmpty = IndexInput(Vocabulary.Pronouns, PronounsIndex);
			IsAdverbsEmpty = IndexInput(Vocabulary.Adverbs, AdverbsIndex);
			IsNounsEmpty = IndexInput(Vocabulary.Nouns, NounsIndex);
            IsIntWordsEmpty = IndexInput(Vocabulary.IntWords, IntWordsIndex);

        }

        // --- Methods ---

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

        private Intent GetTopScoringIntent() {

            Intent topScoringIntent = new Intent();
            float maxValue = 0;

            foreach (Intent intent in Intents) {
                if (intent.Score > maxValue) {
                    topScoringIntent = intent;
                    maxValue = intent.Score;
                }
            }

            return topScoringIntent;
        }

        public JObject GetResponse() {

            JObject reponse =
                new JObject(
                    new JProperty("code", GetResponseCode()),
                    new JProperty("response", Response == String.Empty ? null : Response),
                    new JProperty("entities", Entity)
                );

            return reponse;
        }

        Int16 GetResponseCode() {

            switch (GetTopScoringIntent().Name) {

                case "none":
                    return 11;

                case "justTalk":
                    return 2;

                case "addAlarm":

                    if (Entity == null || Entity["date"] == null || Entity["time"] == null)
                        return 42;
                    else
                        return 41;

                case "addReminder":
                    if (Entity == null || Entity["date"] == null || Entity["time"] == null)
                        return 42;
                    else
                        return 41;

                case "showNews":
                    return 61;

                case "showWeather":
                    return 51;

                case "makeCall":
                    if (Entity == null || Entity["date"] == null || Entity["time"] == null)
                        return 72;
                    else
                        return 71;

                case "playMusic":
                    if (Entity == null || Entity["date"] == null || Entity["time"] == null)
                        return 82;
                    else
                        return 81;

                default:
                    return 0;
            }
        }

        public string GetDebugLog() {

            string debugInfo = "";

            debugInfo += "\nQuery: '" + Query + "'\n";
            debugInfo += $"\nStarlight Response:\n{_nlpResponse.ToString()}\n";
            debugInfo += $"\nPolarisAI Response:\n{GetResponse()}\n";

            return debugInfo;
        }

        [Obsolete("Static cognition is obsolete, please use GetDebugLog() instead")]
        public string GetDebugLogLegacy() {

            string debugInfo = "";

            debugInfo += "Query: '" + Query + "'\n";

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

            return debugInfo;
        }
    }
}

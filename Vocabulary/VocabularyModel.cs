using System;
using System.Collections.Generic;
using System.Linq;

namespace PolarisCore.Vocabulary {

	/// <summary>
	/// Model Class that loads the vocabulary from Resources
	/// </summary>
	public class VocabularyModel {

		public List<String> Verbs { get; } = new List<String>();
		public List<String> Skills { get; } = new List<String>();
		public List<String> Pronouns { get; } = new List<String>();
		public List<String> Adverbs { get; } = new List<String>();
		public List<String> Nouns { get; } = new List<String>();
        public List<String> IntWords { get; } = new List<String>();
        public List<String> IgnoredWords { get; } = new List<String>();
        public List<String> PunctuationMarks { get; } = new List<String>();

        /// <summary>
        /// Loads the vocabulary from Resources once
        /// </summary>
        public VocabularyModel() {

			Verbs = Convert(Properties.Resources.Verbs);
			Skills = Convert(Properties.Resources.Skills);
			Pronouns = Convert(Properties.Resources.Pronouns);
			Adverbs = Convert(Properties.Resources.Adverbs);
			Nouns = Convert(Properties.Resources.Nouns);
            IntWords = Convert(Properties.Resources.Interrogative_Words);
            IgnoredWords = Convert(Properties.Resources.Ignored_Words);
            PunctuationMarks = Convert(Properties.Resources.Punctuation_Marks);
        }

		/// <summary>
		/// Converts a String resource to List<String>
		/// </summary>
		/// <param name="stringToConvert"></param>
		/// <returns></returns>
		private static List<String> Convert(String stringToConvert) {
			List<String> temp = stringToConvert.Split(' ', '\n', '\r', (char)0x2028, (char)0x2029).ToList();
			temp.RemoveAll(String.IsNullOrEmpty);
			return temp;
		}

		public void Debug() {
            Console.WriteLine("\n ------ Vocabulary Debug ------ \n");
            Console.WriteLine("  Nº of Verbs: " + Verbs.Count);
            Console.WriteLine("  Nº of Pronouns: " + Pronouns.Count);
            Console.WriteLine("  Nº of Adverbs: " + Adverbs.Count);
            Console.WriteLine("  Nº of Nouns: " + Nouns.Count);
            Console.WriteLine("  Nº of Skills: " + Skills.Count);
            Console.WriteLine("  Nº of Interrogative Words: " + IntWords.Count);
            Console.WriteLine("\n ----------------------------- \n");
        }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace POLARIS {

	/// <summary>
	/// Model Class that loads the vocabulary from Resources
	/// </summary>
	public class Vocabulary {

		public List<String> Verbs { get; set; } = new List<String>();
		public List<String> Skills { get; set; } = new List<String>();
		public List<String> Pronouns { get; set; } = new List<String>();
		public List<String> Adverbs { get; set; } = new List<String>();
		public List<String> Nouns { get; set; } = new List<String>();
        public List<String> IntWords { get; set; } = new List<String>();
        public List<String> IgnoredWords { get; set; } = new List<String>();
        public List<String> PunctuationMarks { get; set; } = new List<String>();

        /// <summary>
        /// Loads the vocabulary from Resources once
        /// </summary>
        public Vocabulary() {

			Verbs = Convert(POLARIS.Properties.Resources.Verbs);
			Skills = Convert(POLARIS.Properties.Resources.Skills);
			Pronouns = Convert(POLARIS.Properties.Resources.Pronouns);
			Adverbs = Convert(POLARIS.Properties.Resources.Adverbs);
			Nouns = Convert(POLARIS.Properties.Resources.Nouns);
            IntWords = Convert(POLARIS.Properties.Resources.Interrogative_Words);
            IgnoredWords = Convert(POLARIS.Properties.Resources.Ignored_Words);
            PunctuationMarks = Convert(POLARIS.Properties.Resources.Punctuation_Marks);
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

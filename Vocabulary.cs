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
		
		/// <summary>
		/// Loads the vocabulary from Resources once
		/// </summary>
		public Vocabulary() {

			Verbs = Convert(POLARIS.Properties.Resources.Verbs);
			Skills = Convert(POLARIS.Properties.Resources.Skills);
			Pronouns = Convert(POLARIS.Properties.Resources.Pronouns);
			Adverbs = Convert(POLARIS.Properties.Resources.Adverbs);
			Nouns = Convert(POLARIS.Properties.Resources.Nouns);
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
			Console.WriteLine("\nVocabulary Debug ->\n   Nº of Verbs: " + Verbs.Count + "\n   Nº of Pronouns: " + Pronouns.Count + "\n   Nº of Adverbs: " + Adverbs.Count + "\n");
		}
	}
}

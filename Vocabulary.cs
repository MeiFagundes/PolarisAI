using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLARIS {
	public class Vocabulary {

		private List<String> verbs;
		private List<String> skills;
		private List<String> pronouns;
		private List<String> adverbs;
		private List<String> nouns;

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
		public static List<String> Convert(String stringToConvert) {
			List<String> temp = stringToConvert.Split(' ', '\n', '\r', (char)0x2028, (char)0x2029).ToList();
			temp.RemoveAll(String.IsNullOrEmpty);
			return temp;
		}

		public void Debug() {
			Console.WriteLine("\nVocabulary Debug ->\n   Nº of Verbs: " + Verbs.Count + "\n   Nº of Pronouns: " + Pronouns.Count + "\n   Nº of Adverbs: " + Adverbs.Count + "\n");
		}

		public List<String> Verbs { get => verbs; set => verbs = value; }
		public List<String> Skills { get => skills; set => skills = value; }
		public List<String> Pronouns { get => pronouns; set => pronouns = value; }
		public List<String> Adverbs { get => adverbs; set => adverbs = value; }
		public List<String> Nouns { get => nouns; set => nouns = value; }
	}
}

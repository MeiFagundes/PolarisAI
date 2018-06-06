using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLARIS {
	public abstract class Phrase {

		// --- VARIABLES ---
		public String[] verbsFile = File.ReadAllLines("Vocabulary/Verbs.txt", Encoding.UTF8);
		public String[] actionVerbsFile = File.ReadAllLines("Vocabulary/ActionVerbs.txt", Encoding.UTF8);
		public String[] pronounsFile = File.ReadAllLines("Vocabulary/Pronouns.txt", Encoding.UTF8);
		public String[] adverbsFile = File.ReadAllLines("Vocabulary/Adverbs.txt", Encoding.UTF8);
		public List<String> phrase = new List<String>();

		// --- CONSTRUCTORS ---
		public Phrase(String input) {
			
		}
		public Phrase() {}

		// --- METHODS ---

		public abstract void Debug();
	}
}

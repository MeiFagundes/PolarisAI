using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLARIS {
	public abstract class Phrase {

		// --- VARIABLES ---
		public String[] verbs = File.ReadAllLines("Vocabulary/Verbs.txt", Encoding.UTF8);
		public String[] pronouns = File.ReadAllLines("Vocabulary/Pronouns.txt", Encoding.UTF8);
		public String[] phraseIn;
		
		// --- CONSTRUCTORS ---
		public Phrase(String input) {
			
		}
		public Phrase() {}

		// --- METHODS ---

		public abstract void Debug();
	}
}

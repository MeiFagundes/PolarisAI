using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLARIS {
	class Core {
		static void Main(string[] args) {

			Dialog dialog;
			String input = "Polaris, turn on the lights please.";

			while (true) {
				Console.WriteLine("'" + input + "'");
				dialog = new Dialog(input);
				dialog.Debug();

				Console.WriteLine("Type something: ");
				input = Console.ReadLine();
				Console.Clear();
			}
		}
	}
}

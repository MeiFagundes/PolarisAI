using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLARIS {
	class Core {
		static void Main(string[] args) {

			while (true) {
				Console.WriteLine("Type something: ");
				Dialog dialog = new Dialog(Console.ReadLine());
				dialog.Debug();

				Console.WriteLine("\n ----- Press Enter to try again... -----");
				Console.ReadLine();
				Console.Clear();
			}
			
			//System.Threading.Thread.Sleep(60000);
		}
	}
}

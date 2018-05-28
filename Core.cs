using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLARIS {
	class Core {
		static void Main(string[] args) {

			Console.WriteLine("Type something: ");
			Dialog dialog = new Dialog(Console.ReadLine());
			
			dialog.Debug();
			
			System.Threading.Thread.Sleep(60000);
		}
	}
}

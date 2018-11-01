using System;

namespace POLARIS.Skills {
	public static class Search {
		
        /// <summary>
        /// Searches something on Google in the Default browser.
        /// </summary>
        /// <param name="dialog"></param>
		public static void Execute(Dialog dialog){

            if (dialog.Phrase[dialog.VerbsIndex[0] + 1] == "about")
                dialog.Phrase.Remove("about");

            if (dialog.Phrase[dialog.Phrase.Count - 1] == "please")
                dialog.Phrase.Remove("please");

            // Formatting search query to insert it into google search parameters
            String search = String.Join("+",
                dialog.Phrase.GetRange(dialog.VerbsIndex[0] + 1, dialog.Phrase.Count - 1));

            System.Diagnostics.Process.Start("http://google.com/search?q=" + search);
        }
	}
}

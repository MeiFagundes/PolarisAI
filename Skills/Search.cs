using System;

namespace PolarisCore.Skills {
	public static class Search {
		
        /// <summary>
        /// Searches something on Google in the Default browser.
        /// </summary>
        /// <param name="d"></param>
		public static void Execute(Dialog d){

            if (d.Phrase[d.VerbsIndex[0] + 1] == "about")
                d.Phrase.Remove("about");

            if (d.Phrase[d.Phrase.Count - 1] == "please")
                d.Phrase.Remove("please");

            // Formatting search query to insert it into google search parameters
            String search = String.Join(" ",
                d.Phrase.GetRange(d.VerbsIndex[0] + 1, d.Phrase.Count - 1));

            d.Response = "Alright! Searching '" + search + "' for you.";
            
            System.Diagnostics.Process.Start("http://google.com/search?q=" + search.Replace(" ", "+"));
        }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarisCore.Response {
    public static class Question {

        public static void SetResponse(Dialog d) {

            if (d.Phrase.Exists(t => t.Equals("can"))) {

                d.Response = "Well, maybe I can. I don't know it yet...";
                return;
            }

            d.Response = "Sorry, I don't know how to interpret generic questions yet.";
        }
    }
}

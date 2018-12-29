using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarisCore.Response {
    public static class ResponseCore {

        public static void GenerateResponse(Dialog d) {

            if (d.IsRequest)
                Response.Request.SetResponse(d);
            else if (d.IsQuestion)
                Response.Question.SetResponse(d);
        }
    }
}

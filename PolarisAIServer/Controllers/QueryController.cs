using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PolarisAIServer.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class QueryController : ControllerBase {
        // GET query
        [HttpGet]
        public ActionResult<string> Get() {
            return "Query is null.\nPlease use:\n[domain]/query/Search about cute kittens please";
        }

        // GET query/do that
        [HttpGet("{query}")]
        public ActionResult<string> GetQuery(string query) {
            return PolarisAICore.PolarisAICore.Cognize(query, true);
        }
    }
}

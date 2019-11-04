using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace PolarisAIServer.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class QueryController : ControllerBase {

        // GET query
        [HttpGet]
        public ActionResult<string> Get() {
            return "Query is null";
        }

        // GET query/do that
        [HttpGet("{query}")]
        public ActionResult<JObject> GetQuery(string query) {
            return PolarisAICore.PolarisAICore.Cognize(query);
        }
    }
}

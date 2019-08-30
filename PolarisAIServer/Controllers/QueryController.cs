using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<string> GetQuery(string query) {
            return PolarisAICore.PolarisAICore.Cognize(query, true);
        }
    }
}

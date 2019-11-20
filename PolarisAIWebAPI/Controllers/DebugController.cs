using Microsoft.AspNetCore.Mvc;

namespace PolarisAIWebAPI.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class DebugController : ControllerBase {

        // GET query
        [HttpGet]
        public ActionResult<string> Get() {
            return "Query is null";
        }

        // GET debug/do that
        [HttpGet("{query}")]
        public ActionResult<string> GetDebug(string query) {
            return PolarisAICore.PolarisAICore.CognizeDebug(query);
        }
    }
}

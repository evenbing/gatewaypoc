using System.Collections.Generic;
using System.Web.Http;

namespace Router
{
    public class RouteController : ApiController
    {
        [Route("route")]
        [HttpPost]
        public IHttpActionResult UpdateRoute([FromBody]IDictionary<string, string> routeTable)
        {
            Startup.RoutingTable = routeTable;
            return Ok();
        }
    }
}
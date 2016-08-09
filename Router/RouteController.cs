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
            // validate routes... Remove trailing slash (https://google.com is ok, http://vg.no/ is not)
            Startup.RoutingTable = routeTable;
            return Ok();
        }
    }
}
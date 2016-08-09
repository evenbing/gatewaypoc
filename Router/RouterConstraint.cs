using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace Router
{
    public class RouterConstraint: IHttpRouteConstraint {
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values,
            HttpRouteDirection routeDirection)
        {
            return !request.RequestUri.PathAndQuery.StartsWith("/route");
        }
    }
}
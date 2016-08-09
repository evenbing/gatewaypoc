using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using Owin;

namespace Router
{
    public class Startup
    {
        public static IDictionary<string, string> RoutingTable = new Dictionary<string, string>();

        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("Proxy", "{*path}",
                handler: HttpClientFactory.CreatePipeline
                    (
                        innerHandler: new HttpClientHandler(),
                        handlers: new DelegatingHandler[] {new RouterHandler()}
                    ),
                defaults: new {path = RouteParameter.Optional},
                constraints: null
                );

            appBuilder.UseWebApi(config);
        }
    }
}
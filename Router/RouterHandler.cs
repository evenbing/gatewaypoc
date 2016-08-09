using System;
using System.Net;
using System.Net.Http;

namespace Router
{
    public class RouterHandler : DelegatingHandler
    {
        protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            System.Threading.CancellationToken cancellationToken)
        {
            var routeKey = request.RequestUri.Segments[1];
            if (!Startup.RoutingTable.ContainsKey(routeKey))
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            var routeUri = Startup.RoutingTable[routeKey];

            var forwardUriAsString = $"{routeUri}/{request.RequestUri.PathAndQuery}";
            request.RequestUri = new Uri(forwardUriAsString);
            var client = new HttpClient();
            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            return response;
        }
    }
}
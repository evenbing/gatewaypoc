using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Router
{
    public class RouterHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var routeKey = request.RequestUri.Segments[1];
            if (!Startup.RoutingTable.ContainsKey(routeKey))
            {
                var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                var content = new StringContent("Unknown route", Encoding.UTF8, "application/text");
                httpResponseMessage.Content = content;
                return httpResponseMessage;
            }

            var routeUri = Startup.RoutingTable[routeKey];

            var query = request.RequestUri.PathAndQuery.Replace(routeKey, string.Empty);
            var forwardUriAsString = $"{routeUri}{query}";
            request.RequestUri = new Uri(forwardUriAsString);
            var client = new HttpClient();
            if (request.Method == HttpMethod.Get)
                request.Content = null;
            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            return response;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;

namespace Router
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(baseAddress))
            {
                var client = new HttpClient();
                var routes = new Dictionary<string, string>
                {
                    ["template"] = "http://vg.no",
                    ["search"] = "http://google.com"
                };

                var json = JsonConvert.SerializeObject(routes);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsync(baseAddress + "route", content).Result;
                WriteResponse(response);
                var invalid = client.GetAsync(baseAddress + "invalid").Result;
                WriteResponse(invalid);
                var templateResponse = client.GetAsync(baseAddress + "template").Result;
                WriteResponse(templateResponse);
            }

            Console.ReadLine();
        }

        private static void WriteResponse(HttpResponseMessage response)
        {
            Console.WriteLine(response);
            var result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result.Substring(0, result.Length > 200 ? 200 : result.Length));
        }
    }
}
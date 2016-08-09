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
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                var client = new HttpClient();
                var routes = new Dictionary<string, string>
                {
                    ["template"] = "http://templateservice/",
                    ["search"] = "http://searchservice/"
                };

                var json = JsonConvert.SerializeObject(routes);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsync(baseAddress + "route", content).Result;

                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                var templateResponse = client.GetAsync(baseAddress + "template").Result;
            }

            Console.ReadLine();
        }
    }
}
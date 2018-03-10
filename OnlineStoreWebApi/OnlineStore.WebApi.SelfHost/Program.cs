using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.WebApi.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            string port = "9000";
            string baseAddress = "http://localhost:" + port;

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                //HttpClient client = new HttpClient();

                //var response = client.GetAsync(baseAddress + "/api/products/0/1").Result;

                //System.Console.WriteLine(response);
                //System.Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.WriteLine("Listening port: " + port);
                System.Console.ReadLine();
            }
        }
    }
}

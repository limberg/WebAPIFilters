using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace ConsoleAppAuthenticationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://localhost:44361/api/basicauthentication";

            string inputKeys = "male:male";
            byte[] arrayInputKeys = Encoding.ASCII.GetBytes(inputKeys);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Convert.ToBase64String(arrayInputKeys));

                var result = client.GetAsync(url).Result;

                if (result.IsSuccessStatusCode)
                {
                    var serializedResult = result.Content.ReadAsStringAsync().Result;
                    var res = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(serializedResult);

                    Console.WriteLine(res);
                }
            }
        }
    }
}

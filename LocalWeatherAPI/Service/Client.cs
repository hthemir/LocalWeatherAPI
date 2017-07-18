using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Client
    {
        static HttpClient client = new HttpClient();

        public static async Task<Object> getData<T>(string url)
        {
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                var error = JObject.Parse(await response.Content.ReadAsStringAsync())["error"];
                return JsonConvert.DeserializeObject<Error>(error.ToString());
            }
        }
    }
}

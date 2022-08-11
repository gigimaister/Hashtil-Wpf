using Hashtil_Jobs_For_Drivers.Models;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Hashtil_Jobs_For_Drivers.Heplers
{
    public class ApiHelper
    {
        public static HttpClient? ApiClient { get; set; }

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
        public static async Task<User> Login(string username, string password)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync($"{Constants.Url.Login}username={username}&password={password}");
            return JsonConvert.DeserializeObject<User>(response);
        }
    }
}

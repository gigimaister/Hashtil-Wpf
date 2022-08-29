using Hashtil_Jobs_For_Drivers.Models;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;

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
        public static async Task<List<User>>Login(string username, string password)
        {
            string url = $"http://www.hashtildb.pe.hu/LoginMapTable.php?username={username}&password={password}";


            using (HttpResponseMessage response = await HttpReq.ApiClient.GetAsync(url))
            {
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var user = await response.Content.ReadAsStringAsync();
                        var j = JsonConvert.DeserializeObject<List<User>>(user);
                        return j;
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw new Exception();
                }
                
            }
        }
        // Get From Php Green House Data
        public static async Task<List<DashBoardDataPhp>> GetGreenHouseDataMySql()
        {
            using (HttpResponseMessage response = await HttpReq.ApiClient.GetAsync(Constants.Url.PhpGreenHouseData))
            {
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        var greenData = JsonConvert.DeserializeObject<List<DashBoardDataPhp>>(data);
                        return greenData;
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw new Exception();
                }

            }
        }

        // Get Drivers Info
        public static async Task<List<Driver>> GetDrivers()
        {
            using (HttpResponseMessage response = await HttpReq.ApiClient.GetAsync(Constants.Url.PhpDriversData))
            {
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        var drivers = JsonConvert.DeserializeObject<List<Driver>>(data);
                        return drivers;
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw new Exception();
                }

            }
        }
    }
}

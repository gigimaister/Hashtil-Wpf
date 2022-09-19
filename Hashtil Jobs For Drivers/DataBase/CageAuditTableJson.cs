using Hashtil_Jobs_For_Drivers.Heplers;
using Hashtil_Jobs_For_Drivers.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hashtil_Jobs_For_Drivers.DataBase
{
    public class CageAuditTableJson
    {
        string url = "";
        public static async Task<ObservableCollection<CageAudit>> CageAuditFetchJson(int query_selector)
        {
            string url = $"http://hashtildb.pe.hu/cage_audit_table_jason.php?query_selector={query_selector}";


            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var userLogins = await response.Content.ReadAsStringAsync();


                        return JsonConvert.DeserializeObject<ObservableCollection<CageAudit>>(userLogins);


                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
                catch (Exception e)
                {

                    throw new Exception();
                }
            }

        }
    }
}

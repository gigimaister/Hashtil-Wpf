using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Hashtil_Jobs_For_Drivers.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Hashtil_Jobs_For_Drivers.Heplers
{
    static class GSheetsHelper
    {
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static readonly string ApplicationName = "JobsForDrivers";
        static readonly string SpreadsheetId = "1OW516xac_aL82ecobJSq5Xmmo09UQ6jzptTIHehJuYY";
        static readonly string Sheet = "טבלת הוצאות";
        static SheetsService SheetsService;


        static  GSheetsHelper()
        {
            GoogleCredential googleCredential;
            using(var stream =  new FileStream("hashtil-job-table-wpf.json", FileMode.Open, FileAccess.Read))
            {
                googleCredential = GoogleCredential.FromStream(stream)
                    .CreateScoped(Scopes);

            }

            SheetsService = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = googleCredential,
                ApplicationName = ApplicationName,
            });
        }

        // Read Sheet 
        public static Task<List<Order>> ReadEntries()
        {
            List<Order> Orders = new List<Order>();
            var range = $"{Sheet}!A:S";
            var request =  SheetsService.Spreadsheets.Values.Get(SpreadsheetId, range);

            var response = request.Execute();
            var values = response.Values;
            if(values != null && values.Count > 0)
            {
                

                // In Every Row Make Obj And Add To Order List For More Operetions
                foreach (var row in values)
                {
                    try
                    {
                        Order order = new Order();

                        order.Date = Convert.ToDateTime(row[0]);
                        order.Driver = (string)row[1];
                        order.Cx = (string)row[2];
                        order.Gidul = (string)row[3];
                        order.Zan = (string)row[4];
                        order.Plants = Convert.ToDouble(row[5]);
                        order.Magash = Convert.ToDouble(row[6]);
                        order.Passport = (string)(row[7]);
                        order.Avarage = Convert.ToDouble(row[8]);
                        order.Status = (string)row[9];
                        if(!string.IsNullOrEmpty((string)row[10])) 
                        { 
                            order.Cages = Convert.ToDouble(row[10]);
                        }
                        else
                        {
                            order.Cages = 0;
                        }
                        order.Remarks = (string)row[11];
                        order.HasCertificate = (string)row[12];
                        order.CreationDate = Convert.ToDateTime(row[18]);
                        

                        Orders.Add(order);
                    }
                    catch
                    {
                        continue;
                    }
                   

                }               
            }
            else
            {
                
            }
            return Task.FromResult(Orders);
        }

    }
}

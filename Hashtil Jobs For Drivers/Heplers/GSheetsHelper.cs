using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Hashtil_Jobs_For_Drivers.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        /// <summary>
        /// Read Google Sheet And Return List Of Orders Objects
        /// </summary>
        /// <returns></returns>
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
                    Order order = new Order();
                    try
                    {
                        

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

                        // If Remarks Null
                        if (row.Count < 12)
                        {
                            Orders.Add(order);
                            continue;
                        }
                        else
                        {
                            order.HasCertificate = (string)row[12];
                        }
                       
                        
                        // If Timestamp Is Null
                        if(row.Count < 18)
                        {
                            Orders.Add(order);
                            continue;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty((string)row[18]))
                            {
                                order.CreationDate = Convert.ToDateTime(row[18]);
                            }
                            else
                            {
                                order.CreationDate = DateTime.Now.AddDays(1);
                            }

                        }

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

        // Return DashBoard From G Sheets
        /// <summary>
        /// return DashBoardData object
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public static  Task<DashBoardData> DashBoardData(List<Order> orders)
        {
            DashBoardData data = new DashBoardData();
            data.DailyJobsChartList = new List<DailyJobsChart>();

            // Tommorrow
            data.NumOfMagashForTommorrow = (int)orders.Where(x => x.Date == DateTime.Today.AddDays(1)).Sum(X => X.Magash);
            data.NumOfPlantsForTommorrow = (int)orders.Where(x => x.Date == DateTime.Today.AddDays(1)).Sum(x => x.Plants);
            data.NumOfCagesForTommorrow = (int)orders.Where(x => x.Date == DateTime.Today.AddDays(1)).Sum(x => x.Cages);
            data.NumOfOrdersForTommorrow = orders.Where(x => x.Date == DateTime.Today.AddDays(1)).Count();

            // Today
            data.NumOfMagashForToday = (int)orders.Where(x => x.Date == DateTime.Today).Sum(X => X.Magash);
            data.NumOfPlantsForToday = (int)orders.Where(x => x.Date == DateTime.Today).Sum(x => x.Plants);
            data.NumOfCagesForToday = (int)orders.Where(x => x.Date == DateTime.Today).Sum(x => x.Cages);
            data.NumOfOrdersForToday = orders.Where(x => x.Date == DateTime.Today).Count();

            data.OrdersEnteredTodayForToday = orders.Where(x => x.CreationDate == DateTime.Today).Count();

            // DailyJobs List
            var ordersByDate = orders.GroupBy(x => x.Date);
            foreach(var order in ordersByDate.ToList())
            {
                var dailyJobchrt = new DailyJobsChart();
                dailyJobchrt.DayOfWeek = TranslationHelper.GethebrewDayOfTheWeek(order.FirstOrDefault().Date);
                dailyJobchrt.Treys = (int)order.Sum(x => x.Magash);
                data.DailyJobsChartList.Add(dailyJobchrt);
            }

            return Task.FromResult(data);


        }

        // Return Delivery Line Sum Up  From G Sheets
        /// <summary>
        /// Return Delivery Line Sum Up  From G Sheets
        /// </summary>
        /// <param name="orders"></param>
        /// <returns>
        /// List<DeliveryLineStatus>
        /// </returns>
        public static Task<List<DeliveryLineStatus>> GetLinesSumUp(List<Order> orders)
        {
            var delLineList = new List<DeliveryLineStatus>();
            orders = orders.Where(x => x.Date == DateTime.Today.AddDays(1)).ToList();

            // Get Total Line By Grouping
            var totalOfLines = orders.GroupBy(x => x.Driver).ToList();

            foreach(var line in totalOfLines) 
            {
                try
                {
                    var dLine = new DeliveryLineStatus();
                    dLine.LineNum = Convert.ToInt32(line.FirstOrDefault().Driver);
                    dLine.NumOfCx = orders.Where(x => x.Driver == dLine.LineNum.ToString()).ToList().Count();
                    dLine.Orders = orders.Where(x => x.Driver == dLine.LineNum.ToString()).ToList();
                    dLine.NumOfCages = orders.Sum(x => Convert.ToInt32(x.Cages));
                    delLineList.Add(dLine);
                }
                catch
                {
                    continue;
                }
              
            }



            return Task.FromResult(delLineList);
        }

    }
}

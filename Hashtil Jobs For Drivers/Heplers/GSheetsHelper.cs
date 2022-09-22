using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Hashtil_Jobs_For_Drivers.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hashtil_Jobs_For_Drivers.Heplers
{
    static class GSheetsHelper
    {
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static readonly string ApplicationName = "JobsForDrivers";
        static readonly string SpreadsheetId = "1OW516xac_aL82ecobJSq5Xmmo09UQ6jzptTIHehJuYY";
        static readonly string Sheet = "טבלת הוצאות";
        static readonly string RoiSheet = "ROI";
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
            try
            {
                var response = request.Execute();
                var values = response.Values;
                if (values != null && values.Count > 0)
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
                            // Prevent Exception When Len Of Row Is 9
                            if (row.Count > 11)
                            {
                                if (!string.IsNullOrEmpty((string)row[10]))
                                {
                                    order.Cages = Convert.ToDouble(row[10]);
                                }
                                else
                                {
                                    order.Cages = 0;
                                }
                            }
                            else
                            {
                                Orders.Add(order);
                                continue;
                            }
                            order.Remarks = (string)row[11];

                            // If Remarks Null
                            if (row.Count < 13)
                            {
                                Orders.Add(order);
                                continue;
                            }
                            else
                            {
                                order.HasCertificate = (string)row[12];
                            }


                            // If Timestamp Is Null
                            if (row.Count < 18)
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
            }
            catch
            {
                ReadEntries();
            }
            
          

            return Task.FromResult(Orders);
        }

        // Read Roi Line Sheet 
        /// <summary>
        /// Read Google Sheet And Return List Of DeliveryLineStatus object
        /// </summary>
        /// <returns></returns>
        public static Task<List<DeliveryLineStatus>> ReadLineSheet()
        {

            List<DeliveryLineStatus> Orders = new List<DeliveryLineStatus>();
            List<Driver> Drivers = new List<Driver>();

            var range = $"{RoiSheet}!A:M";
            var request = SheetsService.Spreadsheets.Values.Get(SpreadsheetId, range);
            try
            {
                var response = request.Execute();
                var values = response.Values;
                if (values != null && values.Count > 0)
                {

                    // In Every Row Make Obj And Add To Order List For More Operetions
                    foreach (var row in values)
                    {
                        try
                        {
                            // If We Have Split Job
                            if (row.Count > 3)
                            {
                                if ((string)row[3] == "TRUE")
                                {
                                    DeliveryLineStatus deliveryLineStatus = new DeliveryLineStatus();

                                    switch (row.Count)
                                    {
                                        // 2 Drivers
                                        case 10:
                                            try
                                            {
                                                deliveryLineStatus.LineNum = Convert.ToInt32(row[1]);
                                                deliveryLineStatus.DeliveryDate = Convert.ToDateTime(row[0]);
                                                deliveryLineStatus.Driver = new List<Driver>();
                                                // Driver 1
                                                var driver = new Driver();
                                                driver.FullName = (string?)row[4];
                                                driver.SplitMagash = Convert.ToInt32(row[5]);
                                                driver.SplitCage = Convert.ToInt32(row[6]);
                                                deliveryLineStatus.Driver.Add(driver);
                                                // Driver 2
                                                var driver2 = new Driver();
                                                driver2.FullName = (string?)row[7];
                                                driver2.SplitMagash = Convert.ToInt32(row[8]);
                                                driver2.SplitCage =  Convert.ToInt32(row[9]);
                                                deliveryLineStatus.Driver.Add(driver2);
                                                Orders.Add(deliveryLineStatus);
                                            }
                                            catch
                                            {
                                                continue;
                                            }
                                            break;
                                        // 3 Drivers
                                        case 13:
                                            try
                                            {
                                                // Driver 1
                                                var driver = new Driver();
                                                driver.FullName = (string?)row[7];
                                                driver.SplitMagash =  Convert.ToInt32(row[8]);
                                                driver.SplitCage =  Convert.ToInt32(row[9]);
                                                deliveryLineStatus.Driver.Add(driver);
                                                // Driver 2
                                                var driver2 = new Driver();
                                                driver2.FullName = (string?)row[10];
                                                driver2.SplitMagash =  Convert.ToInt32(row[11]);
                                                driver2.SplitCage =  Convert.ToInt32(row[12]);
                                                deliveryLineStatus.Driver.Add(driver2);
                                                Orders.Add(deliveryLineStatus);
                                                // Driver 3
                                                var driver3 = new Driver();
                                                driver2.FullName = (string?)row[10];
                                                driver2.SplitMagash = (int)row[11];
                                                driver2.SplitCage = (int)row[12];
                                                deliveryLineStatus.Driver.Add(driver3);
                                                Orders.Add(deliveryLineStatus);
                                            }
                                            catch
                                            {
                                                continue;
                                            }
                                            break;
                                    }
                                }
                            }
                            // If NO SPLIT JOB
                            else
                            {
                                DeliveryLineStatus deliveryLineStatus = new DeliveryLineStatus();
                                try
                                {
                                    deliveryLineStatus.LineNum = Convert.ToInt32(row[1]);
                                    deliveryLineStatus.DeliveryDate = Convert.ToDateTime(row[0]);
                                    deliveryLineStatus.Driver = new List<Driver>();
                                    var d = new Driver();
                                    deliveryLineStatus.Driver.Add(d);
                                    deliveryLineStatus.Driver.FirstOrDefault().FullName = row[2].ToString();
                                    Orders.Add(deliveryLineStatus);
                                }
                                catch
                                {
                                    continue;
                                }
                            }
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
            }
            catch
            {
                ReadLineSheet();
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
            data.FinishJobs = orders.Where(x => x.Status == "מ" && x.Date == DateTime.Today.AddDays(1)).Count();

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
        public static Task<List<DeliveryLineStatus>> GetLinesSumUp(List<Order> orders, List<DeliveryLineStatus> deliveryLineStatuses, List<Driver> drivers)
        {
            // Get Todays Jobs To Later Analyze
            var todaysOrders = orders.Where(x => x.Date == DateTime.Today);

            // Get Only Tommorows Jobs
            orders = orders.Where(x => x.Date == DateTime.Today.AddDays(1)).ToList();

            // Group By Line Num
            var ordersByline = orders.GroupBy(x => x.Driver).ToList();

            // List Of Lines To Store The Created Lines
            var tempLines = new List<DeliveryLineStatus>();

            // In Every Line
            foreach(var order in ordersByline)
            {
                try
                {
                    var delLine = new DeliveryLineStatus();

                    delLine.LineNum = Convert.ToInt32(order.FirstOrDefault().Driver);
                    delLine.Orders = order.ToList();
                    delLine.DeliveryDate = DateTime.Today.AddDays(1);
                    delLine.NumOfCages = order.Select(x=>x).ToList().Sum(x=> Convert.ToInt32(x.Cages));
                    delLine.NumOfCx = order.GroupBy(x => x.Cx).Count();

                    tempLines.Add(delLine);

                }
                catch
                {
                    continue;
                }
            }

            // Check If We Have Any Data In Roi's Sheet
            //If DeliveryLineStatus != Null
            if (deliveryLineStatuses is not null || deliveryLineStatuses.Count() > 0)
            {
                foreach(var line in deliveryLineStatuses)
                {
                    // If We Have Split Line/Job
                    if (line.Driver.Count > 1)
                    {
                        try
                        {
                            // If Date == Today
                            if (line.DeliveryDate == DateTime.Today)
                            {
                                //If Line == 0
                                if (line.LineNum == 0)
                                {
                                    var driver = new Driver();
                                    // get driver
                                    foreach (var dr in drivers)
                                    {
                                        foreach(var dr2 in line.Driver)
                                        {
                                            if(dr.FullName == dr2.FullName)
                                            {
                                                dr.SplitMagash = dr2.SplitMagash;
                                                dr.SplitCage = dr2.SplitCage;
                                                driver = dr;
                                            }
                                        }
                                    }
                                    //set line driver to full driver object
                                    line.Driver.Add(driver);
                                    // todays orders only with this driver name
                                    var combinedLineorders = todaysOrders.Where(x => x.Driver == driver.TableName).ToList();
                                    // add new Line 
                                    var newLine = new DeliveryLineStatus();
                                    newLine.Driver = new List<Driver>();
                                    newLine.Driver.Add(driver);

                                    newLine.DeliveryDate = DateTime.Today;
                                    newLine.Orders = combinedLineorders;
                                    newLine.LineName = Constants.Hebrew.LineTodaysName;
                                    newLine.SplitLineAlert = Constants.Hebrew.IsSplitLine;
                                    newLine.NumOfCx = combinedLineorders.GroupBy(x => x.Cx).Count();
                                    newLine.NumOfCages = combinedLineorders.Sum(x => Convert.ToInt32(x.Cages));
                                    tempLines.Add(newLine);
                                }
                                // if joined line for today
                                else
                                {
                                    var driver = new Driver();
                                    // get driver
                                    foreach (var dr in drivers)
                                    {
                                        foreach (var dr2 in line.Driver)
                                        {
                                            if (dr.FullName == dr2.FullName)
                                            {
                                                dr.SplitMagash = dr2.SplitMagash;
                                                dr.SplitCage = dr2.SplitCage;
                                                driver = dr;
                                            }
                                        }
                                    }
                                    //set line driver to full driver object
                                    line.Driver.Add(driver);
                                    // find the line group and add the order to orders                              
                                    var combinedLineorders = todaysOrders.Where(x => x.Driver == driver.TableName).ToList(); ;
                                    foreach (var templine in tempLines)
                                    {
                                        if (templine.LineNum == line.LineNum)
                                        {
                                            foreach (var combLine in combinedLineorders)
                                            {
                                                templine.Orders.Add(combLine);
                                            }
                                        }
                                    }

                                }
                            }
                            // if date == tommorrow
                            if (line.DeliveryDate == DateTime.Today.AddDays(1))
                            {
                                foreach (var templine in tempLines)
                                {
                                    if (templine.LineNum == line.LineNum)
                                    {
                                        var driver = new Driver();
                                        templine.Driver = new List<Driver>();
                                        // get driver
                                        foreach (var dr in drivers)
                                        {
                                            foreach (var dr2 in line.Driver)
                                            {
                                                if (dr.FullName == dr2.FullName)
                                                {
                                                    dr.SplitMagash = dr2.SplitMagash;
                                                    dr.SplitCage = dr2.SplitCage;
                                                    dr2.TableName = dr.TableName;
                                                    dr2.PhoneNum = dr.PhoneNum;
                                                    driver = dr;
                                                    //set line driver to full driver object
                                                    templine.Driver.Add(driver);
                                                }
                                            }
                                        }
                                        
                                                 
                                    }                                 
                                }
                                // Create Duplicate Line For Every Driver
                               // 2 Driver
                               if(line.Driver.Count == 2)
                                {
                                    foreach(var dr in line.Driver)
                                    {
                                        // add new Line 
                                        var newLine = new DeliveryLineStatus();
                                        newLine.Driver = new List<Driver>();
                                        newLine.Driver.Add(drivers.Where(x=>x.FullName == line.Driver.FirstOrDefault().FullName).FirstOrDefault());
                                        newLine.LineNum = line.LineNum;
                                        newLine.DeliveryDate = DateTime.Today.AddDays(1);
                                        newLine.Orders = orders.Where(x => x.Driver == line.LineNum.ToString()).ToList();
                                        //newLine.LineName = Constants.Hebrew.LineTodaysName;
                                        newLine.SplitLineAlert = Constants.Hebrew.IsSplitLine;
                                        newLine.NumOfCx = newLine.Orders.GroupBy(x => x.Cx).Count();
                                        newLine.NumOfCages = newLine.Orders.Sum(x => Convert.ToInt32(x.Cages));
                                        tempLines.Where(x => x.LineNum == newLine.LineNum).FirstOrDefault().Driver.RemoveAll(x => x.FullName == newLine.Driver.FirstOrDefault().FullName);
                                        tempLines.Add(newLine);
                                        break;
                                    }
                                }
                                // 3 Drivers
                                if (line.Driver.Count == 3)
                                {
                                    foreach (var dr in line.Driver)
                                    {
                                        // add new Line 
                                        var newLine = new DeliveryLineStatus();
                                        newLine.Driver = new List<Driver>();
                                        newLine.Driver.Add(line.Driver.FirstOrDefault());

                                        newLine.DeliveryDate = DateTime.Today;
                                        newLine.Orders = orders.Where(x => x.Driver == line.LineNum.ToString()).ToList();
                                        newLine.LineName = Constants.Hebrew.LineTodaysName;
                                        newLine.SplitLineAlert = Constants.Hebrew.IsSplitLine;
                                        newLine.NumOfCx = newLine.Orders.GroupBy(x => x.Cx).Count();
                                        newLine.NumOfCages = newLine.Orders.Sum(x => Convert.ToInt32(x.Cages));
                                        tempLines.Add(newLine);
                                        line.Driver.Remove(newLine.Driver.FirstOrDefault());
                                        break;
                                    }
                                    foreach (var dr in line.Driver)
                                    {
                                        // add new Line 
                                        var newLine = new DeliveryLineStatus();
                                        newLine.Driver = new List<Driver>();
                                        newLine.Driver.Add(line.Driver.FirstOrDefault());

                                        newLine.DeliveryDate = DateTime.Today;
                                        newLine.Orders = line.Orders;
                                        newLine.LineName = Constants.Hebrew.LineTodaysName;
                                        newLine.SplitLineAlert = Constants.Hebrew.IsSplitLine;
                                        newLine.NumOfCx = newLine.Orders.GroupBy(x => x.Cx).Count();
                                        newLine.NumOfCages = newLine.Orders.Sum(x => Convert.ToInt32(x.Cages));
                                        tempLines.Add(newLine);
                                        line.Driver.Remove(newLine.Driver.FirstOrDefault());
                                        break;
                                    }

                                }
                            }
                        }
                        catch
                        {

                        }
                    }
                    // If Regular Line
                    else
                    {
                        try
                        {
                            // If Date == Today
                            if (line.DeliveryDate == DateTime.Today)
                            {
                                //If Line == 0
                                if (line.LineNum == 0)
                                {
                                    // get driver
                                    var driver = drivers.Where(x => x.FullName == line.Driver.FirstOrDefault().FullName).ToList();
                                    //set line driver to full driver object
                                    line.Driver = driver;
                                    // todays orders only with this driver name
                                    var combinedLineorders = todaysOrders.Where(x => x.Driver == line.Driver.FirstOrDefault().TableName).ToList();
                                    // add new Line 
                                    var newLine = new DeliveryLineStatus();
                                    newLine.Driver = driver;
                                    newLine.DeliveryDate = DateTime.Today;
                                    newLine.Orders = combinedLineorders;
                                    newLine.LineName = Constants.Hebrew.LineTodaysName;
                                    newLine.NumOfCx = combinedLineorders.GroupBy(x => x.Cx).Count();
                                    newLine.NumOfCages = combinedLineorders.Sum(x => Convert.ToInt32(x.Cages));
                                    tempLines.Add(newLine);
                                }
                                // if joined line for Tommorrow
                                else
                                {
                                    // get driver
                                    var driver = drivers.Where(x => x.FullName == line.Driver.FirstOrDefault().FullName).ToList();
                                    //set line driver to full driver object
                                    line.Driver = driver;
                                    // find the line group and add the order to orders                              
                                    var combinedLineorders = todaysOrders.Where(x => x.Driver == line.Driver.FirstOrDefault().TableName).ToList(); ;
                                    foreach (var templine in tempLines)
                                    {
                                        if (templine.LineNum == line.LineNum)
                                        {
                                            foreach (var combLine in combinedLineorders)
                                            {
                                                templine.Orders.Add(combLine);
                                            }
                                        }
                                    }

                                }
                            }
                            // if date == tommorrow
                            if (line.DeliveryDate == DateTime.Today.AddDays(1))
                            {
                                foreach (var templine in tempLines)
                                {
                                    if (templine.LineNum == line.LineNum)
                                    {
                                        // get driver
                                        var driver = drivers.Where(x => x.FullName == line.Driver.FirstOrDefault().FullName).ToList();
                                        //set line driver to full driver object
                                        templine.Driver = driver;
                                    }
                                }
                            }
                        }
                        catch
                        {

                        }
                    }
                    
                }
            }
            return Task.FromResult(tempLines);
        }
    }
}

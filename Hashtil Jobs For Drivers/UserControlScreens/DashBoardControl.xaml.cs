using Hashtil_Jobs_For_Drivers.Heplers;
using Hashtil_Jobs_For_Drivers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace Hashtil_Jobs_For_Drivers.UserControlScreens
{
    /// <summary>
    /// Interaction logic for DashBoardControl.xaml
    /// </summary>
    public partial class DashBoardControl : UserControl
    {
        DashBoardData DashboardData = new DashBoardData();
        List<DashBoardDataPhp> DashboardDataPhp = new List<DashBoardDataPhp>();
        List<GreenHouse> GreenHouseExcelList = new List<GreenHouse>();
        List<Driver> Drivers = new List<Driver>();
        private static System.Timers.Timer timer;
        public DashBoardControl()
        {
            InitializeComponent();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzAyMzcyQDMyMzAyZTMyMmUzMGtiVTBoVW0rV0twVmV2RWxNaWprblZRclZKbWNHR3ZrcnZmM3JERWJGVkk9");

            HttpReq.InitializeClient();
            Task.Run(() => GetExcel());
            Task.Run(() => GetGSheet());
            Task.Run(() => GetPhpHttp());

            // For aoto refresh
            timer = new System.Timers.Timer();
            timer.Interval = 120000;

            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;

        }

        // Auto Refresh
        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Task.Run(() => GetExcel());
            Task.Run(() => GetGSheet());
            Task.Run(() => GetPhpHttp());
        }

        // Get Excel Data 
        private async void GetExcel()
        {
            try
            {
                GreenHouseExcelList = await ExcelHelper.GetListOfGreenHouse();
                // Green Houses Occupancy
                await this.Dispatcher.BeginInvoke(new ThreadStart(() =>
                {
                    // G1 Occupancy Treys
                    CPBG1.Progress = Convert.ToDouble(GreenHouseExcelList.FirstOrDefault(x => x.GreenNum == 1).PrecentOfOcuupancy);
                    // G2 Occupancy Treys
                    CPBG2.Progress = Convert.ToDouble(GreenHouseExcelList.FirstOrDefault(x => x.GreenNum == 2).PrecentOfOcuupancy);
                    // G3 Occupancy Treys
                    CPBG3.Progress = Convert.ToDouble(GreenHouseExcelList.FirstOrDefault(x => x.GreenNum == 3).PrecentOfOcuupancy);
                    // G4 Occupancy Treys
                    CPBG4.Progress = Convert.ToDouble(GreenHouseExcelList.FirstOrDefault(x => x.GreenNum == 4).PrecentOfOcuupancy);
                    // G5 Occupancy Treys
                    CPBG5.Progress = Convert.ToDouble(GreenHouseExcelList.FirstOrDefault(x => x.GreenNum == 5).PrecentOfOcuupancy);
                    // G6 Occupancy Treys
                    CPBG6.Progress = Convert.ToDouble(GreenHouseExcelList.FirstOrDefault(x => x.GreenNum == 6).PrecentOfOcuupancy);
                    // G7 Occupancy Treys
                    CPBG7.Progress = Convert.ToDouble(GreenHouseExcelList.FirstOrDefault(x => x.GreenNum == 7).PrecentOfOcuupancy);

                }));
            }
            catch
            {

            }
        }

        // Get Php MYsql Data
        private async void GetPhpHttp()
        {
            DashboardDataPhp = await ApiHelper.GetGreenHouseDataMySql();
            Drivers = await ApiHelper.GetDrivers();

            await this.Dispatcher.BeginInvoke(new ThreadStart(() =>
            {          

                // Thai In Green House Chart Source
                CHartThaiGreenhouse.ItemsSource = DashboardDataPhp;
                // Thai In Green House Chart Source
                CHartThaiGreenhouse.Label = $"סהכ - {DashboardDataPhp.Sum(x => x.NumOfThais)}";

                // Total Treys Of Week Chart
                LinechrtTotalJobs.ItemsSource = DashboardData.DailyJobsChartList;

                // Circular Progress Bar Total Drivers
                CPBDrivers.Progress = Drivers.Count;
                CPBDrivers.SegmentCount = Drivers.Count;              
            }));
        }
      
        // Get Google Sheet Data
        private async void GetGSheet()
        {
            var googleSheetsEntries = await GSheetsHelper.ReadEntries();

            DashboardData = await GSheetsHelper.DashBoardData(googleSheetsEntries);
                       
            await this.Dispatcher.BeginInvoke(new ThreadStart(() =>
            {       
                //Jobs && Plants Circular Gauge
                CGNiddleTreys.Value = DashboardData.NumOfMagashForTommorrow;
                CGNiddlePlants.Value = DashboardData.NumOfPlantsForTommorrow;
                CGTexttreys.Text =  DashboardData.NumOfMagashForTommorrow.ToString("#,0");
                CGTextplants.Text = $"{(Convert.ToDouble(DashboardData.NumOfPlantsForTommorrow)/1000000).ToString("N2")}M";

                // Total Treys Of Week Chart
                LinechrtTotalJobs.ItemsSource = DashboardData.DailyJobsChartList;

                // Loading Spinner Off
                LSpinner.Visibility = Visibility.Hidden;

            }));

        }


    }
}

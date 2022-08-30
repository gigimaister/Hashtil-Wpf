﻿using Hashtil_Jobs_For_Drivers.Heplers;
using Hashtil_Jobs_For_Drivers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        public DashBoardControl()
        {
            InitializeComponent();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzAyMzcyQDMyMzAyZTMyMmUzMGtiVTBoVW0rV0twVmV2RWxNaWprblZRclZKbWNHR3ZrcnZmM3JERWJGVkk9");

            HttpReq.InitializeClient();
            GetAllData();                 
        }

        private void GetAllData()
        {
            Task.Run(() => GetAllDataAsync());
            
        }

        private async void GetAllDataAsync()
        {
            DashboardDataPhp = await ApiHelper.GetGreenHouseDataMySql();
            GreenHouseExcelList = await ExcelHelper.GetListOfGreenHouse();

            var yt = await GSheetsHelper.ReadEntries();
            DashboardData = await GSheetsHelper.DashBoardData(yt);
            Drivers = await ApiHelper.GetDrivers();

            await Dispatcher.BeginInvoke(new ThreadStart(() => LSpinner.Visibility = Visibility.Hidden));
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

                //Jobs && Plants Circular Gauge
                CGNiddleTreys.Value = DashboardData.NumOfMagashForTommorrow;
                CGNiddlePlants.Value = DashboardData.NumOfPlantsForTommorrow;
                CGTexttreys.Text =  DashboardData.NumOfMagashForTommorrow.ToString("#,0");
                CGTextplants.Text = $"{(Convert.ToDouble(DashboardData.NumOfPlantsForTommorrow)/1000000).ToString("N2")}M";

                // Thai In Green House Chart Source
                CHartThaiGreenhouse.ItemsSource = DashboardDataPhp;
                // Thai In Green House Chart Source
                CHartThaiGreenhouse.Label = $"סהכ - {DashboardDataPhp.Sum(x => x.NumOfThais)}";

                // Circular Progress Bar Total Drivers
                CPBDrivers.Progress = Drivers.Count;
                CPBDrivers.SegmentCount = Drivers.Count;
            }));

        }


    }
}
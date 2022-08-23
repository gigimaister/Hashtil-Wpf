using Hashtil_Jobs_For_Drivers.Heplers;
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
            var phpGreedList = await ApiHelper.GetGreenHouseDataMySql();
            var greenHouseList = await ExcelHelper.GetListOfGreenHouse();
            DashboardData = await Heplers.GSheetsHelper.DashBoardData();
            await Dispatcher.BeginInvoke(new ThreadStart(() => LSpinner.Visibility = Visibility.Hidden));
            await this.Dispatcher.BeginInvoke(new ThreadStart(() =>
            {
                CGPointerVal.Value = Convert.ToDouble(DashboardData.NumOfOrdersForTommorrow);
                CGAnnoJFoRTommorrow.Content = DashboardData.NumOfOrdersForTommorrow.ToString();
            }));

        }


    }
}

using Hashtil_Jobs_For_Drivers.Heplers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hashtil_Jobs_For_Drivers.Views
{
    /// <summary>
    /// Interaction logic for DashBoard.xaml
    /// </summary>
    public partial class DashBoard : Window
    {
        public DashBoard()
        {
            InitializeComponent();
            HttpReq.InitializeClient();
            Task.Run(() => GetGSheetData());
            Task.Run(() => GetPhpGreenHouseData());
            Task.Run(() => GetExcelData());
        }
        // Get GreenHouse Data From PHP Server
        private async void GetPhpGreenHouseData()
        {
            var phpGreedList = await ApiHelper.GetGreenHouseDataMySql();
        }

        // Get GreenHouse Statistics From Excel(Metzay)
        private async void GetExcelData()
        {
            var greenHouseList = await ExcelHelper.GetListOfGreenHouse();
        }

        // Get Google Sheet Orders Data
        private async void GetGSheetData()
        {
            var sheetBoard = await Heplers.GSheetsHelper.DashBoardData();
        }

    }
}

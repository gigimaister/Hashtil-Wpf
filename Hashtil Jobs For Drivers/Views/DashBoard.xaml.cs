using Hashtil_Jobs_For_Drivers.Heplers;
using System.Threading.Tasks;
using System.Windows;

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

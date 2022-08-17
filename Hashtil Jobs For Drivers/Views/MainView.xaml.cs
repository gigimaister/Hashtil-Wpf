using Hashtil_Jobs_For_Drivers.Heplers;
using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
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

        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        // Page Style
        #region Page Style
        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();

            if (IsDarkTheme = theme.GetBaseTheme()== BaseTheme.Dark)
            {
                IsDarkTheme=false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            paletteHelper.SetTheme(theme);
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
        #endregion
        // Toggle Maximize Screen
        private void btn_fullScreen_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

    }
}

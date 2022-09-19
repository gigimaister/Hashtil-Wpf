using Hashtil_Jobs_For_Drivers.Models;
using Hashtil_Jobs_For_Drivers.UserControlScreens;
using MaterialDesignThemes.Wpf;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
            CC.Content = new DashBoardControl();
        }
    
        // Page Style
        #region Page Style

        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();
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

        #endregion

        
        // Log Out
        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWin = new MainWindow();
            this.Close();
            mainWin.ShowDialog();
        }
     
       
        // Btns
        #region Btn Click

        // DashBoard Btn Click
        private void NavButton_Click(object sender, RoutedEventArgs e)
        {
            CC.Content = new DashBoardControl();
        }

        // Delivery Line Btn Clicked
        private void NBtruck_Click(object sender, RoutedEventArgs e)
        {
            CC.Content = new DeliveryLineControl(); 
        }

        // Drivers Detail Btn Clicked
        private void NBDriversData_Click(object sender, RoutedEventArgs e)
        {
            CC.Content = new DriversDetailsControl();
        }
        // Cages On Map Control Page
        private void NBCagesOnMap_Click(object sender, RoutedEventArgs e)
        {
            CC.Content = new CagesOnMapControl();
        }
        #endregion


    }
}

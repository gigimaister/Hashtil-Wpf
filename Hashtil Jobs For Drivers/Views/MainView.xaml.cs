using Hashtil_Jobs_For_Drivers.Models;
using Hashtil_Jobs_For_Drivers.UserControlScreens;
using MaterialDesignThemes.Wpf;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace Hashtil_Jobs_For_Drivers.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private static System.Timers.Timer timer;
        public MainView()
        {
            InitializeComponent();
            CC.Content = new DashBoardControl();

            // For aoto timer scheduled
            timer = new System.Timers.Timer();
            timer.Interval = 5*60*1000;

            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }
        // Timer 
        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Task.Run(() =>   CheckLoginSession());
        }
        // If User Left Screen Running Go Back To Login
        async Task CheckLoginSession()
        {
            await this.Dispatcher.BeginInvoke(new ThreadStart(() =>
            {
                if (DateTime.Now > Convert.ToDateTime("23:50"))
                {                    
                    this.Close();                
                }

            }));
           
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
        // Go To Metzay Control
        private void NBDMetzayControl_Click(object sender, RoutedEventArgs e)
        {
            CC.Content = new MetzayControl();
        }
        #endregion


    }
}

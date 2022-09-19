using Hashtil_Jobs_For_Drivers.Heplers;
using MaterialDesignThemes.Wpf;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System;
using System.Collections.ObjectModel;
using Hashtil_Jobs_For_Drivers.Views;
using NPOI.Util.Collections;

namespace Hashtil_Jobs_For_Drivers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            HttpReq.InitializeClient();
            if (Properties.Settings.Default.Uname != string.Empty)
            {
                txtUsername.Text = Properties.Settings.Default.Uname;
                txtPassword.Password = Properties.Settings.Default.Upwd;
            }
        }

        public bool IsDarkTheme { get; set; } = true;
        private readonly PaletteHelper paletteHelper = new PaletteHelper();      

        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();

            if(IsDarkTheme = theme.GetBaseTheme()== BaseTheme.Dark)
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

        // Login Btn
        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Save To Properties
                Properties.Settings.Default.Uname = txtUsername.Text;
                Properties.Settings.Default.Upwd = txtPassword.Password;
                Properties.Settings.Default.Save();

                var LoginUsersList = await ApiHelper.Login(txtUsername.Text, txtPassword.Password);
                if (LoginUsersList.FirstOrDefault().UserLevel>0)
                {
                    MainView mainView = new MainView();
                    mainView.txtUserName.Text = LoginUsersList.FirstOrDefault().UserNickName.ToString();
                    try
                    {
                        mainView.txtLaslLogin.Text = Convert.ToDateTime(LoginUsersList.FirstOrDefault().LastLogin).AddHours(3).AddMinutes(-4).ToString();
                    }
                    catch
                    {
                        mainView.txtFixedLastLogin.Text="";
                    }
                    this.Close();
                    mainView.ShowDialog();
                    
                  
                   
                }
                txtErrorMessage.Visibility = Visibility.Hidden;

            }
            catch
            {
                txtErrorMessage.Visibility = Visibility.Visible; 
            }
        }
    }
}

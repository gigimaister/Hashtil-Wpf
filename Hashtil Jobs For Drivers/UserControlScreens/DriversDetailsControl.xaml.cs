using Hashtil_Jobs_For_Drivers.Heplers;
using Hashtil_Jobs_For_Drivers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Windows.Threading;

namespace Hashtil_Jobs_For_Drivers.UserControlScreens
{
    /// <summary>
    /// Interaction logic for DriversDetailsControl.xaml
    /// </summary>
    public partial class DriversDetailsControl : UserControl
    {
        List<Driver> Drivers = new List<Driver>();
        public DriversDetailsControl()
        {
            InitializeComponent();
            Task.Run(() => GetDrivers());
        }

        private async  void GetDrivers()
        {
            Drivers = await ApiHelper.GetDrivers();

            await this.Dispatcher.BeginInvoke(new ThreadStart(() =>
            {
                icDrivers.ItemsSource = Drivers;
                LSpinner.Visibility = Visibility.Collapsed;
            }));
        }

     
    }
}

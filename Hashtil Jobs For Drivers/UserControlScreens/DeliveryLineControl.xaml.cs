using Hashtil_Jobs_For_Drivers.Heplers;
using Hashtil_Jobs_For_Drivers.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hashtil_Jobs_For_Drivers.UserControlScreens
{
    /// <summary>
    /// Interaction logic for DeliveryLineControl.xaml
    /// </summary>
    public partial class DeliveryLineControl : UserControl
    {
        List<Order> Orders = new List<Order>();
        public DeliveryLineControl()
        {
            InitializeComponent();
            Task.Run(() => GetLines());
        }

       

        private async void GetLines()
        {
            Orders = await GSheetsHelper.ReadEntries();
            await GSheetsHelper.GetLinesSumUp(Orders);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }
    }
}

using Hashtil_Jobs_For_Drivers.Heplers;
using Hashtil_Jobs_For_Drivers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        List<DeliveryLineStatus> DeliveryLineStatuses = new List<DeliveryLineStatus>();
        List<Driver> Drivers = new List<Driver>();
        public DeliveryLineControl()
        {
            InitializeComponent();
            Task.Run(() => GetLines());
            
            
        }

       

        private async void GetLines()
        {          
            DeliveryLineStatuses = await GSheetsHelper.ReadLineSheet();
            Orders = await GSheetsHelper.ReadEntries();            
            Drivers = await ApiHelper.GetDrivers();          
            var delLine = await GSheetsHelper.GetLinesSumUp(Orders, DeliveryLineStatuses, Drivers);
            // Get cx From Orders By Group For List View
           
            foreach (var line in delLine)
            {
                var ordersGroup = line.Orders.GroupBy(x => x.Cx).ToList();
                foreach(var order in ordersGroup)
                {
                    var o = new Order();
                    o.Cx = order.Key;
                    line.OrdersGroup.Add(o);
                }
            }
            delLine.OrderByDescending(x => x.LineNum);
            await this.Dispatcher.BeginInvoke(new ThreadStart(() =>
            {
                icDeliveryLine.ItemsSource = delLine;

                LSpinner.Visibility = System.Windows.Visibility.Hidden;
            }));
           
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var y = e;
            var yy = icDeliveryLine;

            CC.Content = new LineBreakDownControl();
        }
    }
}

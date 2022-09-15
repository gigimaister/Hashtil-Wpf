﻿using Hashtil_Jobs_For_Drivers.Heplers;
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
        List<DeliveryLineStatus> DeliveryLineControls = new List<DeliveryLineStatus>();
        DeliveryLineStatus DelLineForControl = new DeliveryLineStatus();
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
            DeliveryLineControls = await GSheetsHelper.GetLinesSumUp(Orders, DeliveryLineStatuses, Drivers);
            // Get cx From Orders By Group For List View
           
            foreach (var line in DeliveryLineControls)
            {
                var ordersGroup = line.Orders.GroupBy(x => x.Cx).ToList();
                foreach(var order in ordersGroup)
                {
                    var o = new Order();
                    o.Cx = order.Key;
                    o.CxCages = Convert.ToInt32(Orders.Where(x => x.Cx == order.Key).Sum(x => x.Cages));
                    o.InnerOrders = line.Orders.Where(x => x.Cx == order.Key).ToList();
                    line.OrdersGroup.Add(o);
                    
                }
            }
            DeliveryLineControls.OrderByDescending(x => x.LineNum);
            await this.Dispatcher.BeginInvoke(new ThreadStart(() =>
            {
                icDeliveryLine.ItemsSource = DeliveryLineControls;
                
                LSpinner.Visibility = System.Windows.Visibility.Hidden;
            }));
           
        }

        // List Box Selected
        // Move To Line BreakDown Control
        private void lbTodoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (icDeliveryLine.SelectedItem != null)
            {
                DelLineForControl = icDeliveryLine.SelectedItem as DeliveryLineStatus;
                CC.Content = new LineBreakDownControl(DelLineForControl);
            }
         
        }
    }
}

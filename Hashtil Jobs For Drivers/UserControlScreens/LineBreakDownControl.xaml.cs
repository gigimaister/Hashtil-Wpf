using Hashtil_Jobs_For_Drivers.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Printing;

namespace Hashtil_Jobs_For_Drivers.UserControlScreens
{
    /// <summary>
    /// Interaction logic for LineBreakDownControl.xaml
    /// </summary>
    public partial class LineBreakDownControl : UserControl
    {
        DeliveryLineStatus SelectedDeliveryLine = new DeliveryLineStatus();

        public LineBreakDownControl(DeliveryLineStatus deliveryLineStatus=null)
        {
            InitializeComponent();
            lbDeliveryLine.ItemsSource = deliveryLineStatus.OrdersGroup;
            txtLineNum.Text = deliveryLineStatus.LineNum.ToString();
            txtHebrewDate.Text = deliveryLineStatus.GetHebrewDay.ToString();
            txtCages.Text = deliveryLineStatus.NumOfCages.ToString();
            txtDriver.Text = deliveryLineStatus.DriversFullName;
            txtCx.Text = deliveryLineStatus.NumOfCx.ToString();
            foreach(var order in deliveryLineStatus.OrdersGroup)
            {
                if(order.SapCxes.Count > 0)
                {
                    order.ErrorMessage = "";
                }
            }
        }

    
        // Print Line Btn
        private void btnPrintLine_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();
            if(printDlg.ShowDialog() == true)
            {
                 printDlg.PrintVisual(gridprint, "");
            }
           

        }
    }
}

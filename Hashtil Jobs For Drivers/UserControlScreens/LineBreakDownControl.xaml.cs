using Hashtil_Jobs_For_Drivers.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

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

            txtHebrewDate.Text = deliveryLineStatus.GetHebrewDay.ToString();
            txtCages.Text = deliveryLineStatus.NumOfCages.ToString();
            txtDriver.Text = deliveryLineStatus.DriversFullName;
            txtCx.Text = deliveryLineStatus.NumOfCx.ToString();
        }
    }
}

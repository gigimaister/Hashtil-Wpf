using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashtil_Jobs_For_Drivers.Models
{
    public class DeliveryLineStatus
    {
        public int LineNum { get; set; }
        public int NumOfCx { get; set; }
        public int NumOfCages { get; set; }
        public string? LineName { get; set; }
        public DateTime DeliveryDate { get; set; }
        public Driver? Driver { get; set; }
        public List<Order>? Orders { get; set; }

    }
}

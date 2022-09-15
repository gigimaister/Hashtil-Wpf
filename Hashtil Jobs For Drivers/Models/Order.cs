using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace Hashtil_Jobs_For_Drivers.Models
{
    public class Order
    {
        public DateTime Date { get; set; }
        public string? Driver { get; set; }
        public string? Cx { get; set; }
        public string? CxPhone1 { get; set; }
        public string? CxPhone2 { get; set; }
        public int CxCages { get; set; }
        public string? Gidul { get; set; }
        public string? Zan { get; set; }
        public double Plants { get; set; }
        public double Magash { get; set; }
        public string? Passport { get; set; }
        public double Avarage { get; set; }
        public string? Status { get; set; }
        public double Cages { get; set; }
        public string? Remarks { get; set; }
        public string? HasCertificate { get; set; }
        public DateTime CreationDate { get; set; }

        // For Split Job
        public int SplitMagash { get; set; }
        public int SplitCages { get; set; }

        // For Line Breake Down Control
        public List<Order>? InnerOrders { get; set; }
    }


}

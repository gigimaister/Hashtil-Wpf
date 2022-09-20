using Hashtil_Jobs_For_Drivers.Heplers;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // For SAP Cx Phon Num
        public List<SapCx> SapCxes { get; set; } = new List<SapCx>();
        public string? ErrorMessage { get; set; }
        
        public void GetNumOfCxPhonesList()
        {
            if(SapCxes.Count == 1)
            {
                CxPhone1 = SapCxes.FirstOrDefault().SapCxP1;
                CxPhone2 = SapCxes.FirstOrDefault().SapCxP2;
            }
            if(SapCxes.Count > 1)
            {
                CxPhone1 = SapCxes.FirstOrDefault().SapCxP1;
                CxPhone2 = SapCxes.FirstOrDefault().SapCxP2;
                ErrorMessage = Constants.Hebrew.ManySapMatch;
            }
            else
            {
                ErrorMessage = Constants.Hebrew.NoSapMatch;
            }
        }
    }

    

}

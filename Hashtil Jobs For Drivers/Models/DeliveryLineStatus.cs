using Hashtil_Jobs_For_Drivers.Heplers;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hashtil_Jobs_For_Drivers.Models
{
    public class DeliveryLineStatus
    {
        public int LineNum { get; set; }
        public int NumOfCx { get; set; }
        public int NumOfCages { get; set; }
        public string? LineName { get; set; }
        public DateTime DeliveryDate { get; set; }
        public List<Driver>? Driver { get; set; } = new List<Driver>();
        public List<Order>? Orders { get; set; }
        public List<Order>? OrdersGroup { get; set; } = new List<Order>();
        public string? SplitLineAlert { get; set; }
        public string? DriversFullName
        {
            get
            {
                if(Driver.Count() > 0)
                {
                    return Driver.FirstOrDefault().FullName ?? "";
                }
                return Constants.Hebrew.StillNoDriver;
            }
        }

        public string? GetHebrewDay => TranslationHelper.GethebrewDayOfTheWeek(DeliveryDate) ??  "";
    }
}

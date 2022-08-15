using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashtil_Jobs_For_Drivers.Models
{
    public class GreenHouse
    {
        public int MaxMagash { get; set; }
        public int NumOfCurrentMagash { get; set; }
        public int PrecentOfOcuupancy => NumOfCurrentMagash/MaxMagash;
    }
}

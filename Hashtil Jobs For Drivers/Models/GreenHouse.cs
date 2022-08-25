using System;

namespace Hashtil_Jobs_For_Drivers.Models
{
    public class GreenHouse
    {
        public int GreenNum { get; set; }
        public int CurrentMagash { get; set; }
        public int MaxMagash { get; set; }
        public int PrecentOfOcuupancy => Convert.ToInt32((Convert.ToDouble(CurrentMagash)/Convert.ToDouble(MaxMagash)*100));
    }
}

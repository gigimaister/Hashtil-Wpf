namespace Hashtil_Jobs_For_Drivers.Models
{
    public class GreenHouse
    {
        public int GreenNum { get; set; }
        public int CurrentMagash { get; set; }
        public int CurrentThai { get; set; }
        public int MaxMagash { get; set; }
        public int PrecentOfOcuupancy => CurrentMagash/MaxMagash;
    }
}

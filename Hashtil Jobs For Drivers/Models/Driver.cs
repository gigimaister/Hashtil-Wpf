using System.Collections.Generic;

namespace Hashtil_Jobs_For_Drivers.Models
{
    public class Driver
    {
        public string? FullName { get; set; }
        public string? TableName { get; set; }
        public string? Contrcator { get; set; }
        public string? PhoneNum { get; set; }
        public string? Photo { get; set; }
        public string? FullPhotoUrl => $"{Constants.Url.MainPhotosUrl}/{Photo}";
        List<Order> Orders { get; set; }

        // For Split Jobs      
        public int SplitMagash { get; set; }
        public int SplitCage { get; set; }         
    }
}

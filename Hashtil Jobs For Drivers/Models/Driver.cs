using System.Collections.Generic;

namespace Hashtil_Jobs_For_Drivers.Models
{
    public class Driver
    {
        public string? FullName { get; set; }
        public string? TableName { get; set; }
        public string? Contrcator { get; set; }
        public string? PhoneNum { get; set; }
        List<Order> Orders { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashtil_Jobs_For_Drivers.Models
{
    public class User
    {
        public string? UsrScreenName { get; set; }
        public int UserLevel { get; set; }
        public DateTime LastLogin { get; set; }
    }
}

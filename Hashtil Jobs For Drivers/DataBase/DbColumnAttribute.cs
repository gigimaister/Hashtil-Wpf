using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashtil_Jobs_For_Drivers.DataBase
{
    [System.AttributeUsage(System.AttributeTargets.All)]
    public class DbColumnAttribute : System.Attribute
    {
        public string Name;

        public DbColumnAttribute(string name)
        {
            Name = name;
        }
    }
}

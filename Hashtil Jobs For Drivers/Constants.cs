using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashtil_Jobs_For_Drivers
{
    public static class Constants
    {
        public static class Url 
        {
            public static string Login = "http://www.hashtildb.pe.hu/LoginMapTable.php?";
        }

        public static class Files
        {
            public static string Metzay = @"J:\מצאי חממות חדש\מצאי חממות חדש.xlsx";
            public static string WPFGreenHouseData = @"J:\קובי\\קובי\DONOTTOUCH\DONOTTOUCHWPF.xlsx";
        }

        public static class MetzayCol
        {
            public static int Ham = 0;
            public static int Gam = 1;
            public static int Pas = 8;
            public static int Mag = 10;
        }
    }
}

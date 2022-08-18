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
            public static string PhpGreenHouseData = "http://www.hashtildb.pe.hu/GreenHouseDataJson.php";
        }

        public static class Files
        {
            public static string Metzay = @"J:\מצאי חממות חדש\מצאי חממות חדש.xlsx";
            public static string WPFGreenHouseData = @"J:\קובי\\קובי\WPFDONOTTOUCH\DONOTTOUCHWPF.xlsx";
        }

        public static class MetzayCol
        {
            public static int Ham = 0;
            public static int Gam = 1;
            public static int Pas = 8;
            public static int Mag = 10;
        }

        // Return Max Num Of Treys Inside GreenHouse
        public static int MaxTreyCapacity(int greenNum)
        {
            int curMagash = 0;
            switch (greenNum)
            {
                case 1:
                    curMagash = 37080;
                    break;
                case 2:
                    curMagash = 37888;
                    break;
                case 3:
                    curMagash = 10000;
                    break;
                case 4:
                    curMagash = 21232;
                    break;
                case 5:
                    curMagash = 18912;
                    break;
                case 6:
                    curMagash = 39648;
                    break;
                case 7:
                    curMagash = 34816;
                    break;
                   
            }
            return curMagash;
        }
    }
}

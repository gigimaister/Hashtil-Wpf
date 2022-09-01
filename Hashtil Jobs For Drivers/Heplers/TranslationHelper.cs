using System;

namespace Hashtil_Jobs_For_Drivers.Heplers
{
    public static class TranslationHelper
    {  
        public static string GethebrewDayOfTheWeek(DateTime date)
        {
            var stringDay = date.DayOfWeek;
            string hebDate = "";

            switch (stringDay)
            {
                case DayOfWeek.Sunday:
                    hebDate = $"ראשון\n{date.ToString("yy-MM-dd")}";
                    break;
                case DayOfWeek.Monday:
                    hebDate =  $"שני\n{date.ToString("yy-MM-dd")}";
                    break;
                case DayOfWeek.Tuesday:
                    hebDate =  $"שלישי\n{date.ToString("yy-MM-dd")}";
                    break;
                case DayOfWeek.Wednesday:
                    hebDate =  $"רביעי\n{date.ToString("yy-MM-dd")}";
                    break;
                case DayOfWeek.Thursday:
                    hebDate =  $"חמישי\n{date.ToString("yy-MM-dd")}";
                    break;
                case DayOfWeek.Friday:
                    hebDate =  $"שישי\n{date.ToString("yy-MM-dd")}";
                    break;
            }
         
            return hebDate;
        }
    }
}

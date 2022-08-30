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
                    hebDate = "ראשון";
                    break;
                case DayOfWeek.Monday:
                    hebDate = "שני";
                    break;
                case DayOfWeek.Tuesday:
                    hebDate = "שלישי";
                    break;
                case DayOfWeek.Wednesday:
                    hebDate = "רביעי";
                    break;
                case DayOfWeek.Thursday:
                    hebDate = "חמישי";
                    break;
                case DayOfWeek.Friday:
                    hebDate = "שישי";
                    break;
            }
         
            return hebDate;
        }
    }
}

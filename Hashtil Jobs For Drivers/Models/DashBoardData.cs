using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashtil_Jobs_For_Drivers.Models
{
    public class DashBoardData
    {
        public int NumOfOrdersForTommorrow { get; set; }
        public int NumOfPlantsForTommorrow { get; set; }
        public int NumOfMagashForTommorrow { get; set; }
        public int NumOfCagesForTommorrow { get; set; }
        public int NumOfOrdersForToday { get; set; }
        public int NumOfPlantsForToday { get; set; }
        public int NumOfMagashForToday { get; set; }
        public int NumOfCagesForToday { get; set; }
        public int OrdersEnteredTodayForToday { get; set; }
        public List<DailyJobsChart>? DailyJobsChartList { get; set; }
        public int FinishJobs { get; set; }
    }
}

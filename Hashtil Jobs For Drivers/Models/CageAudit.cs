using Hashtil_Jobs_For_Drivers.DataBase;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashtil_Jobs_For_Drivers.Models
{
    public class CageAudit
    {
        [DbColumnAttribute("row_id")]
        [Display(Name = "מזהה לקוח", Description = "CustomerName is necessary for identification ")]
        public int CxId { get; set; }

        [DbColumnAttribute("cx")]
        [Display(Name = "לקוח", Description = "CustomerName is necessary for identification ")]
        public string Cx { get; set; }

        [DbColumnAttribute("sector")]
        [Display(Name = "תחום", Description = "CustomerName is necessary for identification ")]
        public string Sector { get; set; }

        [DbColumnAttribute("date")]
        [Display(Name = "תאריך", Description = "CustomerName is necessary for identification ")]
        public DateTime Date { get; set; }

        [DbColumnAttribute("time")]
        [Display(Name = "שעה", Description = "CustomerName is necessary for identification ")]
        public DateTime Time { get; set; }

        [DbColumnAttribute("contractor")]
        [Display(Name = "מבצע", Description = "CustomerName is necessary for identification ")]
        public string Contractor { get; set; }

        [DbColumnAttribute("user")]
        [Display(Name = "נהג", Description = "CustomerName is necessary for identification ")]
        public string User { get; set; }

        [DbColumnAttribute("status")]
        [Display(Name = "מצב", Description = "CustomerName is necessary for identification ")]
        public string Status { get; set; }

        [DbColumnAttribute("num_of_cage")]
        [Display(Name = "מס כלובים", Description = "CustomerName is necessary for identification ")]
        public int NumOfCage { get; set; }

        public int CagePrice { get; set; }

        public int TotalCageCost { get; set; }

        [DbColumnAttribute("Lon")]
        // [JsonIgnore]
        [Display(Name = "קו אורך", Description = "CustomerName is necessary for identification ")]
        public double Lon { get; set; }

        [DbColumnAttribute("Lan")]
        //   [JsonIgnore]
        [Display(Name = "קו רוחב", Description = "CustomerName is necessary for identification ")]
        public double Lan { get; set; }
    }
}

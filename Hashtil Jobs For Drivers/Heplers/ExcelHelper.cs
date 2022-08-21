using Hashtil_Jobs_For_Drivers.Models;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Excel = Microsoft.Office.Interop.Excel;

namespace Hashtil_Jobs_For_Drivers.Heplers
{
    public static class ExcelHelper
    {
        private static Worksheet ws;
        private static Workbook wb;
        
        static ExcelHelper()
        {           
            _Application excel = new Application();       
            wb = excel.Workbooks.Open(Constants.Files.WPFGreenHouseData);
            ws = wb.Worksheets[1];         
        }

        public static Task<List<GreenHouse>> GetListOfGreenHouse()
        {
            var greenList = new List<GreenHouse>();
            _Excel.Range xlRange = ws.UsedRange;

            // Green 1
            var G1 = new GreenHouse
            {
                GreenNum = Convert.ToInt32(xlRange.Cells[1, 1].Value2),
                CurrentMagash = Convert.ToInt32(xlRange.Cells[1, 2].Value2)             

            };
            G1.MaxMagash = Constants.MaxTreyCapacity(G1.GreenNum);
            greenList.Add(G1);
            // Green 2
            var G2 = new GreenHouse
            {
                GreenNum = Convert.ToInt32(xlRange.Cells[2, 1].Value2),
                CurrentMagash = Convert.ToInt32(xlRange.Cells[2, 2].Value2)
            };
            G2.MaxMagash = Constants.MaxTreyCapacity(G2.GreenNum);
            greenList.Add(G2);
            // Green 3
            var G3 = new GreenHouse
            {
                GreenNum = Convert.ToInt32(xlRange.Cells[3, 1].Value2),
                CurrentMagash = Convert.ToInt32(xlRange.Cells[3, 2].Value2)
            };
            G3.MaxMagash = Constants.MaxTreyCapacity(G3.GreenNum);
            greenList.Add(G3);
            // Green 4
            var G4 = new GreenHouse
            {
                GreenNum = Convert.ToInt32(xlRange.Cells[4, 1].Value2),
                CurrentMagash = Convert.ToInt32(xlRange.Cells[4, 2].Value2)
            };
            G4.MaxMagash = Constants.MaxTreyCapacity(G4.GreenNum);
            greenList.Add(G4);
            // Green 5
            var G5 = new GreenHouse
            {
                GreenNum = Convert.ToInt32(xlRange.Cells[5, 1].Value2),
                CurrentMagash = Convert.ToInt32(xlRange.Cells[5, 2].Value2)
            };
            G5.MaxMagash = Constants.MaxTreyCapacity(G5.GreenNum);
            greenList.Add(G5);
            // Green 6
            var G6 = new GreenHouse
            {
                GreenNum = Convert.ToInt32(xlRange.Cells[6, 1].Value2),
                CurrentMagash = Convert.ToInt32(xlRange.Cells[6, 2].Value2)
            };
            G6.MaxMagash = Constants.MaxTreyCapacity(G6.GreenNum);
            greenList.Add(G6);
            // Green 7
            var G7 = new GreenHouse
            {
                GreenNum = Convert.ToInt32(xlRange.Cells[7, 1].Value2),
                CurrentMagash = Convert.ToInt32(xlRange.Cells[7, 2].Value2)
            };
            G7.MaxMagash = Constants.MaxTreyCapacity(G7.GreenNum);
            greenList.Add(G7);

            wb.Close();
            

            return Task.FromResult(greenList);
            
        }
        
    }
}

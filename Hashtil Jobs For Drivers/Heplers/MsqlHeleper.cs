using Hashtil_Jobs_For_Drivers.Models;
using MySql.Data.MySqlClient;
using NPOI.OpenXmlFormats.Dml.Diagram;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashtil_Jobs_For_Drivers.Heplers
{

    public static class MsqlHeleper
    {
        public static string? ConString = "Data Source = hashtil-sap1; Initial Catalog = Hashtil09; User ID=h2;Password=H2kobiApp;";
        static SqlConnection con = new SqlConnection(ConString);
        
        public static Task<List<SapCx>> GetCxPhonesFromSap(string cxName)
        {
            

            var scxList = new List<SapCx>();
            try
            {
                // Open Connection
                con.Open();
                string query = $"select CardName, Phone1, Phone2 from dbo.OCRD where CardName  LIKE N'%{cxName}%';";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    try
                    {
                        var scx = new SapCx();
                        scx.SapCxName = reader.GetValue(0).ToString();
                        scx.SapCxP1 = reader.GetValue(1).ToString();
                        scx.SapCxP2 = reader.GetValue(2).ToString();
                        scxList.Add(scx);
                    }
                    catch
                    {
                        continue;
                    }
                   
                }
               
            }
            catch
            {

            }
            // Close Connection
            con.Close();
            return Task.FromResult(scxList);
        }
    }
}

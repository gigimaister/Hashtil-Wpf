using Hashtil_Jobs_For_Drivers.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Hashtil_Jobs_For_Drivers.DataBase
{
    internal class DBAsync : DBHelperOld
    {
        public ObservableCollection<CageAudit> cageAuditClasses = new ObservableCollection<CageAudit>();
        public ObservableCollection<CageAudit> cageauditcompare = new ObservableCollection<CageAudit>();
        public async Task<ObservableCollection<CageAudit>> GetAllRecordAsync(string query)
        {
            string q = $"SELECT * FROM  {query} ;";

            MySqlCommand cmd = new MySqlCommand(q, con);
            if (con.State == 0)
            {
                con.Open();
            }
            var rdr = cmd.ExecuteReader();
            cageauditcompare.Clear();
            while (await rdr.ReadAsync())
            {

                var CxCageTable = new CageAudit
                {
                    CxId = Convert.ToInt32(rdr["row_id"].ToString()),
                    Cx = Convert.ToString(rdr["cx"].ToString()),
                    Date = Convert.ToDateTime(rdr["date"].ToString()),
                    Time = Convert.ToDateTime(rdr["time"].ToString()),
                    Contractor = Convert.ToString(rdr["contractor"]),
                    User = Convert.ToString(rdr["user"].ToString()),
                    Status = Convert.ToString(rdr["status"]),
                    NumOfCage = Convert.ToInt32(rdr["num_of_cage"].ToString()),
                    Sector = Convert.ToString(rdr["sector"]),
                    //Lon = Convert.ToDouble(rdr["Lon"].ToString()),
                    // Lan = Convert.ToDouble(rdr["Lan"].ToString()),
                    //location = new Location(Convert.ToDouble(rdr["Lan"].ToString()), Convert.ToDouble(rdr["Lon"].ToString())),
                    TotalCageCost = Convert.ToInt32(rdr["costtotalcage"]),
                    CagePrice = Convert.ToInt32(rdr["cageprice"])


                };


                cageauditcompare.Add(CxCageTable);
            }

            if (cageauditcompare.Count() > cageAuditClasses.Count())
            {
                foreach (var cage in cageauditcompare)
                {
                    if (cageAuditClasses.Contains(cage))
                    {
                        cageauditcompare.Remove(cage);
                    }
                }
                con.Close();
                rdr.Close();
                return cageauditcompare;
            }
            con.Close();
            rdr.Close();
            return cageAuditClasses;





        }
    }
}

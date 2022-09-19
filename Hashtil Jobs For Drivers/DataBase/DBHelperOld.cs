using MySql.Data.MySqlClient;

namespace Hashtil_Jobs_For_Drivers.DataBase
{
    public class DBHelperOld
    {
        private const string constring = "SERVER=sql186.main-hosting.eu;DATABASE=u319907874_hashtil_db;UID=u319907874_kobi_gigi;PASSWORD=K5$@F991Sw";

        public MySqlConnection con = new MySqlConnection(constring);
    }
}

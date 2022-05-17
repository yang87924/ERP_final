using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PPERP
{
    class clsDB
    {
        public SqlConnection loginDB()
        {
            SqlConnection dbConn;
            dbConn = new SqlConnection("data source=127.0.0.1; user id=sa; password=jay87924; initial catalog=PPERP");
            dbConn.Open();

            return dbConn;
        }
    }
}

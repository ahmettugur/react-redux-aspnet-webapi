using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.Dapper.Helpers
{
    public class ConnectionHelper
    {
        public static SqlConnection GetSqlServerConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineStoreContext"].ConnectionString);
        }
    }
}

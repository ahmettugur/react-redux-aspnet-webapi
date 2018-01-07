using Dapper;
using OnlineStore.Core.Repository.Dapper;
using OnlineStore.Data.Contracts;
using OnlineStore.Data.Dapper.Helpers;
using OnlineStore.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.Dapper.Repository
{
    public class DapperUserRepository : DapperGenericRepository<User>, IUserRespository
    {
        public override string TableName => "Users";
        public override IDbConnection Connection => ConnectionHelper.GetSqlServerConnection();
        //public override IDbConnection Connection => new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineStoreContext"].ConnectionString);


        public string[] GetUserRoles(User user)
        {
            using (IDbConnection conn = Connection)
            {
                string sql = "SELECT r.Id,r.Name FROM Roles r " +
                    "INNER JOIN UserRoles ur ON r.Id = ur.RoleId " +
                    "WHERE ur.UserId = @UserId";

                conn.Open();
                string[] role = conn.Query<Role>(sql, new { user.UserId }).ToList().Select(_ => _.Name).ToArray();
                conn.Close();
                return role;
            }

            throw new NotImplementedException();
        }
    }
}

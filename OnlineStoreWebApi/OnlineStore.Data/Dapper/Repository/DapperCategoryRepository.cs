using OnlineStore.Core.Repository.Dapper;
using OnlineStore.Core.Repository.EntityFramework;
using OnlineStore.Data.Contracts;
using OnlineStore.Data.Dapper.Helpers;
using OnlineStore.Data.EntityFramework;
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
    public class DapperCategoryRepository : DapperGenericRepository<Category>, ICategoryRepository
    {
        public override string TableName => "Categories";

        public override IDbConnection Connection => ConnectionHelper.GetSqlServerConnection();
        //public override IDbConnection Connection => new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineStoreContext"].ConnectionString);
    }
}

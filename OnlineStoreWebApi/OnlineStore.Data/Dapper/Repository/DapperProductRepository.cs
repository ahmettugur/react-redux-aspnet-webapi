using Dapper;
using OnlineStore.Core.Repository.Dapper;
using OnlineStore.Data.Contracts;
using OnlineStore.Data.Dapper.Helpers;
using OnlineStore.Entity.ComplexType;
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
    public class DapperProductRepository : DapperGenericRepository<Product>, IProductRepository
    {
        public override string TableName => "Products";
        public override IDbConnection Connection => ConnectionHelper.GetSqlServerConnection();
        //public override IDbConnection Connection => new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineStoreContext"].ConnectionString);


        public List<ProductWithCategory> GetAllProductWithCategory()
        {
            using (IDbConnection con = Connection)
            {
                string sql = "SELECT p.Id " +
                    "ProductId,p.CategoryId ," +
                    "p.Name,p.Price," +
                    "p.StockQuantity," +
                    "c.Name CategoryName, " +
                    "p.Details FROM.Products p " +
                    "INNER JOIN Categories c on p.CategoryId = c.Id";

                List<ProductWithCategory> productWithCategory = con.Query<ProductWithCategory>(sql).ToList();

                return productWithCategory;
            }
        }
    }
}

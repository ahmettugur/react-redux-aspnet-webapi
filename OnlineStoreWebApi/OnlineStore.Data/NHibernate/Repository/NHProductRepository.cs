using OnlineStore.Core.Repository.NHibernate;
using OnlineStore.Data.Contracts;
using OnlineStore.Entity.ComplexType;
using OnlineStore.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.NHibernate.Repository
{
    public class NHProductRepository : NHEntityRepository<Product>, IProductRepository
    {
        private readonly NHibernateHelper _nHibernateHelper;

        public NHProductRepository(NHibernateHelper nHibernateHelper) : base(nHibernateHelper)
        {
            _nHibernateHelper = nHibernateHelper;
        }

        public List<ProductWithCategory> GetAllProductWithCategory()
        {
            using (var session = _nHibernateHelper.OpenSession())
            {
                var result = from p in session.Query<Product>()
                             join c in session.Query<Category>()
                             on p.CategoryId equals c.Id
                             select new ProductWithCategory
                             {
                                 ProductId = p.Id,
                                 CategoryId = p.CategoryId,
                                 Name = p.Name,
                                 Price = p.Price,
                                 StockQuantity = p.StockQuantity,
                                 CategoryName = c.Name,
                                 Details = p.Details
                             };

                return result.ToList();
            }
        }
    }
}

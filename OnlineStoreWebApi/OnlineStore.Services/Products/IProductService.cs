using OnlineStore.Core.ComplexType;
using OnlineStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Services.Products
{
    public interface IProductService
    {
        List<Product> GetAll();
        List<Product> GetAll(Expression<Func<Product, bool>> predicate);
        List<ProductComplex> GetAllProductWithCategory();
        Product Get(int id);
        Product Get(Expression<Func<Product, bool>> predicate);
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        void Delete(int id);
    }
}

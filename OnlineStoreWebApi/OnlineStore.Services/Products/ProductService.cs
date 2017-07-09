using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Entities;
using OnlineStore.Data.UnitOfWork;
using OnlineStore.Data.Repository;
using OnlineStore.Core.ComplexType;

namespace OnlineStore.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Product> _repository;
        private readonly IGenericRepository<Category> _categoryRepository;
        public ProductService(IUnitOfWork uow)
        {
            _uow = uow;
            _repository = _uow.GetRepository<Product>();
            _categoryRepository = _uow.GetRepository<Category>();
        }
        public void Add(Product product)
        {
            _repository.Add(product);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Delete(Product product)
        {
            _repository.Delete(product);
        }

        public Product Get(Expression<Func<Product, bool>> predicate)
        {
            return _repository.Get(predicate);
        }

        public Product Get(int id)
        {
            return _repository.Get(id);
        }

        public List<Product> GetAll()
        {
            return _repository.GetAll();
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> predicate)
        {
            return _repository.GetAll(predicate);
        }

        public void Update(Product product)
        {
            _repository.Update(product);
        }

        public List<ProductComplex> GetAllProductWithCategory()
        {
            List<ProductComplex> ProductComplex = (from product in _repository.GetAll()
                                                   join category in _categoryRepository.GetAll()
                                                   on product.CategoryId equals category.Id
                                                   select new ProductComplex
                                                   {
                                                       ProductId = product.Id,
                                                       CategoryId = product.CategoryId,
                                                       Name = product.Name,
                                                       Price = product.Price,
                                                       StockQuantity = product.StockQuantity,
                                                       CategoryName = category.Name,
                                                       Details = product.Details
                                                   }).ToList();

            return ProductComplex;
        }
    }
}

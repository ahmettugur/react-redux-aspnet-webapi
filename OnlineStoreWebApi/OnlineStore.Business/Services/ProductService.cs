using OnlineStore.Business.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Entity.Concrete;
using System.Linq.Expressions;
using OnlineStore.Data.Contracts;
using OnlineStore.Business.ValidationRules.FluentValidation;
using OnlineStore.Core.CrossCuttingConcerns.Aspects.Postsharp.ValidationAspects;
using OnlineStore.Core.CrossCuttingConcerns.Aspects.Postsharp.CacheAsepcts;
using OnlineStore.Core.CrossCuttingConcerns.Caching.Microsoft;
using OnlineStore.Core.CrossCuttingConcerns.Aspects.Postsharp.LogAspects;
using OnlineStore.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using OnlineStore.Core.CrossCuttingConcerns.Aspects.Postsharp.ExceptionAspects;
using OnlineStore.Core.CrossCuttingConcerns.Aspects.Postsharp.PerformanceAspects;
using OnlineStore.Core.CrossCuttingConcerns.Aspects.Postsharp.AuthorizationAspects;
using AutoMapper;
using OnlineStore.Entity.ComplexType;
using OnlineStore.Core.CrossCuttingConcerns.Caching.Redis;

namespace OnlineStore.Business.Services
{
    [LogAspect(typeof(FileLogger))]
    [ExceptionLogAspect(typeof(FileLogger))]
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [AuthorizationAspect(Roles = "Admin")]
        [FluentValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect(typeof(RedisCacheManager))]
        //[CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Product Add(Product entity)
        {
            return _productRepository.Add(entity);
        }

        [AuthorizationAspect(Roles = "Admin")]
        //[CacheRemoveAspect(typeof(MemoryCacheManager))]
        [CacheRemoveAspect(typeof(RedisCacheManager))]
        public int Delete(Product entity)
        {
            return _productRepository.Delete(entity);
        }

        [MethodWorkingTimeAspect(1)]
        public Product Get(Expression<Func<Product, bool>> predicate)
        {
            return _mapper.Map<Product>(_productRepository.Get(predicate));
        }

        //[CacheAspect(typeof(MemoryCacheManager))]
        //[CacheAspect(typeof(RedisCacheManager), 60, typeof(Product))]
        [MethodWorkingTimeAspect(2)]
        public List<Product> GetAll(Expression<Func<Product, bool>> predicate = null)
        {
            return _mapper.Map<List<Product>>(_productRepository.GetAll(predicate));
        }

        //[CacheAspect(typeof(MemoryCacheManager))]
        [CacheAspect(typeof(RedisCacheManager), 60, typeof(ProductWithCategory))]
        [MethodWorkingTimeAspect(2)]
        public List<ProductWithCategory> GetAllProductWithCategory()
        {
            return _mapper.Map<List<ProductWithCategory>>(_productRepository.GetAllProductWithCategory());
        }

        [AuthorizationAspect(Roles = "Admin")]
        [FluentValidationAspect(typeof(ProductValidator))]
        //[CacheRemoveAspect(typeof(MemoryCacheManager))]
        [CacheRemoveAspect(typeof(RedisCacheManager))]
        public Product Update(Product entity)
        {
            return _productRepository.Update(entity);
        }
    }
}

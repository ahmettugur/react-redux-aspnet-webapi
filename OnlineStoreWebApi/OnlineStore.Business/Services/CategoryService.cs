using OnlineStore.Business.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Entity.Concrete;
using System.Linq.Expressions;
using OnlineStore.Data.Contracts;
using OnlineStore.Core.CrossCuttingConcerns.Aspects.Postsharp.AuthorizationAspects;
using OnlineStore.Core.CrossCuttingConcerns.Aspects.Postsharp.ValidationAspects;
using OnlineStore.Core.CrossCuttingConcerns.Aspects.Postsharp.CacheAsepcts;
using OnlineStore.Core.CrossCuttingConcerns.Aspects.Postsharp.ExceptionAspects;
using OnlineStore.Core.CrossCuttingConcerns.Caching.Microsoft;
using OnlineStore.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using OnlineStore.Business.ValidationRules.FluentValidation;
using OnlineStore.Core.CrossCuttingConcerns.Aspects.Postsharp.PerformanceAspects;
using OnlineStore.Core.CrossCuttingConcerns.Aspects.Postsharp.LogAspects;
using AutoMapper;

namespace OnlineStore.Business.Services
{
    [LogAspect(typeof(FileLogger))]
    [ExceptionLogAspect(typeof(FileLogger))]
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [AuthorizationAspect(Roles = "Admin")]
        [FluentValidationAspect(typeof(CategoryValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Category Add(Category entity)
        {
            return _categoryRepository.Add(entity);
        }


        [AuthorizationAspect(Roles = "Admin")]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public int Delete(Category entity)
        {
            return _categoryRepository.Delete(entity);
        }

        [MethodWorkingTimeAspect(1)]
        public Category Get(Expression<Func<Category, bool>> predicate)
        {
            return _mapper.Map<Category>(_categoryRepository.Get(predicate));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [MethodWorkingTimeAspect(2)]
        public List<Category> GetAll(Expression<Func<Category, bool>> predicate = null)
        {
            return _mapper.Map<List<Category>>(_categoryRepository.GetAll(predicate).OrderByDescending(_ => _.Id));
        }

        [AuthorizationAspect(Roles = "Admin")]
        [FluentValidationAspect(typeof(CategoryValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Category Update(Category entity)
        {
            return _categoryRepository.Update(entity);
        }
    }
}

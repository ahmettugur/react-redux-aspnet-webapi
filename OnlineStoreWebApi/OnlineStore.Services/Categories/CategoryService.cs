using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Entities;
using OnlineStore.Data.UnitOfWork;
using OnlineStore.Data.Repository;

namespace OnlineStore.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Category> _repository;
        public CategoryService(IUnitOfWork uow)
        {
            _uow = uow;
            _repository = _uow.GetRepository<Category>();
        }
        public void Add(Category category)
        {
            _repository.Add(category);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Delete(Category category)
        {
            _repository.Delete(category);
        }

        public Category Get(int id)
        {
            return _repository.Get(id);
        }

        public List<Category> GetAll()
        {
            return _repository.GetAll();
        }

        public List<Category> GetAll(Expression<Func<Category, bool>> predicate)
        {
            return _repository.GetAll(predicate);
        }

        public void Update(Category category)
        {
            _repository.Update(category);
        }
    }
}

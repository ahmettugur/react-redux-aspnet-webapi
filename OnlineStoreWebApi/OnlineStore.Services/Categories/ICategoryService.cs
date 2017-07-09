using OnlineStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Services.Categories
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        List<Category> GetAll(Expression<Func<Category, bool>> predicate);
        Category Get(int id);
        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);
        void Delete(int id);
    }
}

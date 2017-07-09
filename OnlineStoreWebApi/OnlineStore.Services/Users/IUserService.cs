using OnlineStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Services.Users
{
    public interface IUserService
    {
        List<User> GetAll();
        List<User> GetAll(Expression<Func<User, bool>> predicate);
        User Get(int id);
        User Get(Expression<Func<User, bool>> predicate);
        void Add(User product);
        void Update(User product);
        void Delete(User product);
        void Delete(int id);
    }
}

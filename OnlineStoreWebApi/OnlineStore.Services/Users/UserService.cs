using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Entities;
using OnlineStore.Data.UnitOfWork;
using OnlineStore.Data.Repository;

namespace OnlineStore.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<User> _repository;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
            _repository = _uow.GetRepository<User>();
        }
        public void Add(User user)
        {
            _repository.Add(user);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Delete(User user)
        {
            _repository.Delete(user);
        }

        public User Get(Expression<Func<User, bool>> predicate)
        {
            return _repository.Get(predicate);
        }

        public User Get(int id)
        {
            return _repository.Get(id);
        }

        public List<User> GetAll()
        {
            return _repository.GetAll();
        }

        public List<User> GetAll(Expression<Func<User, bool>> predicate)
        {
            return _repository.GetAll(predicate);
        }

        public void Update(User user)
        {
            _repository.Update(user);
        }
    }
}

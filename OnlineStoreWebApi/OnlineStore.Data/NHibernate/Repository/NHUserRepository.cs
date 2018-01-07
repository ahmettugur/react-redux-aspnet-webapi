using OnlineStore.Core.Repository.NHibernate;
using OnlineStore.Data.Contracts;
using OnlineStore.Entity.Concrete;
using System.Linq;
using NHibernate.Linq;
using System;

namespace OnlineStore.Data.NHibernate.Repository
{
    public class NHUserRepository : NHEntityRepository<User>, IUserRespository
    {

        private readonly NHibernateHelper _nHibernateHelper;

        public NHUserRepository(NHibernateHelper nHibernateHelper) : base(nHibernateHelper)
        {
            _nHibernateHelper = nHibernateHelper;
        }

        public string[] GetUserRoles(User user)
        {
            using (var session = _nHibernateHelper.OpenSession())
            {
                var roles = (from r in session.Query<Role>()
                             join ur in session.Query<UserRole>()
                             on r.Id equals ur.RoleId
                             where ur.UserId == user.UserId
                             select r.Name).ToArray();

                return roles;
            }
        }
    }
}

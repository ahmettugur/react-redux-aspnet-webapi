using OnlineStore.Core.Contracts.Entities;
using OnlineStore.Core.Contracts.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Repository.NHibernate
{
    public class NHEntityRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        NHibernateHelper _nHibernateHelper;

        public NHEntityRepository(NHibernateHelper nHibernateHelper)
        {
            _nHibernateHelper = nHibernateHelper;
        }

        public TEntity Add(TEntity entity)
        {
            using (var session = _nHibernateHelper.OpenSession())
            {
                session.Save(entity);
                session.Flush();
                return entity;
            }
        }

        public int Delete(TEntity entity)
        {
            using (var session = _nHibernateHelper.OpenSession())
            {
                session.Delete(entity);
                session.Flush();
                return 1;
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            using (var session = _nHibernateHelper.OpenSession())
            {
                return session.Query<TEntity>().FirstOrDefault(predicate);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            using (var session = _nHibernateHelper.OpenSession())
            {
                return predicate == null
                    ? session.Query<TEntity>().ToList()
                    : session.Query<TEntity>().Where(predicate).ToList();
            }
        }

        public TEntity Update(TEntity entity)
        {
            using (var session = _nHibernateHelper.OpenSession())
            {
                session.Update(entity);
                session.Flush();
                return entity;
            }
        }
    }
}

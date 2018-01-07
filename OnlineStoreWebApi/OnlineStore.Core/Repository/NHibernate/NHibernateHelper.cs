using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Repository.NHibernate
{
    public abstract class NHibernateHelper : IDisposable
    {
        static ISessionFactory sessionFactory;

        public virtual ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    sessionFactory = InitializeSessionFactory();
                }

                return sessionFactory;
            }
        }
        protected abstract ISessionFactory InitializeSessionFactory();

        public virtual ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}

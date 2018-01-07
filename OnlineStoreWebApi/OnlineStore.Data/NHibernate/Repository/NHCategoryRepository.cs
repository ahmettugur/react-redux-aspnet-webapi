using OnlineStore.Business.Contracts;
using OnlineStore.Core.Repository.NHibernate;
using OnlineStore.Data.Contracts;
using OnlineStore.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.NHibernate.Repository
{
    public class NHCategoryRepository : NHEntityRepository<Category>, ICategoryRepository
    {
        public NHCategoryRepository(NHibernateHelper nHibernateHelper) : base(nHibernateHelper)
        {
           
        }
    }
}

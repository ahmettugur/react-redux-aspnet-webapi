using Ninject.Modules;
using OnlineStore.Business.Contracts;
using OnlineStore.Business.Services;
using OnlineStore.Core.Repository.NHibernate;
using OnlineStore.Data.Contracts;
using OnlineStore.Data.Dapper.Repository;
using OnlineStore.Data.EntityFramework;
using OnlineStore.Data.EntityFramework.Repository;
using OnlineStore.Data.NHibernate.Helpers;
using OnlineStore.Data.NHibernate.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.DependencyResolvers.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            ///Services
            Bind<IProductService>().To<ProductService>();
            Bind<ICategoryService>().To<CategoryService>();
            Bind<IUserService>().To<UserService>();

            ///DapperRepositories
            Bind<IProductRepository>().To<DapperProductRepository>();
            Bind<ICategoryRepository>().To<DapperCategoryRepository>();
            Bind<IUserRespository>().To<DapperUserRepository>().InSingletonScope();

            ///NHibernate Repository
            //Bind<IProductRepository>().To<NHProductRepository>();
            //Bind<ICategoryRepository>().To<NHCategoryRepository>();
            //Bind<IUserRespository>().To<NHUserRepository>();
            //Bind<NHibernateHelper>().To<SqlServerHelper>();

            ///EfRepositories
            //Bind<IProductRepository>().To<EFProductRepository>();
            //Bind<ICategoryRepository>().To<EFCategoryRepository>();
            //Bind<IUserRespository>().To<EFUserRepository>().InSingletonScope();
            //Bind<DbContext>().To<OnlineStoreContext>();

        }
    }
}

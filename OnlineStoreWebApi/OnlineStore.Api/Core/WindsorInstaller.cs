using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.DynamicProxy;
using OnlineStore.Services.Products;
using OnlineStore.Services.Categories;
using OnlineStore.Data.Repository;
using OnlineStore.Data.UnitOfWork;
using OnlineStore.Data;
using OnlineStore.Services.Users;
using System.Web.Http.Controllers;
using System.Data.Entity;

namespace OnlineStore.Api.Core
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            RegisterService(container);
            RegisterHttpController(container);
            //RegisterInterceptor(container);
            
        }


        private void RegisterInterceptor(IWindsorContainer container)
        {
            container.Register(
                DependencyContainer.Descriptor
                .BasedOn<IInterceptor>()
                .WithService
                .Self()
                .LifestylePerWebRequest()
            );
        }
        private void RegisterHttpController(IWindsorContainer container)
        {
            container.Register(
                DependencyContainer.Descriptor
                .BasedOn<IHttpController>()
                .WithService
                .Self()
                .LifestylePerWebRequest()
            );
        }
        private void RegisterService(IWindsorContainer container)
        {
            container.Register(
             Component
             .For<DbContext>()
             .ImplementedBy<OnlineStoreContext>()
             .LifestylePerWebRequest()
            );
            container.Register(
                DependencyContainer.Descriptor
                .BasedOn<IUnitOfWork>()
                .WithService
                .AllInterfaces()
                .LifestylePerWebRequest()
            );
            container.Register(
                DependencyContainer.Descriptor
                .BasedOn(typeof(IGenericRepository<>))
                .WithService
                .AllInterfaces()
                .LifestylePerWebRequest()
            );

            container.Register(
                DependencyContainer.Descriptor
                .BasedOn<IProductService>()
                .WithService
                .AllInterfaces()
                .LifestylePerWebRequest()
            );
            container.Register(
                DependencyContainer.Descriptor
                .BasedOn<ICategoryService>()
                .WithService
                .AllInterfaces()
                .LifestylePerWebRequest()
            );
            container.Register(
                DependencyContainer.Descriptor
                .BasedOn<IUserService>()
                .WithService
                .AllInterfaces()
                .LifestylePerWebRequest()
            );
        }

    }
}

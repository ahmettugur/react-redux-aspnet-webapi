using AutoMapper;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.DependencyResolvers.Ninject
{
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMapper>().ToConstant(CreateConfiguration().CreateMapper()).InSingletonScope();
        }

        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg=>
            {
                cfg.AddProfiles(GetType().Assembly);
            });

            return config;
        }
    }
}

using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineStore.Core.Utilities.Mvc.Infastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel _kernel;

        public NinjectControllerFactory(params INinjectModule[] module)
        {
            _kernel = new StandardKernel(module);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return _kernel == null ? null : (IController)_kernel.Get(controllerType);
        }
    }
}

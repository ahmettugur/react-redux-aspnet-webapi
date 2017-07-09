using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Api.Core
{
    public static class DependencyContainer
    {
        public static IWindsorContainer WindsorContainer { get; private set; }
        public static FromAssemblyDescriptor Descriptor { get; private set; }

        private static bool bootstrapped;
        public static void Bootstrap()
        {
            if (!bootstrapped)
            {
                WindsorContainer = new WindsorContainer();
                var assemblyFilter = new AssemblyFilter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin"));
                Descriptor = Classes.FromAssemblyInDirectory(assemblyFilter);
                WindsorContainer.Install(FromAssembly.InDirectory(assemblyFilter));

                bootstrapped = true;
            }

        }
        public static T Resolve<T>()
        {
            return WindsorContainer.Resolve<T>();
        }
        public static object Resolve(Type type)
        {
            return WindsorContainer.Resolve(type);
        }
    }

}

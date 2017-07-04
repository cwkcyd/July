using July.Ioc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using System.Reflection;
using System.Linq;
using July.Extensions;
using Microsoft.Extensions.Logging;
using July.Startup;
using Microsoft.AspNetCore.Hosting;

namespace July.Modules
{
    public abstract class JulyModule
    {
        internal IStartupService StartupService { private get; set; }

        protected Assembly ThisAssembly => this.GetType().GetTypeInfo().Assembly;

        protected ILogger Logger
        {
            get
            {                
                ILoggerFactory loggerFactory;

                if (IocContainer.InstanceAccessable)
                {
                    loggerFactory = IocContainer.Instance.Resolve<ILoggerFactory>();
                }
                else
                {
                    loggerFactory = StartupService.LoggerFactory;
                }

                return loggerFactory.CreateLogger(this.GetType());
            }
        }

        protected IHostingEnvironment HostingEnvironment
        {
            get
            {
                if (IocContainer.InstanceAccessable)
                {
                    return IocContainer.Instance.Resolve<IHostingEnvironment>();
                }
                else
                {
                    return StartupService.HostingEnvironment;
                }
            }
        }

        public virtual void Initialize(IocBuilder builder)
        {
            builder.RegisterAssemblyByConvention(ThisAssembly);
        }

        public virtual void Load(IIocContainer iocContainer)
        {

        }

        public virtual void Start(IIocContainer iocContainer)
        {

        }

        public virtual void Shutdown()
        {

        }

        public static bool IsJulyModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return
                typeInfo.IsClass &&
                !typeInfo.IsAbstract &&
                !typeInfo.IsGenericType &&
                typeof(JulyModule).IsAssignableFrom(type);
        }

        /// <summary>
        /// Finds direct depended modules of a module (excluding given module).
        /// </summary>
        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            if (!IsJulyModule(moduleType))
            {
                throw new InvalidOperationException("This type is not a July module: " + moduleType.AssemblyQualifiedName);
            }

            var list = new List<Type>();

            if (moduleType.GetTypeInfo().IsDefined(typeof(DependOnAttribute), true))
            {
                var dependsOnAttributes = moduleType.GetTypeInfo().GetCustomAttributes(typeof(DependOnAttribute), true).Cast<DependOnAttribute>();
                foreach (var dependsOnAttribute in dependsOnAttributes)
                {
                    foreach (var dependedModuleType in dependsOnAttribute.DependOnModules)
                    {
                        list.Add(dependedModuleType);
                    }
                }
            }

            return list;
        }

        public static List<Type> FindDependedModuleTypesRecursivelyIncludingGivenModule(Type moduleType)
        {
            var list = new List<Type>();
            AddModuleAndDependenciesRecursively(list, moduleType);
            list.AddIfNotContains(typeof(JulyKernelModule));
            return list;
        }

        private static void AddModuleAndDependenciesRecursively(List<Type> modules, Type module)
        {
            if (!IsJulyModule(module))
            {
                throw new InvalidOperationException("This type is not a July module: " + module.AssemblyQualifiedName);
            }

            if (modules.Contains(module))
            {
                return;
            }

            modules.Add(module);

            var dependedModules = FindDependedModuleTypes(module);
            foreach (var dependedModule in dependedModules)
            {
                AddModuleAndDependenciesRecursively(modules, dependedModule);
            }
        }
    }
}

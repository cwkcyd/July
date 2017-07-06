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
using July.Settings;
using Microsoft.AspNetCore.Builder;

namespace July.Modules
{
    public abstract class JulyModule
    {
        internal IStartupService StartupService { private get; set; }

        protected Assembly ThisAssembly => this.GetType().GetTypeInfo().Assembly;

        protected IIocContainer IocContainer
        {
            get
            {
                if (!Ioc.IocContainer.InstanceAccessable)
                {
                    throw new InvalidOperationException("Cannot access IocContainer instance. It is available in Configure method and later methods");
                }

                return Ioc.IocContainer.Root;
            }
        }

        protected ILogger Logger
        {
            get
            {                
                ILoggerFactory loggerFactory;

                if (Ioc.IocContainer.InstanceAccessable)
                {
                    loggerFactory = Ioc.IocContainer.Root.Resolve<ILoggerFactory>();
                }
                else
                {
                    loggerFactory = StartupService.LoggerFactory;
                }

                return loggerFactory.CreateLogger(this.GetType());
            }
        }

        protected GlobalSettings Settings => GlobalSettings.Instance;

        protected IHostingEnvironment HostingEnvironment => Settings.HostingEnvironment();

        /// <summary>
        /// Trigger at first, settings should be configured in this method 
        /// </summary>
        public virtual void Initialize()
        {

        }

        /// <summary>
        /// Trigger after Initialize(), should register own services in this method.
        /// IocBuilder.RegisterAssemblyByConvention(ThisAssembly) will call by default. To disable this feature, do not call base.ConfigureServices(builder)
        /// </summary>
        /// <param name="builder"></param>
        public virtual void ConfigureServices(IocBuilder builder)
        {
            builder.RegisterAssemblyByConvention(ThisAssembly);
        }

        public virtual void Configure(IApplicationBuilder app)
        {

        }

        public virtual void OnApplicationStart()
        {

        }

        public virtual void OnApplicationShutdown()
        {

        }

        #region Static methods

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

        #endregion
    }
}

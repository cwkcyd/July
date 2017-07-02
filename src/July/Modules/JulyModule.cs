using July.Ioc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using July.Configuration;
using System.Reflection;
using System.Linq;
using July.Extensions;

namespace July.Modules
{
    public abstract class JulyModule
    {
        protected internal IStartupConfiguration Configuration { get; internal set; }

        public virtual void Initialize(IocBuilder builder)
        {

        }

        public virtual void Load(IIocContainer container)
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
                throw new InvalidOperationException("This type is not an ABP module: " + moduleType.AssemblyQualifiedName);
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
                throw new InvalidOperationException("This type is not an ABP module: " + module.AssemblyQualifiedName);
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

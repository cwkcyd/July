using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Autofac;
using Autofac.Builder;

namespace July.Ioc
{
    public static class IocBuilderExtensions
    {
        public static void RegisterAssemblyByConvention(this IocBuilder iocBuilder, Assembly assembly)
        {
            foreach (var type in assembly.DefinedTypes)
            {
                RegisterTypeByConvention(iocBuilder, type);
            }
        }

        private static void RegisterTypeByConvention(IocBuilder iocBuilder, Type type)
        {
            if (type.IsAbstract || type.IsInterface || type.IsGenericTypeDefinition)
            {
                return;
            }

            var registeration = iocBuilder.RegisterType(type)
                .AsSelf()
                .AsImplementedInterfaces()
                .PropertiesAutowired();

            if (typeof(ITransient).IsAssignableFrom(type))
            {
                registeration = registeration.InstancePerDependency();
            }
            else if (typeof(ISingleton).IsAssignableFrom(type))
            {
                registeration = registeration.SingleInstance();
            }
            else if (typeof(IScoped).IsAssignableFrom(type))
            {
                registeration = registeration.InstancePerLifetimeScope();
            }

            if (typeof(ILifetimeEvents).IsAssignableFrom(type))
            {
                registeration.OnActivating(e => { ((ILifetimeEvents)e.Instance).OnActivating(); });
                registeration.OnActivated(e => { ((ILifetimeEvents)e.Instance).OnActivated(); });
                registeration.OnRelease(e => {
                    ((ILifetimeEvents)e).OnRelease();
                });
            }
        }
    }
}

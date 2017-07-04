using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Autofac;
using Autofac.Builder;
using Microsoft.Extensions.DependencyInjection;
using July.Ioc.Conventions;
using July.Extensions;

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

        private static void RegisterTypeByConvention(IocBuilder iocBuilder, TypeInfo type)
        {
            if (type.IsAbstract || type.IsInterface || type.IsGenericTypeDefinition)
            {
                return;
            }

            var ignoreAttribute = type.GetCustomAttribute<IgnoreAttribute>();
            if (ignoreAttribute != null)
            {
                return;
            }

            var componentAttribute = type.AsType().GetFirstAttribute<ComponentAttribute>();
            if (componentAttribute == null)
            {
                return;
            }

            var registration = iocBuilder.RegisterType(type.AsType());

            IConventionRegister<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> register = new ConventionRegister<object, ConcreteReflectionActivatorData, SingleRegistrationStyle>();
            register.Register(registration, type.AsType());
        }
    }
}

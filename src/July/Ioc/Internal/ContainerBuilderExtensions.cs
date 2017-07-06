using Autofac;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace July.Ioc.Internal
{
    internal static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterDefaultServices(this ContainerBuilder builder)
        {
            builder.RegisterType<IocContainer>().As<IIocContainer>().As<IServiceProvider>().SingleInstance();
            builder.RegisterType<AutofacServiceScope>().As<IServiceScope>().SingleInstance();
            builder.RegisterType<IocScopeFactory>().As<IIocScopeFactory>().As<IServiceScopeFactory>().SingleInstance();

            return builder;
        }
    }
}

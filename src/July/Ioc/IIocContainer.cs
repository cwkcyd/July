using Autofac;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace July.Ioc
{
    public interface IIocContainer : IServiceProvider
    {
        object Resolve(Type type);

        bool IsRegistered(Type type);

        /// <summary>
        /// Get the root container
        /// </summary>
        IIocContainer CreateScope();
    }
}

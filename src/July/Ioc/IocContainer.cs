using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Core;
using Microsoft.Extensions.DependencyInjection;

namespace July.Ioc
{
    public class IocContainer : IIocContainer
    {
        private static IIocContainer _instance;

        public static IIocContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException("Ioc container has not been registered");
                }

                return _instance;
            }
        }

        internal static bool InstanceAccessable
        {
            get
            {
                return _instance != null;
            }
        }

        private ILifetimeScope LifetimeScope { get; set; }

        public IocContainer(ILifetimeScope lifetimeScope)
        {
            LifetimeScope = lifetimeScope ?? throw new ArgumentNullException(nameof(lifetimeScope));
            _instance = this;
        }

        #region IIocContainer

        public object Resolve(Type type)
        {
            return LifetimeScope.ResolveOptional(type);
        }

        public bool IsRegistered(Type type)
        {
            return LifetimeScope.IsRegistered(type);
        }

        public ILifetimeScope BeginLifetimeScope(Action<ContainerBuilder> builderAction)
        {
            if (builderAction != null)
            {
                return LifetimeScope.BeginLifetimeScope(builderAction);
            }
            else
            {
                return LifetimeScope.BeginLifetimeScope();
            }
        }

        #endregion
    }
}

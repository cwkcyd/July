using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Core;
using Microsoft.Extensions.DependencyInjection;

namespace July.Ioc
{
    public class IocContainer : IIocContainer, IStartable, IServiceProvider, ISupportRequiredService
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
        }

        #region IServiceProivder

        public object GetService(Type serviceType)
        {
            return LifetimeScope.ResolveOptional(serviceType);
        }

        #endregion

        #region ISupportRequiredService

        public object GetRequiredService(Type serviceType)
        {
            return LifetimeScope.Resolve(serviceType);
        }

        #endregion

        #region IStartable

        public void Start()
        {
            _instance = this;
        }

        #endregion

        #region IIocContainer

        public object Resolve(Type type)
        {
            return GetService(type);
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

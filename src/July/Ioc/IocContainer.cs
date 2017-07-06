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

        public static IIocContainer Root
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

        internal static IocContainer RootInstance
        {
            set
            {
                if (value.LifetimeScope.Tag != null && value.LifetimeScope.Tag.ToString() == Consts.LifetimeScope.ROOT)
                {
                    _instance = value;
                }
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

        #region IIocContainer

        public object Resolve(Type type)
        {
            return LifetimeScope.ResolveOptional(type);
        }

        public bool IsRegistered(Type type)
        {
            return LifetimeScope.IsRegistered(type);
        }

        public IIocContainer CreateScope()
        {
            return LifetimeScope.Resolve<IIocScopeFactory>().CreateIocContainer();
        }

        #endregion

        public object GetService(Type serviceType)
        {
            return LifetimeScope.ResolveOptional(serviceType);
        }
    }
}

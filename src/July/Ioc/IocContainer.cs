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

        private IComponentContext ComponentContext { get; set; }

        public IocContainer(IComponentContext componentContext)
        {
            ComponentContext = componentContext ?? throw new ArgumentNullException(nameof(componentContext));
        }

        #region IServiceProivder

        public object GetService(Type serviceType)
        {
            return ComponentContext.ResolveOptional(serviceType);
        }

        #endregion

        #region ISupportRequiredService

        public object GetRequiredService(Type serviceType)
        {
            return ComponentContext.Resolve(serviceType);
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
            return ComponentContext.IsRegistered(type);
        }

        #endregion
    }
}

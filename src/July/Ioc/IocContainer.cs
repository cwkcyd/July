using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace July.Ioc
{
    public class IocContainer : IIocContainer, IStartable
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

        public object GetService(Type serviceType)
        {
            return ComponentContext.ResolveOptional(serviceType);
        }

        public object GetRequiredService(Type serviceType)
        {
            return ComponentContext.Resolve(serviceType);
        }

        public void Start()
        {
            _instance = this;
        }
    }
}

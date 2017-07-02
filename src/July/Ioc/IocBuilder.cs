using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Autofac;

namespace July.Ioc
{
    public class IocBuilder : ContainerBuilder, IServiceCollection
    {
        public static IocBuilder New(IServiceCollection services, Type startupModule)
        {
            IocBuilder builder = new IocBuilder(services);

            return builder;
        }

        #region Implement the IServiceCollection interfaces

        public int IndexOf(ServiceDescriptor item)
        {
            return ServiceCollection.IndexOf(item);
        }

        public void Insert(int index, ServiceDescriptor item)
        {
            ServiceCollection.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ServiceCollection.RemoveAt(index);
        }

        public void Add(ServiceDescriptor item)
        {
            ServiceCollection.Add(item);
        }

        public void Clear()
        {
            ServiceCollection.Clear();
        }

        public bool Contains(ServiceDescriptor item)
        {
            return ServiceCollection.Contains(item);
        }

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            ServiceCollection.CopyTo(array, arrayIndex);
        }

        public bool Remove(ServiceDescriptor item)
        {
            return ServiceCollection.Remove(item);
        }

        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            return ServiceCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ServiceCollection.GetEnumerator();
        }

        public int Count => ServiceCollection.Count;

        public bool IsReadOnly => ServiceCollection.IsReadOnly;

        public ServiceDescriptor this[int index] { get => ServiceCollection[index]; set => ServiceCollection[index] = value; }

        #endregion
        
        private IServiceCollection ServiceCollection { get; set; }

        private IocBuilder(IServiceCollection services)
        {
            ServiceCollection = services ?? throw new ArgumentNullException(nameof(services));
        }

        public IServiceProvider Build()
        {
            this.RegisterType<IocContainer>().AsSelf().As<IIocContainer>().As<IServiceProvider>().SingleInstance();
            this.RegisterType<AutofacServiceScopeFactory>().As<IServiceScopeFactory>();

            this.Populate(ServiceCollection);

            var container = base.Build();

            return container.Resolve<IocContainer>();
        }
    }
}

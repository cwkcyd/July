using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Autofac;
using July.Startup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using July.Ioc.Internal;

namespace July.Ioc
{
    public class IocBuilder : ContainerBuilder, IServiceCollection
    {
        public static IocBuilder New(IServiceCollection services, IStartupService startupService)
        {
            IocBuilder builder = new IocBuilder(services, startupService);

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

        private IStartupService StartupService { get; set; }

        private IocBuilder(IServiceCollection services, IStartupService startupService)
        {
            ServiceCollection = services ?? throw new ArgumentNullException(nameof(services));
            StartupService = startupService ?? throw new ArgumentNullException(nameof(startupService));
        }

        public IServiceProvider Build()
        {
            this.Populate(ServiceCollection);

            //Register startup service at last, to prevent user replace them.
            this.RegisterInstance(StartupService).As<IStartupService>().SingleInstance();
            this.RegisterInstance(StartupService.HostingEnvironment).As<IHostingEnvironment>().SingleInstance();
            this.RegisterInstance(StartupService.LoggerFactory).As<ILoggerFactory>().SingleInstance();
            this.RegisterInstance(StartupService.Configuration).As<IConfiguration>().SingleInstance();

            this.RegisterDefaultServices();

            var container = base.Build();

            IocContainer iocContainer = container.Resolve<IocContainer>();

            IocContainer.RootInstance = iocContainer;

            return iocContainer;
        }
    }
}

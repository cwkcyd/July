using Autofac;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using July.Ioc.Internal;
using System.Threading;

namespace July.Ioc
{
    internal class IocScopeFactory : IIocScopeFactory
    {
        private readonly ILifetimeScope _lifetimeScope;

        private static long _scopeId = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="IocScopeFactory"/> class.
        /// </summary>
        /// <param name="lifetimeScope">The lifetime scope.</param>
        public IocScopeFactory(ILifetimeScope lifetimeScope)
        {
            this._lifetimeScope = lifetimeScope;
        }

        /// <summary>
        /// Creates an <see cref="IServiceScope" /> which contains an
        /// <see cref="System.IServiceProvider" /> used to resolve dependencies within
        /// the scope.
        /// </summary>
        /// <returns>
        /// An <see cref="IServiceScope" /> controlling the lifetime of the scope. Once
        /// this is disposed, any scoped services that have been resolved
        /// from the <see cref="IServiceScope.ServiceProvider" />
        /// will also be disposed.
        /// </returns>
        public IServiceScope CreateScope()
        {
            return CreateIocContainer().Resolve<IServiceScope>();
        }

        public IIocContainer CreateIocContainer()
        {
            var id = Interlocked.Increment(ref _scopeId);

            return CreateIocContainer(id.ToString());
        }

        private IIocContainer CreateIocContainer(string tag)
        {
            var childScope = _lifetimeScope.BeginLifetimeScope(tag, builder =>
            {
                builder.RegisterDefaultServices();
            });

            return childScope.Resolve<IocContainer>();
        }
    }
}

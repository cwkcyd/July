using July.Configuration;
using July.Ioc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace July.Bootstrap
{
    public abstract class ApplicationBase
    {
        protected IStartupConfiguration StartupConfiguration { get; private set; }

        protected abstract Type StartupModule { get; }

        public ApplicationBase(IStartupConfiguration startupConfiguration)
        {
            StartupConfiguration = startupConfiguration ?? throw new ArgumentNullException(nameof(startupConfiguration));
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var builder = IocBuilder.New(services, StartupModule);

            Register(builder);

            return builder.Build();
        }

        public virtual void Register(IocBuilder builder)
        {

        }

        public void Configure(IApplicationBuilder app)
        {
            Run(app);
        }

        protected virtual void Run(IApplicationBuilder app)
        {

        }
    }
}

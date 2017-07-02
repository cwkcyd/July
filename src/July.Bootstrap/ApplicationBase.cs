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
        protected ApplicationOptions Options { get; private set; }

        protected abstract Type StartupModule { get; }

        public ApplicationBase(ApplicationOptions options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var builder = IocBuilder.New(services, StartupModule);

            Register(services);

            return builder.Build();
        }

        public virtual void Register(IServiceCollection services)
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

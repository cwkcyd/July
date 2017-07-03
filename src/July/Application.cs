using July.Configuration;
using July.Ioc;
using July.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace July
{
    public abstract class Application
    {
        protected IStartupConfiguration StartupConfiguration { get; private set; }

        public abstract Type StartupModule { get; }

        public Application(IStartupConfiguration startupConfiguration)
        {
            StartupConfiguration = startupConfiguration ?? throw new ArgumentNullException(nameof(startupConfiguration));
        }

        public Application()
        {

        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var builder = IocBuilder.New(services, StartupModule);

            new JulyModuleManager(StartupModule).Initialize(builder, StartupConfiguration);

            return builder.Build();
        }

        public void Configure(IApplicationBuilder app)
        {
            var manager = app.ApplicationServices.GetService<JulyModuleManager>();
            var iocContainer = app.ApplicationServices.GetService<IIocContainer>();

            manager.Load(iocContainer);

            Run(app);
        }

        public abstract void Run(IApplicationBuilder app);
    }
}

using July.Ioc;
using July.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace July.Startup
{
    public abstract class Application
    {
        protected IStartupService StartupService { get; private set; }

        public abstract Type StartupModule { get; }

        public Application(IStartupService startupService)
        {
            StartupService = startupService ?? throw new ArgumentNullException(nameof(startupService));
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var builder = IocBuilder.New(services, StartupService);

            new JulyModuleManager(StartupModule).Initialize(builder, StartupService);

            return builder.Build();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime applicationLifetime)
        {
            var manager = app.ApplicationServices.GetService<JulyModuleManager>();
            var iocContainer = app.ApplicationServices.GetService<IIocContainer>();

            manager.Load(iocContainer);
            
            //register application lifetime events
            applicationLifetime.ApplicationStarted.Register(() => { manager.Start(iocContainer); });
            applicationLifetime.ApplicationStopping.Register(manager.Shutdown);            

            Run(app, env, loggerFactory, applicationLifetime);
        }

        public abstract void Run(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime applicationLifetime);
    }
}

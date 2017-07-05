using July.Ioc;
using July.Modules;
using July.Settings;
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

            GlobalSettings.Initialize(StartupService.Configuration, startupService.HostingEnvironment);
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var builder = IocBuilder.New(services, StartupService);

            JulyModuleManager manager = new JulyModuleManager(StartupModule);
            manager.Initialize(builder, StartupService);

            manager.ConfigureServices(builder);

            return builder.Build();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime applicationLifetime)
        {
            var manager = app.ApplicationServices.GetService<JulyModuleManager>();

            manager.Configure(app);
            
            //register application lifetime events
            applicationLifetime.ApplicationStarted.Register(() => { manager.OnApplicationStart(); });
            applicationLifetime.ApplicationStopping.Register(manager.OnApplicationShutdown);            

            Run(app, env, loggerFactory, applicationLifetime);
        }

        public abstract void Run(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime applicationLifetime);
    }
}

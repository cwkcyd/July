using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using July.Configuration;
using Microsoft.Extensions.Logging;

namespace July.Bootstrap
{
    public sealed class Bootstrapper<TApplication> : IBootstraper<TApplication>
        where TApplication : Application
    {
        private IWebHostBuilder WebHostBuilder { get; set; }

        private List<Action<IWebHostBuilder>> BuilderDelegates { get; set; } = new List<Action<IWebHostBuilder>>();

        public Bootstrapper(string[] args)
        {
            WebHostBuilder = WebHost.CreateDefaultBuilder(args);
        }

        public void ConfigureBuilder(Action<IWebHostBuilder> builderAction)
        {
            if (builderAction == null)
            {
                throw new ArgumentNullException(nameof(builderAction));
            }

            BuilderDelegates.Add(builderAction);
        }

        public void Run()
        {
            IWebHost webHost = BuildWebHost();
            webHost.Run();
        }

        public async Task RunAsync()
        {
            IWebHost webHost = BuildWebHost();
            await webHost.RunAsync();
        }

        private void RegisterStartupServices(IServiceCollection services)
        {
            services.AddSingleton<IStartupConfiguration, JulyStartupConfiguration>();
        }

        private IWebHost BuildWebHost()
        {
            foreach (var @delegate in BuilderDelegates)
            {
                @delegate.Invoke(WebHostBuilder);
            }

            WebHostBuilder.ConfigureServices(RegisterStartupServices);

            return WebHostBuilder.UseStartup<TApplication>().Build();
        }
    }
}

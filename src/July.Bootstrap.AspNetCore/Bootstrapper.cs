using July.Startup;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace July.Bootstrap.AspNetCore
{
    public class Bootstrapper<TApplication> : IBootstrapper<TApplication>
        where TApplication : Application
    {
        private IWebHostBuilder _webHostBuilder;        

        private List<Action<IWebHostBuilder>> _webHostBuilderDelegates = new List<Action<IWebHostBuilder>>();

        public Bootstrapper(string[] args)
        {
            _webHostBuilder = WebHost.CreateDefaultBuilder(args);
        }

        public Bootstrapper<TApplication> ConfigureWebHostBuilder(Action<IWebHostBuilder> builderAction)
        {
            if (builderAction == null)
            {
                throw new ArgumentNullException(nameof(builderAction));
            }

            _webHostBuilderDelegates.Add(builderAction);

            return this;
        }

        public void Run()
        {
            BuildWebHost().Run();
        }

        public async Task RunAsync()
        {
            await BuildWebHost().RunAsync();
        }

        private IWebHost BuildWebHost()
        {
            _webHostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<IStartupService, StartupService>();
            });

            foreach (var @delegate in _webHostBuilderDelegates)
            {
                @delegate.Invoke(_webHostBuilder);
            }

            _webHostBuilder.UseStartup<TApplication>();

            return _webHostBuilder.Build();
        }
    }
}

using July.Startup;
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

        private List<Action<IConfigurationBuilder>> _configurationDelegates = new List<Action<IConfigurationBuilder>>();

        private List<Action<IWebHostBuilder>> _webHostBuilderDelegates = new List<Action<IWebHostBuilder>>();

        private string[] _args;

        public Bootstrapper(string[] args)
        {
            _args = args;

            _webHostBuilder = new WebHostBuilder()
                .ConfigureServices(services => services.AddLogging());
        }

        public Bootstrapper<TApplication> ConfigureConfiguration(Action<IConfigurationBuilder> builderAction)
        {
            if (builderAction == null)
            {
                throw new ArgumentNullException(nameof(builderAction));
            }

            _configurationDelegates.Add(builderAction);

            return this;
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

        public Task RunAsync()
        {
            throw new NotSupportedException();
        }

        private IWebHost BuildWebHost()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(_args);
            foreach (var @delegate in _configurationDelegates)
            {
                @delegate.Invoke(configurationBuilder);
            }
            IConfiguration configuration = configurationBuilder.Build();
            _webHostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton(configuration);
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

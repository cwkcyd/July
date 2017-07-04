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
        private Func<IWebHostBuilder> _webHostBuilderFunc;

        public Bootstrapper(Func<IWebHostBuilder> webHostBuilderFunc)
        {
            _webHostBuilderFunc = webHostBuilderFunc ?? throw new ArgumentNullException(nameof(webHostBuilderFunc));
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
            IWebHostBuilder builder = _webHostBuilderFunc();
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IStartupService, StartupService>();
            });

            builder.UseStartup<TApplication>();
            return builder.Build();
        }
    }
}

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
        where TApplication : class, IApplication
    {
        private IWebHostBuilder WebHostBuilder { get; set; }

        private Action<IWebHostBuilder> WebHostBuilderAction { get; set; }

        public Bootstrapper(string[] args)
        {
            WebHostBuilder = WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(RegisterStartupServices);
        }

        public void ConfigureWebHostBuilder(Action<IWebHostBuilder> webHostBuilderAction)
        {
            WebHostBuilderAction = webHostBuilderAction;
        }

        public void Run()
        {
            WebHostBuilderAction?.Invoke(WebHostBuilder);

            IWebHost webHost = WebHostBuilder.UseStartup<TApplication>().Build();
            webHost.Run();
        }

        public async Task RunAsync()
        {
            WebHostBuilderAction?.Invoke(WebHostBuilder);

            IWebHost webHost = WebHostBuilder.UseStartup<TApplication>().Build();
            await webHost.RunAsync();
        }        

        private void RegisterStartupServices(IServiceCollection services)
        {
            services.AddSingleton<IStartupConfiguration ,JulyStartupConfiguration>();
        }
    }
}

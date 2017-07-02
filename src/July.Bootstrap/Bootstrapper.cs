using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace July.Bootstrap
{
    public sealed class Bootstrapper
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

        public void Run<TApplication>() where TApplication : ApplicationBase
        {
            WebHostBuilderAction?.Invoke(WebHostBuilder);

            IWebHost webHost = WebHostBuilder.UseStartup<TApplication>().Build();
            webHost.Run();
        }

        public async Task RunAsync<TApplication>() where TApplication : ApplicationBase
        {
            WebHostBuilderAction?.Invoke(WebHostBuilder);

            IWebHost webHost = WebHostBuilder.UseStartup<TApplication>().Build();
            await webHost.RunAsync();
        }        

        private void RegisterStartupServices(IServiceCollection services)
        {
            services.AddSingleton<ApplicationOptions>();
        }
    }
}

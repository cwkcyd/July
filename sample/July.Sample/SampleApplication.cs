using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using July.Startup;
using Microsoft.Extensions.DependencyInjection;
using July.Bootstrap.AspNetCore;
using Microsoft.AspNetCore;

namespace July.Sample
{
    public class SampleApplication : Application
    {
        public static void Main(string[] args)
        {
            new Bootstrapper<SampleApplication>(() => WebHost.CreateDefaultBuilder(args)).Run();
        }

        public SampleApplication(IStartupService startupService) : base(startupService)
        {

        }

        public override Type StartupModule => typeof(SampleModule);

        public override void Run(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime applicationLifetime)
        {            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}

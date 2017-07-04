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

namespace July.Sample
{
    public class SampleApplication : Application
    {
        public SampleApplication(IStartupService startupConfiguration) : base(startupConfiguration)
        {
        }

        public override Type StartupModule => typeof(SampleModule);

        public override void Run(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime applicationLifetime)
        {
            loggerFactory.AddConsole();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}

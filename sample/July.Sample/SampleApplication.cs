using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using July.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace July.Sample
{
    public class SampleApplication : Application
    {
        public SampleApplication(IStartupConfiguration startupConfiguration) : base(startupConfiguration)
        {
        }

        public override Type StartupModule => typeof(SampleModule);

        public override void Run(IApplicationBuilder app)
        {
            StartupConfiguration.LoggerFactory.AddConsole();

            if (StartupConfiguration.HostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}

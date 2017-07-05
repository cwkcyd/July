using July.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using July.Ioc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using July.Settings;
using Microsoft.AspNetCore.Hosting;

namespace July.Sample
{
    public class SampleModule : JulyModule
    {
        public override void ConfigureServices(IocBuilder builder)
        {
            base.ConfigureServices(builder);

            builder.AddMvc();
        }

        public override void Configure(IApplicationBuilder app)
        {
            if (Settings.HostingEnvironment().IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using July.Bootstrap;
using Microsoft.AspNetCore.Hosting;
using July.Ioc;
using July.Configuration;

namespace July.Sample
{
    public class Startup : JulyApplication
    {
        public Startup(IStartupConfiguration startupConfiguration)
            : base(startupConfiguration)
        {

        }

        public override Type StartupModule => typeof(JulySampleModule);

        public override void Run(IApplicationBuilder app)
        {
            if (StartupConfiguration.HostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
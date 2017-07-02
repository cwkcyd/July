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
    public class Startup : ApplicationBase
    {
        public Startup(IStartupConfiguration startupConfiguration)
            : base(startupConfiguration)
        {

        }

        protected override Type StartupModule => typeof(Startup);

        // This method gets called by the runtime. Use this method to add services to the container.
        public override void Register(IocBuilder builder)
        {
            builder.AddMvc();
        }

        protected override void Run(IApplicationBuilder app)
        {
            base.Run(app);

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
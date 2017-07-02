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

namespace July.Sample
{
    public class Startup : ApplicationBase
    {
        public Startup(ApplicationOptions options)
            : base(options)
        {

        }

        protected override Type StartupModule => typeof(Startup);

        // This method gets called by the runtime. Use this method to add services to the container.
        public override void Register(IServiceCollection services)
        {
            services.AddMvc();
        }

        protected override void Run(IApplicationBuilder app)
        {
            base.Run(app);

            if (Options.HostingEnvironment.IsDevelopment())
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
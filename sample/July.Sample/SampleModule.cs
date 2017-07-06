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
using July.Events;
using Autofac;

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

    [Transient]
    public class TestService : IDisposable
    {
        private ILifetimeScope _scope;

        public TestService(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public void Dispose()
        {

        }

        public void Test()
        {
            var cs = _scope.BeginLifetimeScope(builder =>
            {
                builder.RegisterInstance<object>(new object()).SingleInstance();
            });

            var ev = cs.Resolve<IEventBus>();
        }
    }
}

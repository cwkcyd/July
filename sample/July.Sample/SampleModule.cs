using July.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using July.Ioc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace July.Sample
{
    public class SampleModule : JulyModule
    {
        public override void ConfigureServices(IocBuilder builder)
        {
            base.ConfigureServices(builder);

            builder.AddMvc();
        }

        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
        }
    }
}

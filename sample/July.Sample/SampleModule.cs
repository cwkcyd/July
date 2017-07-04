using July.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using July.Ioc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace July.Sample
{
    public class SampleModule : JulyModule
    {
        public override void Initialize(IocBuilder builder)
        {
            base.Initialize(builder);

            builder.AddMvc();
        }
    }
}

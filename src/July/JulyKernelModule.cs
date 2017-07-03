using July.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using July.Ioc;
using Autofac;
using July.Events;

namespace July
{
    public class JulyKernelModule : JulyModule
    {
        public override void Initialize(IocBuilder builder)
        {
            base.Initialize(builder);

            //Dot not call this
            //builder.RegisterAssemblyByConvention(ThisAssembly);

            builder.RegisterType<EventBus>().As<IEventBus>().PropertiesAutowired().SingleInstance();
        }

        public override void Load(IIocContainer container)
        {
            base.Load(container);
        }
    }
}

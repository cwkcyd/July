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

            builder.RegisterType<EventBus>().As<IEventBus>().SingleInstance();
        }

        public override void Load(IIocContainer container)
        {
            base.Load(container);
        }
    }
}

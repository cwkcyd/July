using July.Ioc.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace July.Ioc
{
    public class IocConventionOptions
    {
        public List<IConventionRegister> ConventionRegisters { get; private set; }

        public IocConventionOptions()
        {
            ConventionRegisters = new List<IConventionRegister>();

            ConventionRegisters.Add(new ComponentRegister());
            ConventionRegisters.Add(new LifetimeEventsRegister());
            ConventionRegisters.Add(new AspectRegister());
        }
    }
}

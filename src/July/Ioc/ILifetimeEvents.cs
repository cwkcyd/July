using System;
using System.Collections.Generic;
using System.Text;

namespace July.Ioc
{
    public interface ILifetimeEvents
    {
        void OnActivating();

        void OnActivated();

        void OnRelease();
    }
}

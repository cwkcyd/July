using July.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace July.Events.Internal
{
    internal class IocEventHandler<TEventData, TEventHandler> : IEventHandler<TEventData>
    {
        public IocEventHandler()
        {

        }

        public void Handle(TEventData eventData)
        {
            IEventHandler<TEventData> handler = IocContainer.Instance.Resolve<TEventHandler>() as IEventHandler<TEventData>;

            if (handler == null)
            {
                throw new InvalidOperationException("Cannot create event handler: " + typeof(TEventHandler).FullName + ", make sure it implements IEventHandler<" + typeof(TEventData).Name + ">");
            }

            handler.Handle(eventData);
        }
    }
}

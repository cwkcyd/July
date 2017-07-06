using July.Ioc;

namespace July.Events
{
    public interface IEventHandler
    {

    }
    
    [Transient]
    public interface IEventHandler<TEventData> : IEventHandler
        where TEventData : IEventData
    {
        void Handle(TEventData eventData);
    }
}

using July.Ioc;

namespace July.Events
{
    public interface IEventHandler
    {

    }
    
    [Transient]
    public interface IEventHandler<TEventData> : IEventHandler
    {
        void Handle(TEventData eventData);
    }
}

namespace Eflatun.EventBus.interfaces
{
    public interface IEventListener<in TEvent>
        where TEvent : IEvent
    {
        void OnEvent(object sender, TEvent @event);
    }
}

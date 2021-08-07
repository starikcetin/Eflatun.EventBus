namespace Eflatun.EventBus
{
    public delegate void EventHandler<in TEvent>(EventMetadata metadata, TEvent @event) where TEvent : IEvent;
}

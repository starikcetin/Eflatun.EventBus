namespace Eflatun.EventBus
{
    public delegate void Listener<in TEvent>(EventMetadata metadata, TEvent @event) where TEvent : IEvent;
}

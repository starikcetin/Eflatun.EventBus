namespace Eflatun.EventBus
{
    public delegate void EventHandler<in TEvent>(object sender, TEvent @event) where TEvent : IEvent;
}

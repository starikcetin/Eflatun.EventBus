namespace Eflatun.EventBus.interfaces
{
    public interface IEventListener<TEvent>
        where TEvent : IEvent
    {
        void OnEvent(IEventEmitter<TEvent> sender, TEvent @event);
    }

    public interface IEventListener<TEvent, TArgs> : IEventListener<TEvent>
        where TEvent : IEvent<TArgs>
        where TArgs : IEventArguments
    {
    }
}

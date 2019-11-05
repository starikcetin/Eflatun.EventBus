namespace Eflatun.EventBus.interfaces
{
    public interface IEventEmitter<TEvent>
        where TEvent : IEvent
    {
    }

    public interface IEventEmitter<TEvent, TArgs> : IEventEmitter<TEvent>
        where TEvent : IEvent<TArgs>
        where TArgs : IEventArguments
    {
    }
}

namespace Eflatun.EventBus
{
    public interface IEventEmitter<TEvent, TArgs>
        where TEvent : IEvent<TArgs>
        where TArgs : IEventArguments
    {
    }
}

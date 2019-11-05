namespace Eflatun.EventBus
{
    public interface IEventListener<TEvent, TArgs>
        where TEvent : IEvent<TArgs>
        where TArgs : IEventArguments
    {
        void OnEvent(IEventEmitter<TEvent, TArgs> sender, TEvent @event);
    }
}

namespace Eflatun.EventBus
{
    public interface IEvent
    {
    }

    public interface IEvent<out TArgs> : IEvent where TArgs : IEventArguments
    {
        TArgs Arguments { get; }
    }
}

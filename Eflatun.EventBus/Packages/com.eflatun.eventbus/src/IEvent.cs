using Eflatun.EventBus.utils;

namespace Eflatun.EventBus
{
    public interface IEvent<out TArgs>
        where TArgs : IEventArguments
    {
        TArgs Arguments { get; }
    }
}

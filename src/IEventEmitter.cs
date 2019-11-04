namespace Eflatun.EventBus
{
    public interface IEventEmitter<T>
        where T : Event<T>
    {
    }
}

namespace Eflatun.EventBus
{
    public interface IEventListener<in T>
        where T : Event<T>
    {
        void OnEvent(T @event);
    }
}

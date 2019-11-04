namespace Eflatun.EventBus.Sample
{
    public abstract class Event<T> where T : Event<T>
    {
    }
}

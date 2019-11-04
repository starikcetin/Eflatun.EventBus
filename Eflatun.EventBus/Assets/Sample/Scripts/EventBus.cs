using System.Collections.Generic;

namespace Eflatun.EventBus.Sample
{
    public static class EventBus<T> where T : Event<T>
    {
        private static readonly List<IEventListener<T>> _listeners = new List<IEventListener<T>>();

        public static void Emit(T @event)
        {
            _listeners.ForEach(x => x.OnEvent(@event));
        }

        public static void Listen(IEventListener<T> listener)
        {
            _listeners.Add(listener);
        }
    }
}

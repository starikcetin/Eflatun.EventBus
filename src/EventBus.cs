using System.Collections.Generic;

namespace Eflatun.EventBus
{
    public class EventBus<T>
        where T : Event<T>
    {
        private readonly List<IEventListener<T>> _listeners = new List<IEventListener<T>>();

        public void Emit(T @event)
        {
            _listeners.ForEach(x => x.OnEvent(@event));
        }

        public void Listen(IEventListener<T> listener)
        {
            _listeners.Add(listener);
        }
    }
}

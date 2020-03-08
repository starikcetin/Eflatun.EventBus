using System.Collections.Generic;

namespace Eflatun.EventBus
{
    public class EventBus<TEvent>
        where TEvent : IEvent
    {
        private readonly List<EventHandler<TEvent>> _listeners = new List<EventHandler<TEvent>>();

        public void Emit(object sender, TEvent @event)
        {
            _listeners.ForEach(x => x?.Invoke(sender, @event));
        }

        public void Listen(EventHandler<TEvent> listener)
        {
            _listeners.Add(listener);
        }
    }
}

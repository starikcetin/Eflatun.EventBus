using System.Collections.Generic;
using Eflatun.EventBus.interfaces;

namespace Eflatun.EventBus
{
    public class EventBus<TEvent>
        where TEvent : IEvent
    {
        private readonly List<IEventListener<TEvent>> _listeners = new List<IEventListener<TEvent>>();

        public void Emit(object sender, TEvent @event)
        {
            _listeners.ForEach(x => x.OnEvent(sender, @event));
        }

        public void Listen(IEventListener<TEvent> listener)
        {
            _listeners.Add(listener);
        }
    }
}

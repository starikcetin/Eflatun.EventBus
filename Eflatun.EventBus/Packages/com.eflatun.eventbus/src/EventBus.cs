using System.Collections.Generic;

namespace Eflatun.EventBus
{
    public class EventBus<TEvent, TArgs>
        where TEvent : IEvent<TArgs>
        where TArgs : IEventArguments
    {
        private readonly List<IEventListener<TEvent, TArgs>> _listeners = new List<IEventListener<TEvent, TArgs>>();

        public void Emit(IEventEmitter<TEvent, TArgs> sender, TEvent @event)
        {
            _listeners.ForEach(x => x.OnEvent(sender, @event));
        }

        public void Listen(IEventListener<TEvent, TArgs> listener)
        {
            _listeners.Add(listener);
        }
    }
}

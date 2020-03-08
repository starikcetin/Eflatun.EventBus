using System;
using System.Collections.Generic;

namespace Eflatun.EventBus
{
    public class EventBus
    {
        private readonly Dictionary<Type, IEventBus> _eventBuses = new Dictionary<Type, IEventBus>();

        public void Emit<TEvent>(object sender, TEvent @event) where TEvent : IEvent
        {
            var eventBus = EnsureAndGetBus<TEvent>();
            eventBus.Emit(sender, @event);
        }

        public void Listen<TEvent>(EventHandler<TEvent> listener) where TEvent : IEvent
        {
            var eventBus = EnsureAndGetBus<TEvent>();
            eventBus.Listen(listener);
        }

        private __EventBus<TEvent> EnsureAndGetBus<TEvent>() where TEvent : IEvent
        {
            var eventType = typeof(TEvent);

            if (!_eventBuses.ContainsKey(eventType))
            {
                _eventBuses[eventType] = new __EventBus<TEvent>();
            }

            return (__EventBus<TEvent>) _eventBuses[eventType];
        }

        private class IEventBus
        {
        }

        private class __EventBus<TEvent> : IEventBus
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
}

using System.Collections.Generic;

namespace Eflatun.EventBus
{
    public class EventBus<TEvent>
        where TEvent : IEvent
    {
        private readonly List<EventHandler<TEvent>> _listenersBefore = new List<EventHandler<TEvent>>();
        private readonly List<EventHandler<TEvent>> _listeners = new List<EventHandler<TEvent>>();
        private readonly List<EventHandler<TEvent>> _listenersAfter = new List<EventHandler<TEvent>>();

        /// <summary>
        /// Emit an event.
        /// </summary>
        public void Emit(object sender, TEvent @event)
        {
            _listenersBefore.ForEach(x => x?.Invoke(sender, @event));
            _listeners.ForEach(x => x?.Invoke(sender, @event));
            _listenersAfter.ForEach(x => x?.Invoke(sender, @event));
        }

        /// <summary>
        /// Add a listener that is invoked before regular listeners.
        /// </summary>
        public void AddListenerBefore(EventHandler<TEvent> listener)
        {
            _listenersBefore.Add(listener);
        }

        /// <summary>
        /// Add a regular listener.
        /// </summary>
        public void AddListener(EventHandler<TEvent> listener)
        {
            _listeners.Add(listener);
        }

        /// <summary>
        /// Add a listener that is invoked after the regular listeners.
        /// </summary>
        public void AddListenerAfter(EventHandler<TEvent> listener)
        {
            _listenersAfter.Add(listener);
        }

        /// <summary>
        /// Remove a listener from the regular list.
        /// </summary>
        public void RemoveListener(EventHandler<TEvent> listener)
        {
            _listeners.Remove(listener);
        }

        /// <summary>
        /// Remove a listener from the before list.
        /// </summary>
        public void RemoveListenerBefore(EventHandler<TEvent> listener)
        {
            _listenersBefore.Remove(listener);
        }

        /// <summary>
        /// Remove a listener from the after list.
        /// </summary>
        public void RemoveListenerAfter(EventHandler<TEvent> listener)
        {
            _listenersAfter.Remove(listener);
        }
    }
}

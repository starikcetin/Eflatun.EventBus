using System;
using System.Collections.Generic;

namespace Eflatun.EventBus
{
    internal class ChannelContext<TEvent> where TEvent : IEvent
    {
        private readonly List<EventHandler<TEvent>> _beforeListeners = new List<EventHandler<TEvent>>();
        private readonly List<EventHandler<TEvent>> _regularListeners = new List<EventHandler<TEvent>>();
        private readonly List<EventHandler<TEvent>> _afterListeners = new List<EventHandler<TEvent>>();

        internal void Send(object sender, TEvent @event)
        {
            _beforeListeners.ForEach(listener => listener.Invoke(sender, @event));
            _regularListeners.ForEach(listener => listener.Invoke(sender, @event));
            _afterListeners.ForEach(listener => listener.Invoke(sender, @event));
        }

        internal void AddListener(ListenPhase phase, EventHandler<TEvent> listener)
        {
            switch (phase)
            {
                case ListenPhase.Before:
                    _beforeListeners.Add(listener);
                    break;

                case ListenPhase.Regular:
                    _regularListeners.Add(listener);
                    break;

                case ListenPhase.After:
                    _afterListeners.Add(listener);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(phase), phase, null);
            }
        }

        internal void RemoveListener(ListenPhase phase, EventHandler<TEvent> listener)
        {
            switch (phase)
            {
                case ListenPhase.Before:
                    _beforeListeners.Remove(listener);
                    break;

                case ListenPhase.Regular:
                    _regularListeners.Remove(listener);
                    break;

                case ListenPhase.After:
                    _afterListeners.Remove(listener);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(phase), phase, null);
            }
        }
    }
}

using System.Collections.Generic;

namespace Eflatun.EventBus
{
    public class EventBus<TEvent> where TEvent : IEvent
    {
        private readonly Dictionary<ListenPhase, PhaseContext<TEvent>> _phaseContexts =
            new Dictionary<ListenPhase, PhaseContext<TEvent>>
            {
                {ListenPhase.Before, new PhaseContext<TEvent>()},
                {ListenPhase.Regular, new PhaseContext<TEvent>()},
                {ListenPhase.After, new PhaseContext<TEvent>()}
            };

        public void Broadcast(object sender, TEvent @event)
        {
            _phaseContexts[ListenPhase.Before].Broadcast(sender, @event);
            _phaseContexts[ListenPhase.Regular].Broadcast(sender, @event);
            _phaseContexts[ListenPhase.After].Broadcast(sender, @event);
        }

        public void Emit(ISet<int> channels, object sender, TEvent @event)
        {
            _phaseContexts[ListenPhase.Before].Emit(channels, sender, @event);
            _phaseContexts[ListenPhase.Regular].Emit(channels, sender, @event);
            _phaseContexts[ListenPhase.After].Emit(channels, sender, @event);
        }

        public void Emit(int channel, object sender, TEvent @event)
        {
            _phaseContexts[ListenPhase.Before].Emit(channel, sender, @event);
            _phaseContexts[ListenPhase.Regular].Emit(channel, sender, @event);
            _phaseContexts[ListenPhase.After].Emit(channel, sender, @event);
        }

        public void AddListener(ListenerConfig config, EventHandler<TEvent> listener)
        {
            _phaseContexts[config.Phase].AddListener(config, listener);
        }

        public void RemoveListener(ListenerConfig config, EventHandler<TEvent> listener)
        {
            _phaseContexts[config.Phase].RemoveListener(config, listener);
        }
    }
}

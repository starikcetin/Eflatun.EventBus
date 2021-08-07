using System.Collections.Generic;

namespace Eflatun.EventBus
{
    public class PhaseContext<TEvent> where TEvent : IEvent
    {
        private readonly ISet<EventHandler<TEvent>> _broadcastListeners = new HashSet<EventHandler<TEvent>>();
        private readonly ISet<EventHandler<TEvent>> _allChannelsListeners = new HashSet<EventHandler<TEvent>>();
        private readonly IDictionary<int, ISet<EventHandler<TEvent>>> _channelListeners = new Dictionary<int, ISet<EventHandler<TEvent>>>();

        public void Broadcast(object sender, TEvent @event)
        {
            foreach (var listener in _broadcastListeners)
            {
                listener?.Invoke(sender, @event);
            }
        }

        public void Emit(ISet<int> channels, object sender, TEvent @event)
        {
            foreach (var listener in _allChannelsListeners)
            {
                listener?.Invoke(sender, @event);
            }

            foreach (var channel in channels)
            {
                EnsureChannelListenerList(channel);
                foreach (var listener in _channelListeners[channel])
                {
                    listener?.Invoke(sender, @event);
                }
            }
        }

        public void Emit(int channel, object sender, TEvent @event)
        {
            foreach (var listener in _allChannelsListeners)
            {
                listener?.Invoke(sender, @event);
            }

            EnsureChannelListenerList(channel);
            foreach (var listener in _channelListeners[channel])
            {
                listener?.Invoke(sender, @event);
            }
        }

        public void AddListener(ListenerConfig config, EventHandler<TEvent> listener)
        {
            if (config.ListeningToAllChannels)
            {
                _allChannelsListeners.Add(listener);
            }

            if (config.ReceivingBroadcasts)
            {
                _broadcastListeners.Add(listener);
            }

            foreach (var channel in config.SpecificChannels)
            {
                EnsureChannelListenerList(channel);
                _channelListeners[channel].Add(listener);
            }
        }

        public void RemoveListener(ListenerConfig config, EventHandler<TEvent> listener)
        {
            if (config.ListeningToAllChannels)
            {
                _allChannelsListeners.Remove(listener);
            }

            if (config.ReceivingBroadcasts)
            {
                _broadcastListeners.Remove(listener);
            }

            foreach (var channel in config.SpecificChannels)
            {
                EnsureChannelListenerList(channel);
                _channelListeners[channel].Remove(listener);
            }
        }

        private void EnsureChannelListenerList(int channel)
        {
            if (_channelListeners.ContainsKey(channel))
            {
                return;
            }

            _channelListeners.Add(channel, new HashSet<EventHandler<TEvent>>());
        }
    }
}

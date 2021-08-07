using System.Collections.Generic;

namespace Eflatun.EventBus
{
    public class PhaseContext<TEvent> where TEvent : IEvent
    {
        private readonly List<EventHandler<TEvent>> _broadcastListeners = new List<EventHandler<TEvent>>();
        private readonly List<EventHandler<TEvent>> _allChannelsListeners = new List<EventHandler<TEvent>>();
        private readonly Dictionary<int, List<EventHandler<TEvent>>> _channelListeners = new Dictionary<int, List<EventHandler<TEvent>>>();

        public void Broadcast(object sender, TEvent @event)
        {
            _broadcastListeners.ForEach(l => l?.Invoke(sender, @event));
        }

        public void Emit(IEnumerable<int> channels, object sender, TEvent @event)
        {
            _allChannelsListeners.ForEach(l => l?.Invoke(sender, @event));

            foreach (var channel in channels)
            {
                EnsureChannelListenerList(channel);
                _channelListeners[channel].ForEach(l => l?.Invoke(sender, @event));
            }
        }

        public void Emit(int channel, object sender, TEvent @event)
        {
            _allChannelsListeners.ForEach(l => l?.Invoke(sender, @event));

            EnsureChannelListenerList(channel);
            _channelListeners[channel].ForEach(l => l?.Invoke(sender, @event));
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

            _channelListeners.Add(channel, new List<EventHandler<TEvent>>());
        }
    }
}

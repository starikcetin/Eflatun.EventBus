using System.Collections.Generic;

namespace Eflatun.EventBus
{
    public class EventBus<TEvent> where TEvent : IEvent
    {
        private static readonly int AllChannelsChannel = -1;

        private readonly ChannelContext<TEvent> _broadcastContext = new ChannelContext<TEvent>();
        private readonly Dictionary<int, ChannelContext<TEvent>> _channelContexts = new Dictionary<int, ChannelContext<TEvent>>();

        public void Broadcast(object sender, TEvent @event)
        {
            _broadcastContext.Send(sender, @event);
        }

        public void Emit(IEnumerable<int> channels, object sender, TEvent @event)
        {
            _channelContexts[AllChannelsChannel].Send(sender, @event);

            foreach (var channel in channels)
            {
                EnsureChannelContext(channel);
                _channelContexts[channel].Send(sender, @event);
            }
        }

        public void AddListener(ListenerConfig config, EventHandler<TEvent> listener)
        {
            if (config.ReceiveBroadcasts)
            {
                _broadcastContext.AddListener(config.Phase, listener);
            }

            foreach (var channel in config.Channels)
            {
                EnsureChannelContext(channel);
                _channelContexts[channel].AddListener(config.Phase, listener);
            }
        }

        public void RemoveListener(ListenerConfig config, EventHandler<TEvent> listener)
        {
            if (config.ReceiveBroadcasts)
            {
                _broadcastContext.RemoveListener(config.Phase, listener);
            }

            foreach (var channel in config.Channels)
            {
                EnsureChannelContext(channel);
                _channelContexts[channel].RemoveListener(config.Phase, listener);
            }
        }

        private void EnsureChannelContext(int channel)
        {
            if (_channelContexts.ContainsKey(channel))
            {
                return;
            }

            _channelContexts.Add(channel, new ChannelContext<TEvent>());
        }
    }
}

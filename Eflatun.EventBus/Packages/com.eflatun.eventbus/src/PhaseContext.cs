﻿using System.Collections.Generic;

namespace Eflatun.EventBus
{
    public class PhaseContext<TEvent> where TEvent : IEvent
    {
        private readonly ISet<EventHandler<TEvent>> _combineSet = new HashSet<EventHandler<TEvent>>();

        private readonly ISet<EventHandler<TEvent>> _broadcastListeners = new HashSet<EventHandler<TEvent>>();
        private readonly ISet<EventHandler<TEvent>> _allChannelsListeners = new HashSet<EventHandler<TEvent>>();

        private readonly IDictionary<int, ISet<EventHandler<TEvent>>> _channelListeners =
            new Dictionary<int, ISet<EventHandler<TEvent>>>();

        public void Broadcast(object sender, TEvent @event)
        {
            CombineAndSend(sender, @event, Utils.EmptyIntSet, false, true);
        }

        public void Emit(ISet<int> channels, object sender, TEvent @event)
        {
            CombineAndSend(sender, @event, channels, true, false);
        }

        public void EmitAndBroadcast(ISet<int> channels, object sender, TEvent @event)
        {
            CombineAndSend(sender, @event, channels, true, true);
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
            if (!_channelListeners.ContainsKey(channel))
            {
                _channelListeners.Add(channel, new HashSet<EventHandler<TEvent>>());
            }
        }

        private void CombineAndSend(object sender, TEvent @event, ISet<int> channels, bool includeAllChannels, bool includeBroadcast)
        {
            var metadata = new EventMetadata(channels, sender, false);

            // collect all relevant listeners into combineSet
            if (includeAllChannels)
            {
                _combineSet.UnionWith(_allChannelsListeners);
            }

            if (includeBroadcast)
            {
                _combineSet.UnionWith(_broadcastListeners);
            }

            foreach (var channel in channels)
            {
                EnsureChannelListenerList(channel);
                _combineSet.UnionWith(_channelListeners[channel]);
            }

            // invoke the listeners in combineSet
            foreach (var listener in _combineSet)
            {
                listener.Invoke(metadata, @event);
            }

            // clear combineSet
            _combineSet.Clear();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using Eflatun.EventBus.Internal;

namespace Eflatun.EventBus
{
    public class PhaseContext<TEvent> where TEvent : IEvent
    {
        private readonly HashSetList<EventHandler<TEvent>> _combineSet = new HashSetList<EventHandler<TEvent>>();

        private readonly HashSetList<EventHandler<TEvent>> _broadcastListeners = new HashSetList<EventHandler<TEvent>>();
        private readonly HashSetList<EventHandler<TEvent>> _allChannelsListeners = new HashSetList<EventHandler<TEvent>>();

        private readonly Dictionary<int, HashSetList<EventHandler<TEvent>>> _channelListeners =
            new Dictionary<int, HashSetList<EventHandler<TEvent>>>();

        public void Broadcast(object sender, TEvent @event)
        {
            CombineAndSend(sender, @event, Array.Empty<int>(), false, true);
        }

        public void Emit(Span<int> channels, object sender, TEvent @event)
        {
            CombineAndSend(sender, @event, channels, true, false);
        }

        public void EmitAndBroadcast(Span<int> channels, object sender, TEvent @event)
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
                _channelListeners.Add(channel, new HashSetList<EventHandler<TEvent>>());
            }
        }

        private void CombineAndSend(object sender, TEvent @event, Span<int> channels, bool includeAllChannels, bool includeBroadcast)
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

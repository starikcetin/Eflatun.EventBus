using System;
using System.Collections.Generic;
using Eflatun.EventBus.Internal;

namespace Eflatun.EventBus
{
    internal class PhaseContext<TEvent> where TEvent : IEvent
    {
        private readonly HashSetList<HashCachedListener<TEvent>> _combineSet = new HashSetList<HashCachedListener<TEvent>>();

        private readonly HashSetList<HashCachedListener<TEvent>> _broadcastListeners = new HashSetList<HashCachedListener<TEvent>>();
        private readonly HashSetList<HashCachedListener<TEvent>> _allChannelsListeners = new HashSetList<HashCachedListener<TEvent>>();

        private readonly Dictionary<int, HashSetList<HashCachedListener<TEvent>>> _channelListeners =
            new Dictionary<int, HashSetList<HashCachedListener<TEvent>>>();

        public void Broadcast(object sender, TEvent @event)
        {
            var metadata = new EventMetadata(stackalloc int[0], sender, false);

            foreach (var listener in _broadcastListeners)
            {
                listener.Listener.Invoke(metadata, @event);
            }
        }

        public void Emit(int channel, object sender, TEvent @event)
        {
            var metadata = new EventMetadata(stackalloc[] {channel}, sender, false);

            _combineSet.UnionWith(_allChannelsListeners);
            EnsureChannelListenerList(channel);
            _combineSet.UnionWith(_channelListeners[channel]);

            foreach (var listener in _combineSet)
            {
                listener.Listener.Invoke(metadata, @event);
            }

            _combineSet.Clear();
        }

        public void Emit(ReadOnlySpan<int> channels, object sender, TEvent @event)
        {
            var metadata = new EventMetadata(channels, sender, false);

            _combineSet.UnionWith(_allChannelsListeners);

            foreach (var channel in channels)
            {
                EnsureChannelListenerList(channel);
                _combineSet.UnionWith(_channelListeners[channel]);
            }

            foreach (var listener in _combineSet)
            {
                listener.Listener.Invoke(metadata, @event);
            }

            _combineSet.Clear();
        }

        public void EmitAndBroadcast(ReadOnlySpan<int> channels, object sender, TEvent @event)
        {
            var metadata = new EventMetadata(channels, sender, false);

            _combineSet.UnionWith(_allChannelsListeners);
            _combineSet.UnionWith(_broadcastListeners);

            foreach (var channel in channels)
            {
                EnsureChannelListenerList(channel);
                _combineSet.UnionWith(_channelListeners[channel]);
            }

            foreach (var listener in _combineSet)
            {
                listener.Listener.Invoke(metadata, @event);
            }

            _combineSet.Clear();
        }

        public void EmitAndBroadcast(int channel, object sender, TEvent @event)
        {
            var metadata = new EventMetadata(stackalloc[] {channel}, sender, false);

            _combineSet.UnionWith(_allChannelsListeners);
            _combineSet.UnionWith(_broadcastListeners);
            EnsureChannelListenerList(channel);
            _combineSet.UnionWith(_channelListeners[channel]);

            foreach (var listener in _combineSet)
            {
                listener.Listener.Invoke(metadata, @event);
            }

            _combineSet.Clear();
        }

        public void AddListener(ListenerConfig config, HashCachedListener<TEvent> listener)
        {
            if (config.IsListeningToAllChannels)
            {
                _allChannelsListeners.Add(listener);
            }

            if (config.IsReceivingBroadcasts)
            {
                _broadcastListeners.Add(listener);
            }

            foreach (var channel in config.SpecificChannels)
            {
                EnsureChannelListenerList(channel);
                _channelListeners[channel].Add(listener);
            }
        }

        public void RemoveListener(ListenerConfig config, HashCachedListener<TEvent> listener)
        {
            if (config.IsListeningToAllChannels)
            {
                _allChannelsListeners.Remove(listener);
            }

            if (config.IsReceivingBroadcasts)
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
                _channelListeners.Add(channel, new HashSetList<HashCachedListener<TEvent>>());
            }
        }
    }
}

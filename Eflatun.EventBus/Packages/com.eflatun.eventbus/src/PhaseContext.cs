using System;
using System.Collections.Generic;
using Eflatun.EventBus.Internal;

namespace Eflatun.EventBus
{
    internal class PhaseContext<TEvent> where TEvent : IEvent
    {
        private readonly HashSetList<HashCachedEventHandler<TEvent>> _combineSet = new HashSetList<HashCachedEventHandler<TEvent>>();

        private readonly HashSetList<HashCachedEventHandler<TEvent>> _broadcastListeners = new HashSetList<HashCachedEventHandler<TEvent>>();
        private readonly HashSetList<HashCachedEventHandler<TEvent>> _allChannelsListeners = new HashSetList<HashCachedEventHandler<TEvent>>();

        private readonly Dictionary<int, HashSetList<HashCachedEventHandler<TEvent>>> _channelListeners =
            new Dictionary<int, HashSetList<HashCachedEventHandler<TEvent>>>();

        public void Broadcast(object sender, TEvent @event)
        {
            var metadata = new EventMetadata(stackalloc int[0], sender, false);

            foreach (var listener in _broadcastListeners)
            {
                listener.EventHandler.Invoke(metadata, @event);
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
                listener.EventHandler.Invoke(metadata, @event);
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
                listener.EventHandler.Invoke(metadata, @event);
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
                listener.EventHandler.Invoke(metadata, @event);
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
                listener.EventHandler.Invoke(metadata, @event);
            }

            _combineSet.Clear();
        }

        public void AddListener(ListenerConfig config, HashCachedEventHandler<TEvent> listener)
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

        public void RemoveListener(ListenerConfig config, HashCachedEventHandler<TEvent> listener)
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
                _channelListeners.Add(channel, new HashSetList<HashCachedEventHandler<TEvent>>());
            }
        }
    }
}

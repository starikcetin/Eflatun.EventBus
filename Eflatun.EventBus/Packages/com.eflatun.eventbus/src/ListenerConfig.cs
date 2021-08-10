using System;
using System.Collections.Generic;

namespace Eflatun.EventBus
{
    public readonly struct ListenerConfig
    {
        /// Listen to all channels, and receive broadcasts.
        public static ListenerConfig AllChannelsAndBroadcast(ListenPhase phase) =>
            new ListenerConfig(true, true, phase);

        /// Listen to all channels, but do not receive broadcasts.
        public static ListenerConfig AllChannelsNoBroadcast(ListenPhase phase) =>
            new ListenerConfig(true, false, phase);

        /// Listen to multiple channels, and receive broadcasts.
        public static ListenerConfig MultipleChannelsAndBroadcast(int[] channels, ListenPhase phase) =>
            new ListenerConfig(channels, true, phase);

        /// Listen to multiple channels, but do not receive broadcasts.
        public static ListenerConfig MultipleChannelsNoBroadcast(int[] channels, ListenPhase phase) =>
            new ListenerConfig(channels, false, phase);

        /// Listen to a single channel, and receive broadcasts.
        public static ListenerConfig SingleChannelAndBroadcast(int channel, ListenPhase phase) =>
            new ListenerConfig(channel, true, phase);

        /// Listen to a single channel, but do not receive broadcasts.
        public static ListenerConfig SingleChannelNoBroadcast(int channel, ListenPhase phase) =>
            new ListenerConfig(channel, false, phase);

        /// Do not listen to any channels, but receive broadcasts.
        public static ListenerConfig BroadcastOnly(ListenPhase phase) =>
            new ListenerConfig(false, true, phase);

        public int[] SpecificChannels { get; }
        public bool IsListeningToAllChannels { get; }
        public bool IsReceivingBroadcasts { get; }
        public ListenPhase Phase { get; }

        public ListenerConfig(bool listenToAllChannels, bool receiveBroadcasts, ListenPhase phase)
        {
            SpecificChannels = Array.Empty<int>();
            IsListeningToAllChannels = listenToAllChannels;
            IsReceivingBroadcasts = receiveBroadcasts;
            Phase = phase;
        }

        public ListenerConfig(int[] channels, bool receiveBroadcasts, ListenPhase phase)
        {
            SpecificChannels = channels;
            IsListeningToAllChannels = false;
            IsReceivingBroadcasts = receiveBroadcasts;
            Phase = phase;
        }

        public ListenerConfig(int channel, bool receiveBroadcasts, ListenPhase phase)
        {
            SpecificChannels = new[] {channel};
            IsListeningToAllChannels = false;
            IsReceivingBroadcasts = receiveBroadcasts;
            Phase = phase;
        }
    }
}

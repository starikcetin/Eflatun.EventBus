using System.Collections.Generic;
using System.Linq;

namespace Eflatun.EventBus
{
    public class ListenerConfig
    {
        /// Listen to all channels, and receive broadcasts.
        public static ListenerConfig AllChannelsAndBroadcast(ListenPhase phase) =>
            new ListenerConfig(true, true, phase);

        /// Listen to all channels, but do not receive broadcasts.
        public static ListenerConfig AllChannelsNoBroadcast(ListenPhase phase) =>
            new ListenerConfig(true, false, phase);

        /// Listen to multiple channels, and receive broadcasts.
        public static ListenerConfig MultipleChannelsAndBroadcast(IEnumerable<int> channels, ListenPhase phase) =>
            new ListenerConfig(channels, true, phase);

        /// Listen to multiple channels, but do not receive broadcasts.
        public static ListenerConfig MultipleChannelsNoBroadcast(IEnumerable<int> channels, ListenPhase phase) =>
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

        public IEnumerable<int> SpecificChannels { get; }
        public bool ListeningToAllChannels { get; }
        public bool ReceivingBroadcasts { get; }
        public ListenPhase Phase { get; }

        public ListenerConfig(bool listenToAllChannels, bool receiveBroadcasts, ListenPhase phase)
        {
            SpecificChannels = Enumerable.Empty<int>();
            ListeningToAllChannels = listenToAllChannels;
            ReceivingBroadcasts = receiveBroadcasts;
            Phase = phase;
        }

        public ListenerConfig(IEnumerable<int> channels, bool receiveBroadcasts, ListenPhase phase)
        {
            SpecificChannels = channels;
            ListeningToAllChannels = false;
            ReceivingBroadcasts = receiveBroadcasts;
            Phase = phase;
        }

        public ListenerConfig(int channel, bool receiveBroadcasts, ListenPhase phase)
            : this(new[] {channel}, receiveBroadcasts, phase)
        {
        }
    }
}

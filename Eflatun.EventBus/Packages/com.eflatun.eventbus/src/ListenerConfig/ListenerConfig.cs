using System.Collections.Generic;

namespace Eflatun.EventBus
{
    public class ListenerConfig
    {
        /// Listen to all channels, and receive broadcasts.
        public static ListenerConfig AllChannelsAndBroadcast(ListenPhase phase) =>
            new ListenerConfig(EventBusConstants.AllChannelsChannelArray, true, phase);

        /// Listen to all channels, but do not receive broadcasts.
        public static ListenerConfig AllChannelsNoBroadcast(ListenPhase phase) =>
            new ListenerConfig(EventBusConstants.AllChannelsChannelArray, false, phase);

        /// Listen to specific channels, and receive broadcasts.
        public static ListenerConfig SpecificChannelsAndBroadcast(IEnumerable<int> channels, ListenPhase phase) =>
            new ListenerConfig(channels, true, phase);

        /// Listen to specific channels, but do not receive broadcasts.
        public static ListenerConfig SpecificChannelsNoBroadcast(IEnumerable<int> channels, ListenPhase phase) =>
            new ListenerConfig(channels, false, phase);

        /// Do not listen to any channels, but receive broadcasts.
        public static ListenerConfig BroadcastOnly(ListenPhase phase) =>
            new ListenerConfig(EventBusConstants.EmptyChannelArray, true, phase);

        public IEnumerable<int> Channels { get; }
        public bool ReceiveBroadcasts { get; }
        public ListenPhase Phase { get; }

        public ListenerConfig(IEnumerable<int> channels, bool receiveBroadcasts, ListenPhase phase)
        {
            Channels = channels;
            ReceiveBroadcasts = receiveBroadcasts;
            Phase = phase;
        }
    }
}

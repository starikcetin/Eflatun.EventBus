using System.Collections.Generic;

namespace Eflatun.EventBus
{
    public readonly struct ListenerConfig
    {
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

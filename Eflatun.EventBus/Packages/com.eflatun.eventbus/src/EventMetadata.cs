using System.Collections.Generic;

namespace Eflatun.EventBus
{
    public readonly struct EventMetadata
    {
        public object Sender { get; }
        public ISet<int> Channels { get; }
        public bool IsBroadcast { get; }

        public EventMetadata(ISet<int> channels, object sender, bool isBroadcast)
        {
            Sender = sender;
            IsBroadcast = isBroadcast;
            Channels = channels;
        }
    }
}

using System;
using System.Collections.Generic;

namespace Eflatun.EventBus
{
    public readonly ref struct EventMetadata
    {
        public object Sender { get; }
        public Span<int> Channels { get; }
        public bool IsBroadcast { get; }

        public EventMetadata(Span<int> channels, object sender, bool isBroadcast)
        {
            Sender = sender;
            IsBroadcast = isBroadcast;
            Channels = channels;
        }
    }
}

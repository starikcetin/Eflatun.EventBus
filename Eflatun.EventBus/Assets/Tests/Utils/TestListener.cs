using System.Linq;
using NUnit.Framework;

namespace Eflatun.EventBus.Dev.Tests.Utils
{
    internal class TestListener<TEvent> where TEvent : IEvent
    {
        private readonly ListenerConfig _config;
        private readonly Listener<TEvent> _listener;
        private readonly EventBus<TEvent> _eb;

        private bool _isInEventBus;

        public int EventCount { get; private set; }
        public int RegisteredCount { get; private set; }

        public TestListener(ListenerConfig config, EventBus<TEvent> eb)
        {
            _config = config;
            _eb = eb;
            _listener = (metadata, @event) => { EventCount++; };
        }

        public void AddToEventBus()
        {
            _eb.AddListener(_config, _listener);
            _isInEventBus = true;
        }

        public void RemoveFromEventBus()
        {
            _eb.RemoveListener(_config, _listener);
            _isInEventBus = false;
        }

        public void RegisterCount(int[] channels, bool broadcast)
        {
            if (!_isInEventBus)
            {
                return;
            }

            var shouldRegister = _config.IsReceivingBroadcasts && broadcast
                                 || _config.IsListeningToAllChannels && channels.Any()
                                 || _config.SpecificChannels.Intersect(channels).Any();
            if (shouldRegister)
            {
                RegisteredCount++;
            }
        }

        public void Check()
        {
            Assert.AreEqual(EventCount, RegisteredCount);
        }
    }
}

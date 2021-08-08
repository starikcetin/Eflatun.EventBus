using System.Collections.Generic;
using NUnit.Framework;

namespace Eflatun.EventBus.Dev.Tests.ListenerConfigTest
{
    public class ListenerConfigTest
    {
        [Test]
        public void ListenerConfigTestSimplePasses()
        {
            var eb = new EventBus<EventFoo>();

            var l1Count = 0;
            var l1Config = ListenerConfig.AllChannelsAndBroadcast(ListenPhase.Regular);
            EventHandler<EventFoo> l1 = (metadata, @event) => { l1Count++; };
            eb.AddListener(l1Config, l1);

            var l2Count = 0;
            var l2Config = ListenerConfig.BroadcastOnly(ListenPhase.Regular);
            EventHandler<EventFoo> l2 = (metadata, @event) => { l2Count++; };
            eb.AddListener(l2Config, l2);

            var l3Count = 0;
            var l3Config = ListenerConfig.AllChannelsNoBroadcast(ListenPhase.Regular);
            EventHandler<EventFoo> l3 = (metadata, @event) => { l3Count++; };
            eb.AddListener(l3Config, l3);

            var l4sCount = 0;
            var l4sConfig = ListenerConfig.SingleChannelAndBroadcast(0, ListenPhase.Regular);
            EventHandler<EventFoo> l4s = (metadata, @event) => { l4sCount++; };
            eb.AddListener(l4sConfig, l4s);

            var l4mCount = 0;
            var l4mConfig =
                ListenerConfig.MultipleChannelsAndBroadcast(new HashSet<int>(new[] {0}), ListenPhase.Regular);
            EventHandler<EventFoo> l4m = (metadata, @event) => { l4mCount++; };
            eb.AddListener(l4mConfig, l4m);

            var l5sCount = 0;
            var l5sConfig = ListenerConfig.SingleChannelNoBroadcast(0, ListenPhase.Regular);
            EventHandler<EventFoo> l5s = (metadata, @event) => { l5sCount++; };
            eb.AddListener(l5sConfig, l5s);

            var l5mCount = 0;
            var l5mConfig = ListenerConfig.MultipleChannelsNoBroadcast(new HashSet<int>(new[] {0}), ListenPhase.Regular);
            EventHandler<EventFoo> l5m = (metadata, @event) => { l5mCount++; };
            eb.AddListener(l5mConfig, l5m);

            // E1
            eb.Broadcast(null, new EventFoo());
            Assert.AreEqual(l1Count, 1);
            Assert.AreEqual(l2Count, 1);
            Assert.AreEqual(l3Count, 0);
            Assert.AreEqual(l4sCount, 1);
            Assert.AreEqual(l4mCount, 1);
            Assert.AreEqual(l5sCount, 0);
            Assert.AreEqual(l5mCount, 0);

            // E2
            eb.Emit(0, null, new EventFoo());
            Assert.AreEqual(l1Count, 2);
            Assert.AreEqual(l2Count, 1);
            Assert.AreEqual(l3Count, 1);
            Assert.AreEqual(l4sCount, 2);
            Assert.AreEqual(l4mCount, 2);
            Assert.AreEqual(l5sCount, 1);
            Assert.AreEqual(l5mCount, 1);
        }
    }
}

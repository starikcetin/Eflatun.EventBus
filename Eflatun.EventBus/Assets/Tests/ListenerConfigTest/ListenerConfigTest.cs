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

            var lABCount = 0;
            var lABConfig = ListenerConfig.AllChannelsAndBroadcast(ListenPhase.Regular);
            EventHandler<EventFoo> lAB = (metadata, @event) => { lABCount++; };
            eb.AddListener(lABConfig, lAB);

            var lBCount = 0;
            var lBConfig = ListenerConfig.BroadcastOnly(ListenPhase.Regular);
            EventHandler<EventFoo> lB = (metadata, @event) => { lBCount++; };
            eb.AddListener(lBConfig, lB);

            var lACount = 0;
            var lAConfig = ListenerConfig.AllChannelsNoBroadcast(ListenPhase.Regular);
            EventHandler<EventFoo> lA = (metadata, @event) => { lACount++; };
            eb.AddListener(lAConfig, lA);

            var l0BsCount = 0;
            var l0BsConfig = ListenerConfig.SingleChannelAndBroadcast(0, ListenPhase.Regular);
            EventHandler<EventFoo> l0Bs = (metadata, @event) => { l0BsCount++; };
            eb.AddListener(l0BsConfig, l0Bs);

            var l0BmCount = 0;
            var l0BmConfig =
                ListenerConfig.MultipleChannelsAndBroadcast(new HashSet<int>(new[] {0}), ListenPhase.Regular);
            EventHandler<EventFoo> l0Bm = (metadata, @event) => { l0BmCount++; };
            eb.AddListener(l0BmConfig, l0Bm);

            var l0sCount = 0;
            var l0sConfig = ListenerConfig.SingleChannelNoBroadcast(0, ListenPhase.Regular);
            EventHandler<EventFoo> l0s = (metadata, @event) => { l0sCount++; };
            eb.AddListener(l0sConfig, l0s);

            var l0mCount = 0;
            var l0mConfig = ListenerConfig.MultipleChannelsNoBroadcast(new HashSet<int>(new[] {0}), ListenPhase.Regular);
            EventHandler<EventFoo> l0m = (metadata, @event) => { l0mCount++; };
            eb.AddListener(l0mConfig, l0m);

            // EB
            eb.Broadcast(null, new EventFoo());
            Assert.AreEqual(lABCount, 1);
            Assert.AreEqual(lBCount, 1);
            Assert.AreEqual(lACount, 0);
            Assert.AreEqual(l0BsCount, 1);
            Assert.AreEqual(l0BmCount, 1);
            Assert.AreEqual(l0sCount, 0);
            Assert.AreEqual(l0mCount, 0);

            // E0
            eb.Emit(0, null, new EventFoo());
            Assert.AreEqual(lABCount, 2);
            Assert.AreEqual(lBCount, 1);
            Assert.AreEqual(lACount, 1);
            Assert.AreEqual(l0BsCount, 2);
            Assert.AreEqual(l0BmCount, 2);
            Assert.AreEqual(l0sCount, 1);
            Assert.AreEqual(l0mCount, 1);

            // E0B
            eb.EmitAndBroadcast(0, null, new EventFoo());
            Assert.AreEqual(lABCount, 3);
            Assert.AreEqual(lBCount, 2);
            Assert.AreEqual(lACount, 2);
            Assert.AreEqual(l0BsCount, 3);
            Assert.AreEqual(l0BmCount, 3);
            Assert.AreEqual(l0sCount, 2);
            Assert.AreEqual(l0mCount, 2);
        }
    }
}

using System.Collections.Generic;
using NUnit.Framework;

namespace Eflatun.EventBus.Dev.Tests.ChannelsTest
{
    public class ChannelsTest
    {
        [Test]
        public void ChannelsTestSimplePasses()
        {
            var eb = new EventBus<EventFoo>();

            var l0Count = 0;
            var l0Config = ListenerConfig.SingleChannelNoBroadcast(0, ListenPhase.Regular);
            EventHandler<EventFoo> l0 = (metadata, @event) => { l0Count++; };
            eb.AddListener(l0Config, l0);

            var l0BCount = 0;
            var l0BConfig = ListenerConfig.SingleChannelAndBroadcast(0, ListenPhase.Regular);
            EventHandler<EventFoo> l0B = (metadata, @event) => { l0BCount++; };
            eb.AddListener(l0BConfig, l0B);

            var l1Count = 0;
            var l1Config = ListenerConfig.SingleChannelNoBroadcast(1, ListenPhase.Regular);
            EventHandler<EventFoo> l1 = (metadata, @event) => { l1Count++; };
            eb.AddListener(l1Config, l1);

            var l1BCount = 0;
            var l1BConfig = ListenerConfig.SingleChannelAndBroadcast(1, ListenPhase.Regular);
            EventHandler<EventFoo> l1B = (metadata, @event) => { l1BCount++; };
            eb.AddListener(l1BConfig, l1B);

            var l2Count = 0;
            var l2Config = ListenerConfig.SingleChannelNoBroadcast(2, ListenPhase.Regular);
            EventHandler<EventFoo> l2 = (metadata, @event) => { l2Count++; };
            eb.AddListener(l2Config, l2);

            var l2BCount = 0;
            var l2BConfig = ListenerConfig.SingleChannelAndBroadcast(2, ListenPhase.Regular);
            EventHandler<EventFoo> l2B = (metadata, @event) => { l2BCount++; };
            eb.AddListener(l2BConfig, l2B);

            var l01Count = 0;
            var l01Config = ListenerConfig.MultipleChannelsNoBroadcast(new HashSet<int>(new [] {0, 1}), ListenPhase.Regular);
            EventHandler<EventFoo> l01 = (metadata, @event) => { l01Count++; };
            eb.AddListener(l01Config, l01);

            var l01BCount = 0;
            var l01BConfig = ListenerConfig.MultipleChannelsAndBroadcast(new HashSet<int>(new [] {0, 1}), ListenPhase.Regular);
            EventHandler<EventFoo> l01B = (metadata, @event) => { l01BCount++; };
            eb.AddListener(l01BConfig, l01B);

            var l02Count = 0;
            var l02Config = ListenerConfig.MultipleChannelsNoBroadcast(new HashSet<int>(new [] {0, 2}), ListenPhase.Regular);
            EventHandler<EventFoo> l02 = (metadata, @event) => { l02Count++; };
            eb.AddListener(l02Config, l02);

            var l02BCount = 0;
            var l02BConfig = ListenerConfig.MultipleChannelsAndBroadcast(new HashSet<int>(new [] {0, 2}), ListenPhase.Regular);
            EventHandler<EventFoo> l02B = (metadata, @event) => { l02BCount++; };
            eb.AddListener(l02BConfig, l02B);

            var l12Count = 0;
            var l12Config = ListenerConfig.MultipleChannelsNoBroadcast(new HashSet<int>(new [] {1, 2}), ListenPhase.Regular);
            EventHandler<EventFoo> l12 = (metadata, @event) => { l12Count++; };
            eb.AddListener(l12Config, l12);

            var l12BCount = 0;
            var l12BConfig = ListenerConfig.MultipleChannelsAndBroadcast(new HashSet<int>(new [] {1, 2}), ListenPhase.Regular);
            EventHandler<EventFoo> l12B = (metadata, @event) => { l12BCount++; };
            eb.AddListener(l12BConfig, l12B);

            var lACount = 0;
            var lAConfig = ListenerConfig.AllChannelsNoBroadcast(ListenPhase.Regular);
            EventHandler<EventFoo> lA = (metadata, @event) => { lACount++; };
            eb.AddListener(lAConfig, lA);

            var lABCount = 0;
            var lABConfig = ListenerConfig.AllChannelsAndBroadcast(ListenPhase.Regular);
            EventHandler<EventFoo> lAB = (metadata, @event) => { lABCount++; };
            eb.AddListener(lABConfig, lAB);

            var lBCount = 0;
            var lBConfig = ListenerConfig.BroadcastOnly(ListenPhase.Regular);
            EventHandler<EventFoo> lB = (metadata, @event) => { lBCount++; };
            eb.AddListener(lBConfig, lB);

            // E0
            eb.Emit(0, null, new EventFoo());
            Assert.AreEqual(l0Count, 1);
            Assert.AreEqual(l0BCount, 1);
            Assert.AreEqual(l1Count, 0);
            Assert.AreEqual(l1BCount, 0);
            Assert.AreEqual(l2Count, 0);
            Assert.AreEqual(l2BCount, 0);
            Assert.AreEqual(l01Count, 1);
            Assert.AreEqual(l01BCount, 1);
            Assert.AreEqual(l02Count, 1);
            Assert.AreEqual(l02BCount, 1);
            Assert.AreEqual(l12Count, 0);
            Assert.AreEqual(l12BCount, 0);
            Assert.AreEqual(lACount, 1);
            Assert.AreEqual(lABCount, 1);
            Assert.AreEqual(lBCount, 0);

            // E1
            eb.Emit(1, null, new EventFoo());
            Assert.AreEqual(l0Count, 1);
            Assert.AreEqual(l0BCount, 1);
            Assert.AreEqual(l1Count, 1);
            Assert.AreEqual(l1BCount, 1);
            Assert.AreEqual(l2Count, 0);
            Assert.AreEqual(l2BCount, 0);
            Assert.AreEqual(l01Count, 2);
            Assert.AreEqual(l01BCount, 2);
            Assert.AreEqual(l02Count, 1);
            Assert.AreEqual(l02BCount, 1);
            Assert.AreEqual(l12Count, 1);
            Assert.AreEqual(l12BCount, 1);
            Assert.AreEqual(lACount, 2);
            Assert.AreEqual(lABCount, 2);
            Assert.AreEqual(lBCount, 0);

            // E2
            eb.Emit(2, null, new EventFoo());
            Assert.AreEqual(l0Count, 1);
            Assert.AreEqual(l0BCount, 1);
            Assert.AreEqual(l1Count, 1);
            Assert.AreEqual(l1BCount, 1);
            Assert.AreEqual(l2Count, 1);
            Assert.AreEqual(l2BCount, 1);
            Assert.AreEqual(l01Count, 2);
            Assert.AreEqual(l01BCount, 2);
            Assert.AreEqual(l02Count, 2);
            Assert.AreEqual(l02BCount, 2);
            Assert.AreEqual(l12Count, 2);
            Assert.AreEqual(l12BCount, 2);
            Assert.AreEqual(lACount, 3);
            Assert.AreEqual(lABCount, 3);
            Assert.AreEqual(lBCount, 0);

            // E01
            eb.Emit(new HashSet<int>(new [] {0, 1}), null, new EventFoo());
            Assert.AreEqual(l0Count, 2);
            Assert.AreEqual(l0BCount, 2);
            Assert.AreEqual(l1Count, 2);
            Assert.AreEqual(l1BCount, 2);
            Assert.AreEqual(l2Count, 1);
            Assert.AreEqual(l2BCount, 1);
            Assert.AreEqual(l01Count, 3);
            Assert.AreEqual(l01BCount, 3);
            Assert.AreEqual(l02Count, 3);
            Assert.AreEqual(l02BCount, 3);
            Assert.AreEqual(l12Count, 3);
            Assert.AreEqual(l12BCount, 3);
            Assert.AreEqual(lACount, 4);
            Assert.AreEqual(lABCount, 4);
            Assert.AreEqual(lBCount, 0);

            // E02
            eb.Emit(new HashSet<int>(new [] {0, 2}), null, new EventFoo());
            Assert.AreEqual(l0Count, 3);
            Assert.AreEqual(l0BCount, 3);
            Assert.AreEqual(l1Count, 2);
            Assert.AreEqual(l1BCount, 2);
            Assert.AreEqual(l2Count, 2);
            Assert.AreEqual(l2BCount, 2);
            Assert.AreEqual(l01Count, 4);
            Assert.AreEqual(l01BCount, 4);
            Assert.AreEqual(l02Count, 4);
            Assert.AreEqual(l02BCount, 4);
            Assert.AreEqual(l12Count, 4);
            Assert.AreEqual(l12BCount, 4);
            Assert.AreEqual(lACount, 5);
            Assert.AreEqual(lABCount, 5);
            Assert.AreEqual(lBCount, 0);

            // E12
            eb.Emit(new HashSet<int>(new [] {1, 2}), null, new EventFoo());
            Assert.AreEqual(l0Count, 3);
            Assert.AreEqual(l0BCount, 3);
            Assert.AreEqual(l1Count, 3);
            Assert.AreEqual(l1BCount, 3);
            Assert.AreEqual(l2Count, 3);
            Assert.AreEqual(l2BCount, 3);
            Assert.AreEqual(l01Count, 5);
            Assert.AreEqual(l01BCount, 5);
            Assert.AreEqual(l02Count, 5);
            Assert.AreEqual(l02BCount, 5);
            Assert.AreEqual(l12Count, 5);
            Assert.AreEqual(l12BCount, 5);
            Assert.AreEqual(lACount, 6);
            Assert.AreEqual(lABCount, 6);
            Assert.AreEqual(lBCount, 0);

            // EB
            eb.Broadcast(null, new EventFoo());
            Assert.AreEqual(l0Count, 3);
            Assert.AreEqual(l0BCount, 4);
            Assert.AreEqual(l1Count, 3);
            Assert.AreEqual(l1BCount, 4);
            Assert.AreEqual(l2Count, 3);
            Assert.AreEqual(l2BCount, 4);
            Assert.AreEqual(l01Count, 5);
            Assert.AreEqual(l01BCount, 6);
            Assert.AreEqual(l02Count, 5);
            Assert.AreEqual(l02BCount, 6);
            Assert.AreEqual(l12Count, 5);
            Assert.AreEqual(l12BCount, 6);
            Assert.AreEqual(lACount, 6);
            Assert.AreEqual(lABCount, 7);
            Assert.AreEqual(lBCount, 1);
        }
    }
}

using Eflatun.EventBus.Dev.Tests.Utils;
using NUnit.Framework;

namespace Eflatun.EventBus.Dev.Tests.ListenerConfigTest
{
    public class ListenerConfigTest
    {
        [Test]
        public void ListenerConfigTestSimplePasses()
        {
            var ctx = new TestListenerContext<EventFoo>(new EventBus<EventFoo>());

            var lABConfig = ListenerConfig.AllChannelsAndBroadcast(ListenPhase.Regular);
            ctx.MakeListener_AddToEventBus(lABConfig);

            var lBConfig = ListenerConfig.BroadcastOnly(ListenPhase.Regular);
            ctx.MakeListener_AddToEventBus(lBConfig);

            var lAConfig = ListenerConfig.AllChannelsNoBroadcast(ListenPhase.Regular);
            ctx.MakeListener_AddToEventBus(lAConfig);

            var l0BsConfig = ListenerConfig.SingleChannelAndBroadcast(0, ListenPhase.Regular);
            ctx.MakeListener_AddToEventBus(l0BsConfig);

            var l0BmConfig = ListenerConfig.MultipleChannelsAndBroadcast(new[] {0}, ListenPhase.Regular);
            ctx.MakeListener_AddToEventBus(l0BmConfig);

            var l0sConfig = ListenerConfig.SingleChannelNoBroadcast(0, ListenPhase.Regular);
            ctx.MakeListener_AddToEventBus(l0sConfig);

            var l0mConfig = ListenerConfig.MultipleChannelsNoBroadcast(new[] {0}, ListenPhase.Regular);
            ctx.MakeListener_AddToEventBus(l0mConfig);

            ctx.Broadcast_Register_Check(null, new EventFoo());
            ctx.Emit_Register_Check(0, null, new EventFoo());
            ctx.Emit_Register_Check(stackalloc[] {0}, null, new EventFoo());
            ctx.EmitAndBroadcast_Register_Check(0, null, new EventFoo());
            ctx.EmitAndBroadcast_Register_Check(stackalloc[] {0}, null, new EventFoo());
        }
    }
}

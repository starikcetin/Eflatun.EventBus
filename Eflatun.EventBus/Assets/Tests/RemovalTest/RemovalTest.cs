using Eflatun.EventBus.Dev.Tests.Utils;
using NUnit.Framework;

namespace Eflatun.EventBus.Dev.Tests.RemovalTest
{
    public class RemovalTest
    {
        [Test]
        public void RemovalTestSimplePasses()
        {
            var ctx = new TestListenerContext<EventFoo>(new EventBus<EventFoo>());

            var l0Config = ListenerConfig.SingleChannelNoBroadcast(0, ListenPhase.Regular);
            ctx.MakeListener_AddToEventBus(l0Config);

            var l0BConfig = ListenerConfig.SingleChannelAndBroadcast(0, ListenPhase.Regular);
            ctx.MakeListener_AddToEventBus(l0BConfig);

            var l1Config = ListenerConfig.SingleChannelNoBroadcast(1, ListenPhase.Regular);
            ctx.MakeListener_AddToEventBus(l1Config);

            var l1BConfig = ListenerConfig.SingleChannelAndBroadcast(1, ListenPhase.Regular);
            ctx.MakeListener_AddToEventBus(l1BConfig);

            var l2Config = ListenerConfig.SingleChannelNoBroadcast(2, ListenPhase.Regular);
            ctx.MakeListener_AddToEventBus(l2Config);

            var l2BConfig = ListenerConfig.SingleChannelAndBroadcast(2, ListenPhase.Regular);
            ctx.MakeListener_AddToEventBus(l2BConfig);

            var l01Config = ListenerConfig.MultipleChannelsNoBroadcast(new[] {0, 1}, ListenPhase.Regular);
            ctx.MakeListener_AddToEventBus(l01Config);

            var l01BConfig = ListenerConfig.MultipleChannelsAndBroadcast(new[] {0, 1}, ListenPhase.Regular);
            ctx.MakeListener_AddToEventBus(l01BConfig);

            var l02Config = ListenerConfig.MultipleChannelsNoBroadcast(new[] {0, 2}, ListenPhase.Regular);
            ctx.MakeListener_AddToEventBus(l02Config);

            var l02BConfig = ListenerConfig.MultipleChannelsAndBroadcast(new[] {0, 2}, ListenPhase.Regular);
            ctx.MakeListener_AddToEventBus(l02BConfig);

            var l12Config = ListenerConfig.MultipleChannelsNoBroadcast(new[] {1, 2}, ListenPhase.Regular);
            ctx.MakeListener_AddToEventBus(l12Config);

            var l12BConfig = ListenerConfig.MultipleChannelsAndBroadcast(new[] {1, 2}, ListenPhase.Regular);
            ctx.MakeListener_AddToEventBus(l12BConfig);

            var lAConfig = ListenerConfig.AllChannelsNoBroadcast(ListenPhase.Regular);
            ctx.MakeListener_AddToEventBus(lAConfig);

            var lABConfig = ListenerConfig.AllChannelsAndBroadcast(ListenPhase.Regular);
            ctx.MakeListener_AddToEventBus(lABConfig);

            var lBConfig = ListenerConfig.BroadcastOnly(ListenPhase.Regular);
            ctx.MakeListener_AddToEventBus(lBConfig);

            TestStep(ctx);

            ctx.GetListener(l0Config).RemoveFromEventBus();
            TestStep(ctx);

            ctx.GetListener(l1Config).RemoveFromEventBus();
            TestStep(ctx);

            ctx.GetListener(l2Config).RemoveFromEventBus();
            TestStep(ctx);

            ctx.GetListener(l01Config).RemoveFromEventBus();
            TestStep(ctx);

            ctx.GetListener(l02Config).RemoveFromEventBus();
            TestStep(ctx);

            ctx.GetListener(l12Config).RemoveFromEventBus();
            TestStep(ctx);

            ctx.GetListener(l0BConfig).RemoveFromEventBus();
            TestStep(ctx);

            ctx.GetListener(l1BConfig).RemoveFromEventBus();
            TestStep(ctx);

            ctx.GetListener(l2BConfig).RemoveFromEventBus();
            TestStep(ctx);

            ctx.GetListener(l01BConfig).RemoveFromEventBus();
            TestStep(ctx);

            ctx.GetListener(l02BConfig).RemoveFromEventBus();
            TestStep(ctx);

            ctx.GetListener(l12BConfig).RemoveFromEventBus();
            TestStep(ctx);

            ctx.GetListener(lBConfig).RemoveFromEventBus();
            TestStep(ctx);

            ctx.GetListener(lAConfig).RemoveFromEventBus();
            TestStep(ctx);

            ctx.GetListener(lABConfig).RemoveFromEventBus();
            TestStep(ctx);
        }

        private static void TestStep(TestListenerContext<EventFoo> ctx)
        {
            ctx.Emit_Register_Check(0, null, new EventFoo());
            ctx.Emit_Register_Check(1, null, new EventFoo());
            ctx.Emit_Register_Check(2, null, new EventFoo());
            ctx.Emit_Register_Check(stackalloc[] {0, 1}, null, new EventFoo());
            ctx.Emit_Register_Check(stackalloc[] {0, 2}, null, new EventFoo());
            ctx.Emit_Register_Check(stackalloc[] {1, 2}, null, new EventFoo());
            ctx.Broadcast_Register_Check(null, new EventFoo());
            ctx.EmitAndBroadcast_Register_Check(0, null, new EventFoo());
            ctx.EmitAndBroadcast_Register_Check(1, null, new EventFoo());
            ctx.EmitAndBroadcast_Register_Check(2, null, new EventFoo());
            ctx.EmitAndBroadcast_Register_Check(stackalloc[] {0, 1}, null, new EventFoo());
            ctx.EmitAndBroadcast_Register_Check(stackalloc[] {0, 2}, null, new EventFoo());
            ctx.EmitAndBroadcast_Register_Check(stackalloc[] {1, 2}, null, new EventFoo());
            ctx.Emit_Register_Check(stackalloc[] {0, 1, 2}, null, new EventFoo());
            ctx.EmitAndBroadcast_Register_Check(stackalloc[] {0, 1, 2}, null, new EventFoo());
        }
    }
}
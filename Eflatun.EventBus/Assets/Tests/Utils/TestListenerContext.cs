using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Eflatun.EventBus.Dev.Tests.Utils
{
    internal class TestListenerContext<TEvent> where TEvent : IEvent
    {
        private readonly Dictionary<ListenerConfig, TestListener<TEvent>> _listeners = new Dictionary<ListenerConfig, TestListener<TEvent>>();
        private readonly EventBus<TEvent> _eb;

        public TestListenerContext(EventBus<TEvent> eb)
        {
            _eb = eb;
        }
        
        public TestListener<TEvent> GetListener(ListenerConfig config)
        {
            return _listeners[config];
        }

        public void MakeListener(ListenerConfig config)
        {
            Assert.IsFalse(_listeners.ContainsKey(config), $"{nameof(TestListenerContext<TEvent>)} already has this config!\n{config}");
            _listeners.Add(config, new TestListener<TEvent>(config, _eb));
        }

        public void RegisterCount(int[] channels, bool broadcast)
        {
            foreach (var listener in _listeners.Values)
            {
                listener.RegisterCount(channels, broadcast);
            }
        }

        public void Check()
        {
            foreach (var listener in _listeners.Values)
            {
                listener.Check();
            }
        }
        
        public void MakeListener_AddToEventBus(ListenerConfig config)
        {
            MakeListener(config);
            GetListener(config).AddToEventBus();
        }

        public void Emit_Register_Check(ReadOnlySpan<int> channels, object sender, TEvent @event)
        {
            _eb.Emit(channels, sender, @event);
            RegisterCount(channels.ToArray(), false);
        }

        public void Emit_Register_Check(int channel, object sender, TEvent @event)
        {
            _eb.Emit(channel, sender, @event);
            RegisterCount(new []{channel}, false);
        }

        public void EmitAndBroadcast_Register_Check(ReadOnlySpan<int> channels, object sender, TEvent @event)
        {
            _eb.EmitAndBroadcast(channels, sender, @event);
            RegisterCount(channels.ToArray(), true);
        }

        public void EmitAndBroadcast_Register_Check(int channel, object sender, TEvent @event)
        {
            _eb.EmitAndBroadcast(channel, sender, @event);
            RegisterCount(new []{channel}, true);
        }

        public void Broadcast_Register_Check(object sender, TEvent @event)
        {
            _eb.Broadcast(sender, @event);
            RegisterCount(new int[0], true);
        }
    }
}

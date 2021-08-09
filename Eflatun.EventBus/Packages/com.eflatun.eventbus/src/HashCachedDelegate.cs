using System;

namespace Eflatun.EventBus
{
    internal readonly struct HashCachedEventHandler<TEvent> : IEquatable<HashCachedEventHandler<TEvent>> where TEvent : IEvent
    {
        private readonly int _eventHandlerHashCode;
        public readonly EventHandler<TEvent> EventHandler;

        public HashCachedEventHandler(EventHandler<TEvent> eventHandler)
        {
            _eventHandlerHashCode = eventHandler?.GetHashCode() ?? 0;
            EventHandler = eventHandler;
        }

        public bool Equals(HashCachedEventHandler<TEvent> other)
        {
            return Equals(EventHandler, other.EventHandler);
        }

        public override bool Equals(object obj)
        {
            return obj is HashCachedEventHandler<TEvent> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _eventHandlerHashCode;
        }

        public static bool operator ==(HashCachedEventHandler<TEvent> left, HashCachedEventHandler<TEvent> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HashCachedEventHandler<TEvent> left, HashCachedEventHandler<TEvent> right)
        {
            return !left.Equals(right);
        }
    }
}

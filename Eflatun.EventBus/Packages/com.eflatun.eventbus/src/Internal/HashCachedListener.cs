using System;

namespace Eflatun.EventBus
{
    internal readonly struct HashCachedListener<TEvent> : IEquatable<HashCachedListener<TEvent>> where TEvent : IEvent
    {
        private readonly int _eventHandlerHashCode;
        public readonly Listener<TEvent> Listener;

        public HashCachedListener(Listener<TEvent> listener)
        {
            _eventHandlerHashCode = listener?.GetHashCode() ?? 0;
            Listener = listener;
        }

        public bool Equals(HashCachedListener<TEvent> other)
        {
            return Equals(Listener, other.Listener);
        }

        public override bool Equals(object obj)
        {
            return obj is HashCachedListener<TEvent> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _eventHandlerHashCode;
        }

        public static bool operator ==(HashCachedListener<TEvent> left, HashCachedListener<TEvent> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HashCachedListener<TEvent> left, HashCachedListener<TEvent> right)
        {
            return !left.Equals(right);
        }
    }
}

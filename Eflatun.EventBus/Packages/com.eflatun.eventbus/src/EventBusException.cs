using System;

namespace Eflatun.EventBus
{
    public class EventBusException : Exception
    {
        public EventBusException()
        {
        }

        public EventBusException(string message) : base(message)
        {
        }

        public EventBusException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

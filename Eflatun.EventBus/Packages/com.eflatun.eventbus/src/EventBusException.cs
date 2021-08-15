using System;

namespace Eflatun.EventBus
{
    public class EventBusException : Exception
    {
        internal EventBusException()
        {
        }

        internal EventBusException(string message) : base(message)
        {
        }

        internal EventBusException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

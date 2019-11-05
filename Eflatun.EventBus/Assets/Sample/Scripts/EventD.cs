using Eflatun.EventBus.interfaces;
using UnityEngine;

namespace Eflatun.EventBus.Sample
{
    public struct EventD : IEvent<EventD.Args>
    {
        public Args Arguments { get; }

        public EventD(Args arguments)
        {
            Arguments = arguments;
        }

        public struct Args : IEventArguments
        {
            public string Message { get; }
            
            public Args(string message)
            {
                Message = message;
            }
        }
    }
}

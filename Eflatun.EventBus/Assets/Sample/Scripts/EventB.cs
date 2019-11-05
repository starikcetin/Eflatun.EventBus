using Eflatun.EventBus.interfaces;

namespace Eflatun.EventBus.Sample
{
    public struct EventB : IEvent<EventB.Args>
    {
        public Args Arguments { get; }

        public EventB(Args arguments)
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

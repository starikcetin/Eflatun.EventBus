using Eflatun.EventBus;

namespace sample
{
    public struct EventA : IEvent<EventA.Args>
    {
        public Args Arguments { get; }

        public EventA(Args arguments)
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

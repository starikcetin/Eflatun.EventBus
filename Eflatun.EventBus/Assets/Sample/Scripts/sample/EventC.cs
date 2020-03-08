using Eflatun.EventBus;

namespace sample
{
    public struct EventC : IEvent<EventC.Args>
    {
        public Args Arguments { get; }

        public EventC(Args arguments)
        {
            Arguments = arguments;
        }

        public struct Args : IEventArguments
        {
            public int Tick { get; }

            public Args(int tick)
            {
                Tick = tick;
            }
        }
    }
}

using Eflatun.EventBus;

namespace beforeAfterSample
{
    public readonly struct SampleEvent : IEvent<SampleEvent.Args>
    {
        public Args Arguments { get; }

        public SampleEvent(Args arguments)
        {
            Arguments = arguments;
        }

        public readonly struct Args : IEventArguments
        {
        }
    }
}


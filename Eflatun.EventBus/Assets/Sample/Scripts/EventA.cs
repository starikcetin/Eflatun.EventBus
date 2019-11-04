namespace Eflatun.EventBus.Sample
{
    public class EventA : Event<EventA>
    {
        public EventA(IEventEmitter<EventA> sender, Args args) : base(sender, args)
        {
        }

        public class Args : EventArguments<EventA>
        {
            public string Message { get; }

            public Args(string message)
            {
                Message = message;
            }
        }
    }
}

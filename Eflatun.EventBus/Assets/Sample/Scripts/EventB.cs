namespace Eflatun.EventBus.Sample
{
    public class EventB : Event<EventB>
    {
        public EventB(IEventEmitter<EventB> sender, Args args) : base(sender, args)
        {
        }

        public class Args : EventArguments<EventB>
        {
            public string Message { get; }

            public Args(string message)
            {
                Message = message;
            }
        }
    }
}

namespace Eflatun.EventBus.Sample
{
    public class EventC : Event<EventC>
    {
        public EventC(IEventEmitter<EventC> sender, Args arguments) : base(sender, arguments)
        {
        }

        public class Args : EventArguments<EventC>
        {
            public int Tick { get; }

            public Args(int tick)
            {
                Tick = tick;
            }
        }
    }
}
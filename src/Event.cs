using Eflatun.EventBus.utils;

namespace Eflatun.EventBus
{
    public abstract class Event<TEvent>
        where TEvent : Event<TEvent>
    {
        public IEventEmitter<TEvent> Sender { get; }
        public EventArguments<TEvent> Arguments { get; }

        protected Event(IEventEmitter<TEvent> sender, EventArguments<TEvent> arguments)
        {
            Sender = sender;
            Arguments = arguments;
        }

        public override string ToString()
        {
            return this.ToObjectSummaryString();
        }
    }
}

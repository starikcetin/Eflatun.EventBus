using Eflatun.EventBus.utils;

namespace Eflatun.EventBus
{
    public abstract class EventArguments<TEvent>
        where TEvent : Event<TEvent>
    {
        public override string ToString()
        {
            return this.ToObjectSummaryString();
        }
    }
}

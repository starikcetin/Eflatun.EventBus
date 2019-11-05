using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Sample
{
    public class ListenerC : IInitializable, IEventListener<EventC, EventC.Args>
    {
        private readonly EventBus<EventC, EventC.Args> _eventBus;

        public ListenerC(EventBus<EventC, EventC.Args> eventBus)
        {
            _eventBus = eventBus;
        }

        public void Initialize()
        {
            _eventBus.Listen(this);
        }

        public void OnEvent(IEventEmitter<EventC, EventC.Args> sender, EventC @event)
        {
            var args = @event.Arguments;
            var tick = args.Tick;
            Debug.Log($"{nameof(ListenerC)} received {@event} from {sender}: Tick={tick}");
        }
    }
}

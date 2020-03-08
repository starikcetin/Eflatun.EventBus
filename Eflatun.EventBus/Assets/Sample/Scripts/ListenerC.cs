using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Sample
{
    public class ListenerC : IInitializable
    {
        private readonly EventBus<EventC> _eventBus;

        public ListenerC(EventBus<EventC> eventBus)
        {
            _eventBus = eventBus;
        }

        public void Initialize()
        {
            _eventBus.Listen(OnEvent);
        }

        private void OnEvent(object sender, EventC @event)
        {
            var args = @event.Arguments;
            var tick = args.Tick;
            Debug.Log($"{nameof(ListenerC)} received {@event} from {sender}: Tick={tick}");
        }
    }
}

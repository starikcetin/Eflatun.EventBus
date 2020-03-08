using Eflatun.EventBus;
using UnityEngine;
using Zenject;

namespace sample
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
            _eventBus.AddListener(OnEvent);
        }

        private void OnEvent(object sender, EventC @event)
        {
            var args = @event.Arguments;
            var tick = args.Tick;
            Debug.Log($"{nameof(ListenerC)} received {@event} from {sender}: Tick={tick}");
        }
    }
}

using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Sample
{
    public class ListenerC : IInitializable, IEventListener<EventC>
    {
        private readonly EventBus<EventC> _eventBus;

        public ListenerC(EventBus<EventC> eventBus)
        {
            _eventBus = eventBus;
        }

        public void Initialize()
        {
            _eventBus.Listen(this);
        }

        public void OnEvent(EventC @event)
        {
            Debug.Log($"{nameof(ListenerC)} received: " + @event);
        }
    }
}

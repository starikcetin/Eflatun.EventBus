using Eflatun.EventBus.interfaces;
using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Sample
{
    public class ListenerE : IInitializable, IEventListener<EventE>
    {
        private readonly EventBus<EventE> _eventBus;

        public ListenerE(EventBus<EventE> eventBus)
        {
            _eventBus = eventBus;
        }

        public void Initialize()
        {
            _eventBus.Listen(this);
        }

        public void OnEvent(object sender, EventE @event)
        {
            Debug.Log($"{nameof(ListenerE)} received {@event} from {sender}");
        }
    }
}
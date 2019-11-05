using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Sample
{
    public class ListenerB : MonoBehaviour, IEventListener<EventB, EventB.Args>
    {
        private EventBus<EventB, EventB.Args> _eventBus;

        [Inject]
        public void Init(EventBus<EventB, EventB.Args> eventBus)
        {
            _eventBus = eventBus;
        }

        public void Start()
        {
            _eventBus.Listen(this);
        }

        public void OnEvent(IEventEmitter<EventB, EventB.Args> sender, EventB @event)
        {
            Debug.Log($"{nameof(ListenerB)} on {gameObject.name} received {@event} from {sender}");
        }
    }
}

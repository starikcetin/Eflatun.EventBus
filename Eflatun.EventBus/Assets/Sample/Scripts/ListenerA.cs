using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Sample
{
    public class ListenerA : MonoBehaviour, IEventListener<EventA, EventA.Args>
    {
        private EventBus<EventA, EventA.Args> _eventBus;

        [Inject]
        public void Init(EventBus<EventA, EventA.Args> eventBus)
        {
            _eventBus = eventBus;
        }

        public void Start()
        {
            _eventBus.Listen(this);
        }

        public void OnEvent(IEventEmitter<EventA, EventA.Args> sender, EventA @event)
        {
            Debug.Log($"{nameof(ListenerA)} on {gameObject.name} received {@event} from {sender}");
        }
    }
}

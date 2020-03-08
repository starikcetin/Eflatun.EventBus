using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Sample
{
    public class ListenerB : MonoBehaviour
    {
        private EventBus _eventBus;

        [Inject]
        public void Init(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Start()
        {
            _eventBus.Listen<EventB>(OnEvent);
        }

        private void OnEvent(object sender, EventB @event)
        {
            Debug.Log($"{nameof(ListenerB)} on {gameObject.name} received {@event} from {sender}");
        }
    }
}

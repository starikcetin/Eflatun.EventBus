using Eflatun.EventBus;
using UnityEngine;
using Zenject;

namespace sample
{
    public class ListenerB : MonoBehaviour
    {
        private EventBus<EventB> _eventBus;

        [Inject]
        public void Init(EventBus<EventB> eventBus)
        {
            _eventBus = eventBus;
        }

        public void Start()
        {
            _eventBus.AddListener(OnEvent);
        }

        private void OnEvent(object sender, EventB @event)
        {
            Debug.Log($"{nameof(ListenerB)} on {gameObject.name} received {@event} from {sender}");
        }
    }
}

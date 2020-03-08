using Eflatun.EventBus;
using UnityEngine;
using Zenject;

namespace sample
{
    public class ListenerA : MonoBehaviour
    {
        private EventBus<EventA> _eventBus;

        [Inject]
        public void Init(EventBus<EventA> eventBus)
        {
            _eventBus = eventBus;
        }

        public void Start()
        {
            _eventBus.AddListener(OnEvent);
        }

        private void OnEvent(object sender, EventA @event)
        {
            var args = @event.Arguments;
            var message = args.Message;
            Debug.Log($"{nameof(ListenerA)} on {gameObject.name} received {@event} from {sender}");
        }
    }
}

using Eflatun.EventBus;
using UnityEngine;
using Zenject;

namespace sample
{
    public class ListenerD : MonoBehaviour
    {
        private EventBus<EventD> _eventBus;

        [Inject]
        public void Init(EventBus<EventD> eventBus)
        {
            _eventBus = eventBus;
        }

        public void Start()
        {
            _eventBus.AddListener(OnEvent);
        }

        private void OnEvent(object sender, EventD @event)
        {
            var args = @event.Arguments;
            var message = args.Message;
            Debug.Log($"{nameof(ListenerD)} on {gameObject.name} received {@event} from {sender}: Message={message}");
        }
    }
}

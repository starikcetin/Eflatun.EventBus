using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Sample
{
    public class ListenerD : MonoBehaviour
    {
        private EventBus _eventBus;

        [Inject]
        public void Init(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Start()
        {
            _eventBus.Listen<EventD>(OnEvent);
        }

        private void OnEvent(object sender, EventD @event)
        {
            var args = @event.Arguments;
            var message = args.Message;
            Debug.Log($"{nameof(ListenerD)} on {gameObject.name} received {@event} from {sender}: Message={message}");
        }
    }
}

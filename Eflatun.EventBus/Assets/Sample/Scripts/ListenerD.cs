using Eflatun.EventBus.interfaces;
using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Sample
{
    public class ListenerD : MonoBehaviour, IEventListener<EventD>
    {
        private EventBus<EventD> _eventBus;

        [Inject]
        public void Init(EventBus<EventD> eventBus)
        {
            _eventBus = eventBus;
        }

        public void Start()
        {
            _eventBus.Listen(this);
        }

        public void OnEvent(object sender, EventD @event)
        {
            var args = @event.Arguments;
            var message = args.Message;
            Debug.Log($"{nameof(ListenerD)} on {gameObject.name} received {@event} from {sender}: Message={message}");
        }
    }
}

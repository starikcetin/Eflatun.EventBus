using Eflatun.EventBus.interfaces;
using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Sample
{
    public class ListenerA : MonoBehaviour, IEventListener<EventA>
    {
        private EventBus<EventA> _eventBus;

        [Inject]
        public void Init(EventBus<EventA> eventBus)
        {
            _eventBus = eventBus;
        }

        public void Start()
        {
            _eventBus.Listen(this);
        }

        public void OnEvent(object sender, EventA @event)
        {
            var args = @event.Arguments;
            var message = args.Message;
            Debug.Log($"{nameof(ListenerA)} on {gameObject.name} received {@event} from {sender}");
        }
    }
}

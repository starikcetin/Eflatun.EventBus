using Eflatun.EventBus;
using UnityEngine;
using Zenject;

namespace sample
{
    public class ListenerE : IInitializable
    {
        private readonly EventBus<EventE> _eventBus;

        public ListenerE(EventBus<EventE> eventBus)
        {
            _eventBus = eventBus;
        }

        public void Initialize()
        {
            _eventBus.AddListener(OnEvent);
        }

        private void OnEvent(object sender, EventE @event)
        {
            Debug.Log($"{nameof(ListenerE)} received {@event} from {sender}");
        }
    }
}

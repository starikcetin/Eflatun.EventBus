using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Dev.Sample.Channels
{
    public class E1 : MonoBehaviour
    {
        private EventBus<EventA> _eventBus;

        [Inject]
        private void _Init(EventBus<EventA> eventBus)
        {
            _eventBus = eventBus;
        }

        private void Start()
        {
            _eventBus.Broadcast(this, new EventA());
        }
    }
}

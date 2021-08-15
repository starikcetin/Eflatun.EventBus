using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Dev.Samples.StressSample.Emitters
{
    public class EB : MonoBehaviour
    {
        private EventBus<EventFoo> _eventBus;

        [Inject]
        private void _Init(EventBus<EventFoo> eventBus)
        {
            _eventBus = eventBus;
        }

        private void Update()
        {
            _eventBus.Broadcast(this, new EventFoo());
        }
    }
}

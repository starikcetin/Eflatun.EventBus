using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Dev.Samples.StressSample.Emitters
{
    public class E1B : MonoBehaviour
    {
        private EventBus<EventFoo> _eventBus;

        [Inject]
        private void _Init(EventBus<EventFoo> eventBus)
        {
            _eventBus = eventBus;
        }

        private void Update()
        {
            _eventBus.EmitAndBroadcast(1, this, new EventFoo());
        }
    }
}

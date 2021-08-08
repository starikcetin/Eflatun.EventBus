using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Dev.Samples.StressSample.Scripts.Emitters
{
    public class E1 : MonoBehaviour
    {
        private EventBus<EventFoo> _eventBus;

        [Inject]
        private void _Init(EventBus<EventFoo> eventBus)
        {
            _eventBus = eventBus;
        }

        private void Update()
        {
            _eventBus.Emit(1, this, new EventFoo());
        }
    }
}

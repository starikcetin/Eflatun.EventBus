using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Dev.Samples.ListenerConfigSample
{
    public class E0 : MonoBehaviour
    {
        private EventBus<EventFoo> _eventBus;

        [Inject]
        private void _Init(EventBus<EventFoo> eventBus)
        {
            _eventBus = eventBus;
        }

        private void Start()
        {
            Debug.Log($"------ Start of {nameof(E0)} ------");
            _eventBus.Emit(0, this, new EventFoo());
        }
    }
}

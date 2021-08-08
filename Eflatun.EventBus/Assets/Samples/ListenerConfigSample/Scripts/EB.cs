using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Dev.Samples.ListenerConfigSample
{
    public class EB : MonoBehaviour
    {
        private EventBus<EventFoo> _eventBus;

        [Inject]
        private void _Init(EventBus<EventFoo> eventBus)
        {
            _eventBus = eventBus;
        }

        private void Start()
        {
            Debug.Log($"------ Start of {nameof(EB)} ------");
            _eventBus.Broadcast(this, new EventFoo());
        }
    }
}

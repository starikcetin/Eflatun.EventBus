using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Dev.Samples.ChannelsSample
{
    public class E2B : MonoBehaviour
    {
        private EventBus<EventFoo> _eventBus;

        [Inject]
        private void _Init(EventBus<EventFoo> eventBus)
        {
            _eventBus = eventBus;
        }

        private void Start()
        {
            Debug.Log($"------ Start of {nameof(E2B)} ------");
            _eventBus.Emit(2, this, new EventFoo());
        }
    }
}

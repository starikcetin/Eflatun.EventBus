using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Dev.Samples.ChannelsSample
{
    public class E1B : MonoBehaviour
    {
        private EventBus<EventFoo> _eventBus;

        [Inject]
        private void _Init(EventBus<EventFoo> eventBus)
        {
            _eventBus = eventBus;
        }

        private void Start()
        {
            Debug.Log($"------ Start of {nameof(E1B)} ------");
            _eventBus.EmitAndBroadcast(1, this, new EventFoo());
        }
    }
}

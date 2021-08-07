using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Dev.Samples.ChannelsTest
{
    public class E2 : MonoBehaviour
    {
        private EventBus<EventA> _eventBus;

        [Inject]
        private void _Init(EventBus<EventA> eventBus)
        {
            _eventBus = eventBus;
        }

        private void Start()
        {
            _eventBus.Emit(2, this, new EventA());
            Debug.Log($"------ End of {nameof(E2)} ------");
        }
    }
}
using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Dev.Samples.StressSample.Listeners
{
    public class LB : MonoBehaviour
    {
        private ulong _called;
        private EventBus<EventFoo> _eventBus;

        [Inject]
        private void _Init(EventBus<EventFoo> eventBus)
        {
            _eventBus = eventBus;
        }

        private void OnEnable()
        {
            var listenerConfig = ListenerConfig.BroadcastOnly(ListenPhase.Regular);
            _eventBus.AddListener(listenerConfig, OnEventA);
        }

        private void OnDisable()
        {
            _eventBus.RemoveListener(OnEventA);
        }

        private void OnApplicationQuit()
        {
            Debug.Log($"{name}\t{nameof(LB)}\t->\t{_called}");
        }

        private void OnEventA(EventMetadata metadata, EventFoo @event)
        {
            _called++;
        }
    }
}

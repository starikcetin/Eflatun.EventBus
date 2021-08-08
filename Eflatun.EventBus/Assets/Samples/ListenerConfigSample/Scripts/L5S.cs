using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Dev.Samples.ListenerConfigSample
{
    public class L5S : MonoBehaviour
    {
        private EventBus<EventFoo> _eventBus;
        private ListenerConfig _listenerConfig;

        [Inject]
        private void _Init(EventBus<EventFoo> eventBus)
        {
            _listenerConfig = ListenerConfig.SingleChannelNoBroadcast(0, ListenPhase.Regular);
            _eventBus = eventBus;
        }

        private void OnEnable()
        {
            _eventBus.AddListener(_listenerConfig, OnEventA);
        }

        private void OnDisable()
        {
            _eventBus.RemoveListener(_listenerConfig, OnEventA);
        }

        private void OnEventA(EventMetadata metadata, EventFoo @event)
        {
            Debug.Log($"{nameof(L5S)} received {nameof(EventFoo)} from {metadata.Sender.GetType().Name}");
        }
    }
}

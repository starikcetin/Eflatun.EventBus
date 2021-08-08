using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Dev.Samples.DestroyedListenerSample
{
    public class L0 : MonoBehaviour
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

        private void OnEventA(EventMetadata metadata, EventFoo @event)
        {
            Debug.Log($"{metadata.Sender.GetType().Name}\t->\t{nameof(L0)}");
        }
    }
}

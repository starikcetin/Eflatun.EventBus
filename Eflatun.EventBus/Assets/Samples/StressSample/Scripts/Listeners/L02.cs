using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Dev.Samples.StressSample.Scripts.Listeners
{
    public class L02 : MonoBehaviour
    {
        private uint _called;
        private EventBus<EventFoo> _eventBus;
        private ListenerConfig _listenerConfig;

        [Inject]
        private void _Init(EventBus<EventFoo> eventBus)
        {
            _listenerConfig = ListenerConfig.MultipleChannelsNoBroadcast(new[] {0, 2}, ListenPhase.Regular);
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

        private void OnApplicationQuit()
        {
            Debug.Log($"{nameof(L0)}\t->\t{_called}");
        }

        private void OnEventA(EventMetadata metadata, EventFoo @event)
        {
            _called++;
        }
    }
}

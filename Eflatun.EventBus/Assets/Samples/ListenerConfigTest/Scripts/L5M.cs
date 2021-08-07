using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Dev.Samples.ListenerConfigTest
{
    public class L5M : MonoBehaviour
    {
        private EventBus<EventA> _eventBus;
        private ListenerConfig _listenerConfig;

        [Inject]
        private void _Init(EventBus<EventA> eventBus)
        {
            _listenerConfig = ListenerConfig.MultipleChannelsNoBroadcast(new HashSet<int>(new[] {0}), ListenPhase.Regular);
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

        private void OnEventA(object sender, EventA @event)
        {
            Debug.Log($"{nameof(L5M)} received {nameof(EventA)} from {sender.GetType().Name}");
        }
    }
}

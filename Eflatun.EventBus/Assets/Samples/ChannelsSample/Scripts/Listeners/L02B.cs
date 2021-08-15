using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Dev.Samples.ChannelsSample
{
    public class L02B : MonoBehaviour
    {
        private EventBus<EventFoo> _eventBus;

        [Inject]
        private void _Init(EventBus<EventFoo> eventBus)
        {
            _eventBus = eventBus;
        }

        private void OnEnable()
        {
            var listenerConfig = ListenerConfig.MultipleChannelsAndBroadcast(new[] {0, 2}, ListenPhase.Regular);
            _eventBus.AddListener(listenerConfig, OnEventA);
        }

        private void OnDisable()
        {
            _eventBus.RemoveListener(OnEventA);
        }

        private void OnEventA(EventMetadata metadata, EventFoo @event)
        {
            Debug.Log($"{metadata.Sender.GetType().Name}\t->\t{nameof(L02B)}");
        }
    }
}

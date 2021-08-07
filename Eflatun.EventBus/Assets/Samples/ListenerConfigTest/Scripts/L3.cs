using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Dev.Samples.ListenerConfigTest
{
    public class L3 : MonoBehaviour
    {
        private EventBus<EventA> _eventBus;
        private ListenerConfig _listenerConfig;

        [Inject]
        private void _Init(EventBus<EventA> eventBus)
        {
            _listenerConfig = ListenerConfig.AllChannelsNoBroadcast(ListenPhase.Regular);
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
            Debug.Log($"{nameof(L3)} received {nameof(EventA)} from {sender.GetType().Name}");
        }
    }
}

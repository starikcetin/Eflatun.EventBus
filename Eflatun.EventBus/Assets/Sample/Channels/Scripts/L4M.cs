using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Dev.Sample.Channels
{
    public class L4M : MonoBehaviour
    {
        private EventBus<EventA> _eventBus;
        private ListenerConfig _listenerConfig;

        [Inject]
        private void _Init(EventBus<EventA> eventBus)
        {
            _listenerConfig = ListenerConfig.MultipleChannelsAndBroadcast(new[] {0}, ListenPhase.Regular);
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
            Debug.Log($"{nameof(L4M)} received {nameof(EventA)} from {sender.GetType().Name}");
        }
    }
}

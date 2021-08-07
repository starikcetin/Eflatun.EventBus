using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Dev.Samples.DestroyedListenerTest
{
    public class LB : MonoBehaviour
    {
        private EventBus<EventA> _eventBus;
        private ListenerConfig _listenerConfig;

        [Inject]
        private void _Init(EventBus<EventA> eventBus)
        {
            _listenerConfig = ListenerConfig.BroadcastOnly(ListenPhase.Regular);
            _eventBus = eventBus;
        }

        private void OnEnable()
        {
            _eventBus.AddListener(_listenerConfig, OnEventA);
        }

        private void OnEventA(object sender, EventA @event)
        {
            Debug.Log($"{sender.GetType().Name}\t->\t{nameof(LB)}");
        }
    }
}

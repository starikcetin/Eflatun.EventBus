using System.Collections;
using Eflatun.EventBus.interfaces;
using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Sample
{
    public class EmitterA : MonoBehaviour, IEventEmitter<EventA>
    {
        private EventBus<EventA> _eventBus;

        [Inject]
        public void Init(EventBus<EventA> eventBus)
        {
            _eventBus = eventBus;
        }

        public IEnumerator Start()
        {
            yield return null;
            var args = new EventA.Args($"sent from {nameof(EmitterA)}");
            var @event = new EventA(args);
            _eventBus.Emit(this, @event);
        }
    }
}

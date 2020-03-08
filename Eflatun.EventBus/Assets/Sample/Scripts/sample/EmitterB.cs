using System.Collections;
using Eflatun.EventBus;
using UnityEngine;
using Zenject;

namespace sample
{
    public class EmitterB : MonoBehaviour
    {
        private EventBus<EventB> _eventBus;

        [Inject]
        public void Init(EventBus<EventB> eventBus)
        {
            _eventBus = eventBus;
        }

        public IEnumerator Start()
        {
            yield return null;
            var args = new EventB.Args($"sent from {nameof(EmitterB)}");
            var @event = new EventB(args);
            _eventBus.Emit(this, @event);
        }
    }
}

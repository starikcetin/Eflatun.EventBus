using System.Collections;
using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Sample
{
    public class EmitterB : MonoBehaviour, IEventEmitter<EventB>
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
            _eventBus.Emit(new EventB(this, null));
        }
    }
}

using System.Collections;
using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Dev.Samples.DestroyedListenerSample
{
    public class E0B : MonoBehaviour
    {
        private EventBus<EventFoo> _eventBus;

        [Inject]
        private void _Init(EventBus<EventFoo> eventBus)
        {
            _eventBus = eventBus;
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(2);

            Debug.Log($"------ Start of {nameof(E0B)} ------");
            _eventBus.EmitAndBroadcast(0, this, new EventFoo());
        }
    }
}

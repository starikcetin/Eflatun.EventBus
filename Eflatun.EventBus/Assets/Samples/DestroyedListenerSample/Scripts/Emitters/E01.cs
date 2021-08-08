using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Dev.Samples.DestroyedListenerSample
{
    public class E01 : MonoBehaviour
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

            Debug.Log($"------ Start of {nameof(E01)} ------");
            _eventBus.Emit(new HashSet<int>(new[] {0, 1}), this, new EventFoo());
        }
    }
}

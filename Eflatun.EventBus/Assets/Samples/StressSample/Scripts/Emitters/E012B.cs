using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Dev.Samples.StressSample.Scripts.Emitters
{
    public class E012B : MonoBehaviour
    {
        private EventBus<EventFoo> _eventBus;

        [Inject]
        private void _Init(EventBus<EventFoo> eventBus)
        {
            _eventBus = eventBus;
        }

        private void Update()
        {
            _eventBus.EmitAndBroadcast(stackalloc[] {0, 1, 2}, this, new EventFoo());
        }
    }
}

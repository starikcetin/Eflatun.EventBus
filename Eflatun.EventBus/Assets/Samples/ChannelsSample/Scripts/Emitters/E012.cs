using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Dev.Samples.ChannelsSample
{
    public class E012 : MonoBehaviour
    {
        private EventBus<EventFoo> _eventBus;

        [Inject]
        private void _Init(EventBus<EventFoo> eventBus)
        {
            _eventBus = eventBus;
        }

        private void Start()
        {
            Debug.Log($"------ Start of {nameof(E012)} ------");
            _eventBus.Emit(new HashSet<int>(new[] {0, 1, 2}), this, new EventFoo());
        }
    }
}

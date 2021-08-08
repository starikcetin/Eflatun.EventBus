using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Dev.Samples.ChannelsSample
{
    public class E01B : MonoBehaviour
    {
        private EventBus<EventFoo> _eventBus;

        [Inject]
        private void _Init(EventBus<EventFoo> eventBus)
        {
            _eventBus = eventBus;
        }

        private void Start()
        {
            Debug.Log($"------ Start of {nameof(E01B)} ------");
            _eventBus.EmitAndBroadcast(new HashSet<int>(new[] {0, 1}), this, new EventFoo());
        }
    }
}

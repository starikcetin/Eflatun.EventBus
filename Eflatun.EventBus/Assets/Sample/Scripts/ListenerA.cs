using System;
using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Sample
{
    public class ListenerA : MonoBehaviour, IEventListener<EventA>
    {
        public void Awake()
        {
            EventBus<EventA>.Listen(this);
        }

        public void OnEvent(EventA @event)
        {
            Debug.Log($"{nameof(ListenerA)} on {gameObject.name} received: " + @event);
        }
    }
}

using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Sample
{
    public class ListenerB : MonoBehaviour, IEventListener<EventB>
    {
        public void Awake()
        {
            EventBus<EventB>.Listen(this);
        }

        public void OnEvent(EventB @event)
        {
            Debug.Log($"{nameof(ListenerB)} on {gameObject.name} received: " + @event);
        }
    }
}

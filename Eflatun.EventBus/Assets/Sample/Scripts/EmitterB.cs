using System;
using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Sample
{
    public class EmitterB : MonoBehaviour
    {
        private void Start()
        {
            EventBus<EventB>.Emit(new EventB());
        }
    }
}

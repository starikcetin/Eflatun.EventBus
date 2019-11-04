using System;
using UnityEngine;
using Zenject;

namespace Eflatun.EventBus.Sample
{
    public class EmitterA : MonoBehaviour
    {
        private void Start()
        {
            EventBus<EventA>.Emit(new EventA());
        }
    }
}

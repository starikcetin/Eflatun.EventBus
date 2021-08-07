using System;
using UnityEngine;

namespace Eflatun.EventBus.Dev.Samples.DestroyedListenerTest
{
    public class DelayedDestroyer : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 1);
        }
    }
}

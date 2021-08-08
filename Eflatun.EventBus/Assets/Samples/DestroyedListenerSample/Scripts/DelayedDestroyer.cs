using UnityEngine;

namespace Eflatun.EventBus.Dev.Samples.DestroyedListenerSample
{
    public class DelayedDestroyer : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 1);
        }
    }
}

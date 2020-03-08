using Eflatun.EventBus;
using UnityEngine;

namespace beforeAfterSample
{
    public class Listener
    {
        public Listener(EventBus<SampleEvent> sampleEventBus)
        {
            sampleEventBus.AddListenerBefore(OnBeforeSample);
            sampleEventBus.AddListener(OnSample);
            sampleEventBus.AddListenerAfter(OnAfterSample);
        }

        private void OnAfterSample(object sender, SampleEvent @event)
        {
            Debug.Log("On After Sample");
        }

        private void OnSample(object sender, SampleEvent @event)
        {
            Debug.Log("On Sample");
        }

        private void OnBeforeSample(object sender, SampleEvent @event)
        {
            Debug.Log("On Before Sample");
        }
    }
}
using Eflatun.EventBus;
using Zenject;

namespace beforeAfterSample
{
    public class Emitter : IInitializable
    {
        private readonly EventBus<SampleEvent> _sampleEventBus;

        public Emitter(EventBus<SampleEvent> sampleEventBus)
        {
            _sampleEventBus = sampleEventBus;
        }

        public void Initialize()
        {
            _sampleEventBus.Emit(this, new SampleEvent(new SampleEvent.Args()));
        }
    }
}
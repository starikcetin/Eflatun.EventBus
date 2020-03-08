using Zenject;

namespace Eflatun.EventBus.Sample
{
    public class EmitterE : ITickable
    {
        private readonly EventBus _eventBus;

        public EmitterE(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private int _ticks = 0;

        public void Tick()
        {
            _ticks++;

            if (_ticks % 10 == 0)
            {
                var @event = new EventE();
                _eventBus.Emit(this, @event);
            }
        }
    }
}

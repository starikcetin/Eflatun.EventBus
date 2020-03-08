using Zenject;

namespace Eflatun.EventBus.Sample
{
    public class EmitterC : ITickable
    {
        private readonly EventBus _eventBus;

        public EmitterC(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private int _ticks = 0;

        public void Tick()
        {
            _ticks++;

            if (_ticks % 10 == 0)
            {
                var args = new EventC.Args(_ticks);
                var @event = new EventC(args);
                _eventBus.Emit(this, @event);
            }
        }
    }
}

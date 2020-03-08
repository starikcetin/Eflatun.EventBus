using Eflatun.EventBus;
using Zenject;

namespace sample
{
    public class EmitterC : ITickable
    {
        private readonly EventBus<EventC> _eventBus;

        public EmitterC(EventBus<EventC> eventBus)
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

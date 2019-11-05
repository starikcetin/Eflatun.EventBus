using Zenject;

namespace Eflatun.EventBus.Sample
{
    public class EmitterC : ITickable, IEventEmitter<EventC, EventC.Args>
    {
        private readonly EventBus<EventC, EventC.Args> _eventBus;

        public EmitterC(EventBus<EventC, EventC.Args> eventBus)
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

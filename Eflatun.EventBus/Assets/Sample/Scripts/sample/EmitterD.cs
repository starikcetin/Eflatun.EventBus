using Eflatun.EventBus;
using Zenject;

namespace sample
{
    public class EmitterD : ITickable
    {
        private readonly EventBus<EventD> _eventBus;

        public EmitterD(EventBus<EventD> eventBus)
        {
            _eventBus = eventBus;
        }

        private int _ticks = 0;

        public void Tick()
        {
            _ticks++;

            if (_ticks % 10 == 0)
            {
                var args = new EventD.Args(_ticks.ToString());
                var @event = new EventD(args);
                _eventBus.Emit(this, @event);
            }
        }
    }
}

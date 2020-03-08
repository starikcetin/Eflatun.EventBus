﻿using Zenject;

namespace Eflatun.EventBus.Sample
{
    public class EmitterD : ITickable
    {
        private readonly EventBus _eventBus;

        public EmitterD(EventBus eventBus)
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

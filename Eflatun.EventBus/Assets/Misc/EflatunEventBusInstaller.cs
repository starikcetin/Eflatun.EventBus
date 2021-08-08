using Zenject;

namespace Eflatun.EventBus.Dev.Misc
{
    public class EflatunEventBusInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(EventBus<>)).ToSelf().AsSingle();
        }
    }
}

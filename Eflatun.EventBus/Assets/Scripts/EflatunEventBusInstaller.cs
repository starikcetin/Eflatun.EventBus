using Zenject;

namespace Eflatun.EventBus.Dev
{
    public class EflatunEventBusInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(EventBus<>)).ToSelf().AsSingle();
        }
    }
}

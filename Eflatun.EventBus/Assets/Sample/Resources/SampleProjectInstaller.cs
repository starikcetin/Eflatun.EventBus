using Zenject;

namespace Eflatun.EventBus.Sample
{
    public class SampleProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<EmitterC>().AsSingle();
            Container.BindInterfacesTo<ListenerC>().AsSingle();
            Container.BindInterfacesTo<EmitterD>().AsSingle();
            Container.BindInterfacesTo<EmitterE>().AsSingle();
            Container.BindInterfacesTo<ListenerE>().AsSingle();
        }
    }
}

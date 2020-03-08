using Zenject;

namespace sample
{
    public class SampleInstaller : MonoInstaller
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

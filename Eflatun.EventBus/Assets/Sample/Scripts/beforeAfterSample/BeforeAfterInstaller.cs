using Zenject;

namespace beforeAfterSample
{
    public class BeforeAfterInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Emitter>().AsSingle().NonLazy();
            Container.Bind<Listener>().AsSingle().NonLazy();
        }
    }
}

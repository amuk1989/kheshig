using Zenject;

namespace Main.Bootstrap
{
    public class MainInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Install<DOTSIntsaller>();
            Container.Install<ConfigInstaller>();
        }
    }
}
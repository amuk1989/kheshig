using Unity.Entities;
using Zenject;

namespace Main.Bootstrap
{
    public class DOTSIntsaller:Installer
    {
        public override void InstallBindings()
        {
            Container
                .Bind<EntityManager>()
                .FromInstance(World.DefaultGameObjectInjectionWorld.EntityManager)
                .AsSingle();
        }
    }
}
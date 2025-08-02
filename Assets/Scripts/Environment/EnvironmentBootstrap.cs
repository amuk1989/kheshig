using VContainer;

namespace Environment
{
    public static class EnvironmentBootstrap
    {
        public static void InstallBindings(IContainerBuilder container)
        {
            container.Register<SpawnHandler.Factory>(Lifetime.Singleton);

            container
                .Register<EnvironmentService>(Lifetime.Singleton)
                .AsImplementedInterfaces();
        }
    }
}
using VContainer;

namespace AssetsController
{
    public static class AssetsBootstrap
    {
        public static RegistrationBuilder InstallBindings(IContainerBuilder builder)
        {
            return builder
                .Register<AssetService>(Lifetime.Singleton)
                .AsImplementedInterfaces();
        }
    }
}
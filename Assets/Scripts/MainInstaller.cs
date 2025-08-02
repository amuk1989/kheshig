using AssetsController;
using Environment;
using VContainer;
using VContainer.Unity;

public class MainInstaller : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        AssetsBootstrap.InstallBindings(builder);
        EnvironmentBootstrap.InstallBindings(builder);
    }
}
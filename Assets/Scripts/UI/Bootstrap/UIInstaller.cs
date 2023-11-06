using UI.Factories;
using UI.Services;
using UI.Utils;
using UI.Views;
using Zenject;

namespace UI.Bootstrap
{
    public class UIInstaller: Installer
    {
        public override void InstallBindings()
        {
            Container
                .BindFactory<string, BaseUI, BaseUI.Factory>()
                .FromFactory<UIFactory>();

            Container
                .BindInterfacesTo<UIService>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<MainCanvas>()
                .FromComponentInNewPrefabResource(UIConsts.Canvas)
                .AsSingle();
        }
    }
}
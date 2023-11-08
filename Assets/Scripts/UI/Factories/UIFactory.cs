using UI.Interfaces;
using UI.Views;
using Zenject;

namespace UI.Factories
{
    public class UIFactory: IFactory<string, BaseUI>
    {
        private readonly DiContainer _diContainer;
        private readonly IUIService _uiService;

        public UIFactory(DiContainer diContainer, IUIService uiService)
        {
            _diContainer = diContainer;
            _uiService = uiService;
        }

        public BaseUI Create(string param)
        {
            return _diContainer.InstantiatePrefabResourceForComponent<BaseUI>(param, _uiService.Canvas.transform);
        }
    }
}
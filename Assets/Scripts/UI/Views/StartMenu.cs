using System;
using UI.Interfaces;
using UI.Utils;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Views
{
    public class StartMenu : BaseUI
    {
        [SerializeField] private Button _newGameButton;

        private IUIService _uiService;

        [Inject]
        private void Construct(IUIService uiService)
        {
            _uiService = uiService;
        }

        private void Start()
        {
            _newGameButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _uiService.GetOrCreateWindowFromResource(UIConsts.CharacterUpdateUI);
                    _uiService.DestroyWindow<StartMenu>();
                })
                .AddTo(this);
        }
    }
}
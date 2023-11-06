using System.Collections.Generic;
using UI.Interfaces;
using UI.Utils;
using UI.Views;
using UnityEngine;
using Zenject;

namespace UI.Services
{
    public class UIService: IUIService, IInitializable
    {
        private readonly MainCanvas _mainCanvas;
        private readonly BaseUI.Factory _uiFactory;
        private readonly Dictionary<string, BaseUI> _windows = new();

        private UIService(MainCanvas mainCanvas, BaseUI.Factory uiFactory)
        {
            _mainCanvas = mainCanvas;
            _uiFactory = uiFactory;
        }

        public Canvas Canvas => _mainCanvas.Canvas;

        public void Initialize()
        {
            CreateWindowFromResource(UIConsts.StartMenu);
        }

        public BaseUI CreateWindowFromResource(string path)
        {
            if (_windows.TryGetValue(path, out var window))
            {
                window.Enable();
            }
            else
            {
                _windows[path] = _uiFactory.Create(path);
            }
            
            return _windows[path];
        }
    }
}
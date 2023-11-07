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
            GetOrCreateWindowFromResource(UIConsts.StartMenu);
        }

        public BaseUI GetOrCreateWindowFromResource(string path)
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

        public void HideWindow<TWindow>() where TWindow : BaseUI
        {
            foreach (var baseUI in _windows.Values)
            {
                var window = baseUI as TWindow;
                if (window == null) continue;
                window.Disable();
                return;
            }
        }

        public void DestroyWindow<TWindow>() where TWindow : BaseUI
        {
            var destroyId = string.Empty;
            foreach (var baseUI in _windows)
            {
                var window = baseUI.Value as TWindow;
                if (window == null) continue;
                window.Dispose();
                destroyId = baseUI.Key;
                break;
            }

            if (_windows.ContainsKey(destroyId)) _windows.Remove(destroyId);
        }
    }
}
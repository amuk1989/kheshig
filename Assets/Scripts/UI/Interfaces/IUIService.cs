using UI.Views;
using UnityEngine;

namespace UI.Interfaces
{
    public interface IUIService
    {
        public Canvas Canvas { get; }
        public BaseUI GetOrCreateWindowFromResource(string path);
        public void HideWindow<TWindow>() where TWindow : BaseUI;
        public void DestroyWindow<TWindow>() where TWindow : BaseUI;
    }
}
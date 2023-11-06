using UI.Views;
using UnityEngine;

namespace UI.Interfaces
{
    public interface IUIService
    {
        public Canvas Canvas { get; }
        public BaseUI CreateWindowFromResource(string path);
    }
}
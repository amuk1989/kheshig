using UnityEngine;
using Zenject;

namespace UI.Views
{
    internal class MainCanvas : MonoBehaviour
    {
        public class Factory:PlaceholderFactory<MainCanvas> { }
        
        [SerializeField] private Canvas _mainCanvas;
        
        public Canvas Canvas => _mainCanvas;
    }
}
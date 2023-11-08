using System;
using UnityEngine;
using Zenject;

namespace UI.Views
{
    public abstract class BaseUI: MonoBehaviour, IDisposable
    {
        public class Factory: PlaceholderFactory<string, BaseUI>
        {
        }
        
        public void Enable()
        {
            gameObject.SetActive(true);
        }
        
        public void Disable()
        {
            gameObject.SetActive(false);
        } 
        
        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}
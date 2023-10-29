using UnityEngine;
using Zenject;

namespace Main.Bootstrap
{
    [CreateAssetMenu(fileName = "DebugRegistry", menuName = "Registries/DebugRegistry")]
    public class DebugRegistry: ScriptableObjectInstaller
    {
        [SerializeField] private bool _isEnable;

        public override void InstallBindings()
        {
            if (!_isEnable) return;
        }
        
        private void InstallDebugRegistry<TRegistry>(TRegistry registry) where TRegistry:ScriptableObject
        {
            if (registry == null) return;
            
            Container
                .Bind<TRegistry>()
                .FromInstance(registry);
        }
    }
}
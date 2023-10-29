using Character.Configs;
using UnityEngine;
using Utility;
using Zenject;

namespace Main.Bootstrap
{
    [CreateAssetMenu(fileName = "ConfigRegistry", menuName = "Registries/ConfigRegistry", order = 0)]
    public class ConfigRegistry : ScriptableObjectInstaller
    {
        [SerializeField] private CharacterConfig _characterConfig;

        public override void InstallBindings()
        {
            Container.InstallRegistry(_characterConfig.Data);
        }
    }
}
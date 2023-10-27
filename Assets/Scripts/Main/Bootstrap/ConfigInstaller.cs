using Character.Configs;
using Main.Data;
using Unity.Entities;
using Zenject;

namespace Main.Bootstrap
{
    public class ConfigInstaller: Installer
    {
        private CharacterConfigData _characterConfigData;

        public ConfigInstaller(CharacterConfigData characterConfigData)
        {
            _characterConfigData = characterConfigData;
        }

        public override void InstallBindings()
        {
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var entity =  entityManager.CreateSingleton<ConfigTag>();
            
            entityManager.AddComponentData(entity, _characterConfigData);
        }
    }
}